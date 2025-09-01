using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Buffers;

namespace FSUIPC;

internal class ConnectionInfo
{

	internal uint messageID;

	internal IntPtr hWnd;

	internal IntPtr atomFileName;

	internal IntPtr hMap;

	internal IntPtr pView;

}

public static class FSUIPCConnection
{

	private static readonly ConnectionInfo? connectionInfo = new();

	internal static ConcurrentDictionary<string, List<Offset>> dataGroups = new();

	private static readonly string LVARGroup = "~~LVARGroup~~";

	private static readonly string FS6IPCMessageName = "FsasmLib:IPC";

	internal static readonly uint MaximumDataSize = 32512u;

	private static readonly int ERROR_ALREADY_EXISTS = 387;


	private static int tryCount = 0;


	internal static void AddOffset(Offset offset) => dataGroups.GetOrAdd(offset.Group, _ => []).Add(offset);

	public static void Open()
	{
		if (connectionInfo == null)
			return;

		// Buscar ventana FSUIPC/WideFS
		IntPtr hWnd = Win32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "UIPCMAIN", IntPtr.Zero);
		if (hWnd == IntPtr.Zero)
			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOFS, "FSUIPC/WideFS not running.");
		connectionInfo.hWnd = hWnd;

		// Registrar mensaje
		uint msgId = Win32.RegisterWindowMessage(FS6IPCMessageName);
		if (msgId == 0)
			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_REGMSG, "Could not register IPC message.");
		connectionInfo.messageID = msgId;

		tryCount++;

		// Construcción ultra rápida del nombre único
		int pid = Environment.ProcessId;
		string fsName = FS6IPCMessageName;

		// Calcular tamaño máximo necesario (nombre base + ":" + pidHex + ":" + tryCountHex)
		// El pid en HEX puede ser hasta 8 chars, el tryCount igual. Le damos margen.
		Span<char> buffer = stackalloc char[fsName.Length + 1 + 8 + 1 + 8];

		// Copiar FS6IPCMessageName
		fsName.AsSpan().CopyTo(buffer);
		int pos = fsName.Length;


		// pid y tryCount en HEX dentro del buffer (sin heap extra)
		buffer[pos++] = ':';
		pid.TryFormat(buffer.Slice(pos), out int w1, "X");
		pos += w1;

		buffer[pos++] = ':';
		tryCount.TryFormat(buffer.Slice(pos), out int w2, "X");
		pos += w2;


		// Crear string final sin concatenaciones
		string uniqueName = new(buffer[..pos]);

		// Atom
		IntPtr atom = Win32.GlobalAddAtom(uniqueName);
		if (atom == IntPtr.Zero)
			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_ATOM, "Could not add Global Atom.");
		connectionInfo.atomFileName = atom;

		// File mapping
		IntPtr hMap = Win32.CreateFileMapping(
			new IntPtr(-1),
			IntPtr.Zero,
			PageProtection.ReadWrite,
			0,
			MaximumDataSize + 256,
			uniqueName);

		if (hMap == IntPtr.Zero || Marshal.GetLastWin32Error() == ERROR_ALREADY_EXISTS)
			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_MAP, "Could not create file mapping.");
		connectionInfo.hMap = hMap;

		// Mapear vista
		IntPtr pView = Win32.MapViewOfFile(hMap, DesiredAccess.MapWrite, 0, 0, 0);
		if (pView == IntPtr.Zero)
			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_VIEW, "Could not map file view.");
		connectionInfo.pView = pView;
	}


	#region PROCESS
	public static unsafe void Process(string groupName)
	{
		lock (dataGroups)
		{
			// 1) Recolectar offsets SIN LINQ ni listas intermedias
			int totalCount = 0;

			if (!dataGroups.TryGetValue(groupName, out var list))
				throw new Exception($"Group '{groupName}' does not exist.");

			totalCount += list.Count;

			var offsetsPool = ArrayPool<Offset>.Shared;
			var offsets = offsetsPool.Rent(totalCount);

			int n = 0;

			for (int i = 0; i < list.Count; i++) offsets[n++] = list[i];


			// 2) blockSize
			int blockSize = 80;
			blockSize = (blockSize << 8) + 97;

			// 3) Evaluar acciones pendientes (Autosense con comparación unsafe)
			for (int i = 0; i < n; i++)
			{
				var off = offsets[i];
				switch (off.ActionAtNextProcess)
				{
					case OffsetAction.Read:
						off.Write = false;
						off.ActionAtNextProcess = OffsetAction.Autosense;
						break;

					case OffsetAction.Write:
						off.Write = true;
						off.ActionAtNextProcess = off.writeOnly ? OffsetAction.Write : OffsetAction.Autosense;
						break;

					case OffsetAction.Autosense:
						if (!off.Write && off.DataValue != null)
							off.Write = !BytesEqualUnsafe(off.DataValue, off.lastReadValue, off.DataLength);
						break;
				}
			}

			blockSize = (blockSize << 16) + 30060; // <- este valor se usa en los headers de READ

			// 4) Construcción en memoria mapeada con punteros (sin Marshal.Copy por pieza)
			long baseAddr = connectionInfo.pView.ToInt64();
			byte* basePtr = (byte*)connectionInfo.pView.ToPointer();
			byte* cur = basePtr;

			for (int i = 0; i < n; i++)
			{
				var off = offsets[i];

				int headerLenInts;
				if (off.Write)
				{
					// Header WRITE: [2, Address, DataLength]
					headerLenInts = 3;
					EnsureFits(cur, basePtr, headerLenInts, off.DataLength, (int)MaximumDataSize);

					WriteInt(cur + 0, 2);
					WriteInt(cur + 4, off.Address);
					WriteInt(cur + 8, off.DataLength);
					cur += headerLenInts * 4;

					// Validación de tamaño y copia de datos
					var src = off.DataValue;
					if (src.Length != off.DataLength)
						throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_WRITE_OVERFLOW,
							$"Offset {off.Address:X} is declared with {off.DataLength} bytes, but attempted write has {src.Length} bytes.");

					fixed (byte* pSrc = src)
						Buffer.MemoryCopy(pSrc, cur, off.DataLength, off.DataLength);

					cur += off.DataLength;
				}
				else if (!off.WriteOnly)
				{
					// Header READ: [1, Address, DataLength, blockSize]
					headerLenInts = 4;
					EnsureFits(cur, basePtr, headerLenInts, off.DataLength, (int)MaximumDataSize);

					WriteInt(cur + 0, 1);
					WriteInt(cur + 4, off.Address);
					WriteInt(cur + 8, off.DataLength);
					WriteInt(cur + 12, blockSize);

					// FileAddress apunta al inicio de los datos (después del header)
					off.FileAddress = baseAddr + (cur - basePtr) + (headerLenInts * 4);

					cur += headerLenInts * 4;
					cur += off.DataLength; // reservar espacio para los datos de respuesta
				}
				// Si es WriteOnly y no Write, no se arma nada.
			}

			// Terminador
			WriteInt(cur, 0);

			// 5) Enviar mensaje
			_ = Win32.SendMessageTimeout(connectionInfo.hWnd, connectionInfo.messageID, connectionInfo.atomFileName, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_BLOCK, 1000u, out _);

			// 6) Leer resultados
			for (int i = 0; i < n; i++)
			{
				var off = offsets[i];

				if (!off.Write && !off.WriteOnly)
				{
					var len = off.DataLength;
					var buf = new byte[len];
					fixed (byte* dst = buf)
						Buffer.MemoryCopy((void*)off.FileAddress, dst, len, len);
					off.DataValue = buf;
				}

				off.CheckForChanges();

				Buffer.BlockCopy(off.lastReadValue, 0, off.previousValue, 0, off.lastReadValue.Length);
				Buffer.BlockCopy(off.DataValue, 0, off.lastReadValue, 0, off.DataValue.Length);

				off.Write = false;
			}

			// 7) Devolver el array al pool
			Array.Clear(offsets, 0, n); // limpiar referencias
			offsetsPool.Return(offsets);
		}
	}

	// -------- helpers --------
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static unsafe void EnsureFits(byte* cur, byte* basePtr, int headerLenInts, int dataLen, int maxSize)
	{
		var used = (int)((cur - basePtr) + (headerLenInts * 4) + dataLen + 4);
		if (used > maxSize)
			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_SIZE,
				"The requested data exceeded the maximum allowed in one Process().");
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static unsafe void WriteInt(byte* p, int value) => *(int*)p = value;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static unsafe bool BytesEqualUnsafe(byte[] a, byte[] b, int len)
	{
		if (a == null || b == null) return a == b;
		if (a.Length < len || b.Length < len) return false;
		if (ReferenceEquals(a, b)) return true;

		fixed (byte* pa = a)
		fixed (byte* pb = b)
		{
			byte* x = pa;
			byte* y = pb;

			int n = len;
			while (n >= 8)
			{
				if (*(ulong*)x != *(ulong*)y) return false;
				x += 8; y += 8; n -= 8;
			}
			if (n >= 4)
			{
				if (*(uint*)x != *(uint*)y) return false;
				x += 4; y += 4; n -= 4;
			}
			while (n-- > 0)
			{
				if (*x != *y) return false;
				x++; y++;
			}
			return true;
		}
	}
	#endregion


	#region LVAR

	// READ
	private static readonly Offset<int> offsetCommand = new(LVARGroup, 3436, WriteOnly: true);
	private static readonly Offset<string> offsetParam = new(LVARGroup, 3440, 128, WriteOnly: true);
	private static readonly Offset<double> offsetResult = new(LVARGroup, 26360);

	public static double ReadLVar(string LVAR)
	{

		offsetCommand.Value = 26360;
		offsetParam.Value = $":{LVAR}";

		Process(LVARGroup);

		return offsetResult.Value;

	}



	// WRITE
	private static readonly Offset<double> writeValueOffset = new(LVARGroup, 26360, WriteOnly: true);
	private static readonly Offset<int> writeLvarIdOffset = new(LVARGroup, 3436, WriteOnly: true);
	private static readonly Offset<string> writeLvarNameOffset = new(LVARGroup, 3440, 128, WriteOnly: true);

	public static void WriteLVar(string lvarName, double newValue)
	{

		writeValueOffset.Value = newValue;
		writeLvarIdOffset.Value = 26360;                // ID fijo asociado al WRITE
		writeLvarNameOffset.Value = "::" + lvarName;    // Nombre del LVAR con prefijo

		Process(LVARGroup);

	}

	#endregion

}
