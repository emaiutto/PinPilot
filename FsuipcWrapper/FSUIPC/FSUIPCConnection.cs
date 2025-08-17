using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace FSUIPC;

public static class FSUIPCConnection
{

	 
	private static readonly Dictionary<byte, ConnectionInfo> connections = [];

	internal static ConcurrentDictionary<string, List<Offset>> dataGroups = new();

	
	private static readonly string FS6IPCMessageName = "FsasmLib:IPC";

	private static readonly string SystemOffsetsGroup = "~SystemOffsets~";

	internal static readonly uint MaximumDataSize = 32512u;

	private static readonly int ERROR_ALREADY_EXISTS = 387;

	

	private static IntPtr pNext = IntPtr.Zero;

	private static int tryCount = 0;

	private static bool groupsIsolatedToThread = false;



	public static bool GroupsIsolatedToThread { get; set; }

 

	public static FSUIPCVersion FSUIPCVersionForClass(byte ClassInstance) => connections[ClassInstance].fsuipcVersion;


	public static bool WideClientForClass(byte ClassInstance)
	{
		if (connections.TryGetValue(ClassInstance, out ConnectionInfo? value)) return value.wideClient;

		throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "You cannot access the WideClient connection status until the connection is open.");
	}

	public static bool IsConnectionOpenForClass(byte ClassInstance) => connections.ContainsKey(ClassInstance);

	public static FsVersion FlightSimVersionForClass(byte ClassInstance)
	{
		if (connections.TryGetValue(ClassInstance, out ConnectionInfo? value)) return value.fsVersion;

		throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "You cannot access the Flight Sim Version until the connection is open.");
	}

	internal static void AddOffset(Offset Offset)
	{
		string key = groupsIsolatedToThread ? (Environment.CurrentManagedThreadId + "~" + Offset.Group) : Offset.Group;
		
		dataGroups.GetOrAdd(key, []).Add(Offset);
		
		Offset.connected = true;
	}

	internal static void RemoveOffset(Offset Offset)
	{
		string key = groupsIsolatedToThread ? (Environment.CurrentManagedThreadId + "~" + Offset.Group) : Offset.Group;
		
		if (dataGroups.TryGetValue(key, out List<Offset>? value))
		{
			value.Remove(Offset);
			Offset.Connected = false;
		}
	}

	public static void Open()
	{

		byte ClassInstance = 0;

		DeleteGroup(SystemOffsetsGroup);
		 
		if (!connections.ContainsKey(ClassInstance))
		{
			ConnectionInfo? connectionInfo = new();
			connections.Add(ClassInstance, connectionInfo);

			if (ClassInstance == 0)
			{
				connectionInfo.hWnd = Win32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "UIPCMAIN", IntPtr.Zero);
				//if (connectionInfo.hWnd == IntPtr.Zero)
				//{
				//	connectionInfo.hWnd = Win32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "FS98MAIN", IntPtr.Zero);
				//	connectionInfo.wideClient = true;
				//}
			}
			//else
			//{
			//	connectionInfo.hWnd = Win32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "FS98MAIN" + ClassInstance.ToString("D2"), IntPtr.Zero);
			//	connectionInfo.wideClient = true;
			//}

			if (connectionInfo.hWnd == IntPtr.Zero)
			{
				Close(ClassInstance);
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOFS, "Cannot find FSUIPC or WideFS running on this machine.");
			}
			connectionInfo.messageID = Win32.RegisterWindowMessage(FS6IPCMessageName);
			if (connectionInfo.messageID == 0)
			{
				Close(ClassInstance);
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_REGMSG, "Could not register the IPC window message");
			}
			tryCount++;
			string text = FS6IPCMessageName + ":" + System.Diagnostics.Process.GetCurrentProcess().Id.ToString("X") + ":" + tryCount.ToString("X");
			connectionInfo.atomFileName = Win32.GlobalAddAtom(text);
			if (connectionInfo.atomFileName == IntPtr.Zero)
			{
				Close(ClassInstance);
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_ATOM, "Could not add the Global Atom for the file mapping path.");
			}
			connectionInfo.hMap = Win32.CreateFileMapping(new IntPtr(-1), IntPtr.Zero, PageProtection.ReadWrite, 0u, MaximumDataSize + 256, text);
			if (connectionInfo.hMap == IntPtr.Zero || Marshal.GetLastWin32Error() == ERROR_ALREADY_EXISTS)
			{
				Close(ClassInstance);
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_MAP, "Could not create file mapping.");
			}
			connectionInfo.pView = Win32.MapViewOfFile(connectionInfo.hMap, DesiredAccess.MapWrite, 0u, 0u, 0u);
			if (connectionInfo.pView == IntPtr.Zero)
			{
				Close(ClassInstance);
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_VIEW, "Could not Open file view.");
			}
			Offset<int> offset = new(SystemOffsetsGroup, 13060);
			Offset<int> offset2 = new(SystemOffsetsGroup, 13064);

			//Offset<byte> offset3 = new(SystemOffsetsGroup, 12580);


			SpinWait.SpinUntil(() =>
								{
									Process(ClassInstance, SystemOffsetsGroup);
									return offset.Value != 0 && offset2.Value != 0;
								},	150 // timeout total en ms (3 intentos * 50)
							);

   //         int retries = 3;
			//while (retries-- > 0 && (offset.Value == 0 || offset2.Value == 0))
			//{
			//	Process(ClassInstance, SystemOffsetsGroup);
			//	Thread.Sleep(50);
			//}


			//if (offset.Value < 429391877 || (offset2.Value & 0xFFFF0000u) != 4208852992u)
			//{
			//	if (connectionInfo.wideClient)
			//	{
			//		Close(ClassInstance);
			//		throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_RUNNING, "FSUIPC is not running.");
			//	}
			//	Close(ClassInstance);
			//	throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_VERSION, "Incorrect version of FSUIPC.");
			//}

			//int num = offset2.Value & 0xFFFF;

			//connectionInfo.fsVersion = new FsVersion((FlightSim)num, offset3.Value);
			//if (RequiredFlightSimVersion != 0 && RequiredFlightSimVersion != num)
			//{
			//	Close(ClassInstance);
			//	throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_WRONGFS, "Incorrect version of Flight Sim");
			//}
			DeleteGroup(SystemOffsetsGroup);
			
		 

			//connectionInfo.fsuipcVersion = new FSUIPCVersion(offset.Value);
			return;
		}
		throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_OPEN, "The connection to FSUIPC is already open.");
	}

 

	public static void DeleteGroup(string groupName)
	{
		string key = groupsIsolatedToThread ? (Environment.CurrentManagedThreadId + "~" + groupName) : groupName;
		
		if (!dataGroups.ContainsKey(key)) return;

		_ = dataGroups.TryRemove(key, out _);
	}

	public static void Close()
	{
		byte[] array = new byte[connections.Count];
		connections.Keys.CopyTo(array, 0);
		byte[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Close(array2[i]);
		}
	}

	public static void Close(byte ClassInstance)
	{
		lock (dataGroups)
		{
			if (connections.TryGetValue(ClassInstance, out ConnectionInfo? connectionInfo))
			{
				connectionInfo.hWnd = IntPtr.Zero;
				connectionInfo.messageID = 0u;
				if (connectionInfo.atomFileName != IntPtr.Zero)
				{
					Win32.GlobalDeleteAtom(connectionInfo.atomFileName);
					connectionInfo.atomFileName = IntPtr.Zero;
				}
				if (connectionInfo.pView != IntPtr.Zero)
				{
					Win32.UnmapViewOfFile(connectionInfo.pView);
					connectionInfo.pView = IntPtr.Zero;
				}
				if (connectionInfo.hMap != IntPtr.Zero)
				{
					Win32.CloseHandle(connectionInfo.hMap);
					connectionInfo.hMap = IntPtr.Zero;
				}
				connections.Remove(ClassInstance);
			}
		}
	}

	public static void Process() => Process(0, "");

	public static void Process(string GroupName) => Process(0, GroupName);

	public static void Process(IEnumerable<string> GroupNames) => Process(0, GroupNames);

	public static void Process(byte ClassInstance) => Process(ClassInstance, "");

	public static void Process(byte ClassInstance, string GroupName)
	{
		List<string> list = [GroupName];
		Process(ClassInstance, list);
	}



	public static void Process(byte ClassInstance, IEnumerable<string> GroupNames)
	{
		lock (dataGroups)
		{
			double num = 0.0;
			double num2 = 0.0;
			double num3 = 0.0;
			double num4 = 0.0;
			double num5 = 0.0;

			Stopwatch stopwatch = new();
			stopwatch.Start();

			ConnectionInfo? connectionInfo = null;
			if (connections.ContainsKey(ClassInstance))
			{
				connectionInfo = connections[ClassInstance];
				if (connectionInfo.pView == IntPtr.Zero)
				{
					Close(ClassInstance);
					throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to FSUIPC is not open.");
				}
				int num6 = 80;

				List<Offset> list = [];
				long num7 = connectionInfo.pView.ToInt64();
				long num8 = num7;
				int num9 = 0;
				int num10 = 0;

				List<Offset> list2 = [];
				foreach (string GroupName in GroupNames)
				{
					string key = groupsIsolatedToThread ? (Environment.CurrentManagedThreadId + "~" + GroupName) : GroupName;
					if (dataGroups.ContainsKey(key))
					{
						dataGroups.TryGetValue(key, out var value);
						list2.AddRange(value);
						continue;
					}
					throw new Exception("Group '" + GroupName + "' does not exist.");
				}
				num6 = (num6 << 8) + 97;
				foreach (Offset item in list2)
				{
					switch (item.ActionAtNextProcess)
					{
						case OffsetAction.Read:
							item.Write = false;
							item.ActionAtNextProcess = OffsetAction.Autosense;
							break;

						case OffsetAction.Write:
							item.Write = true;
							item.ActionAtNextProcess = item.writeOnly ? OffsetAction.Write : OffsetAction.Autosense;
							break;

						case OffsetAction.Autosense:
							{
								if (item.Write)
								{
									break;
								}
								byte[] dataValue = item.DataValue;
								if (dataValue != null)
								{
									byte[] lastReadValue = item.lastReadValue;
									bool flag = true;
									for (int i = 0; i < lastReadValue.Length; i++)
									{
										flag &= lastReadValue[i] == dataValue[i];
									}
									item.Write = !flag;
								}
								break;
							}
					}
					num10++;
				}
				num10 = 0;
				num6 = (num6 << 16) + 30060;
				foreach (Offset item2 in list2)
				{
					if (item2.OnceOnly)
					{
						list.Add(item2);
					}
					int[]? array = null;
					if (item2.Write)
					{
						array = [2, item2.Address, item2.DataLength];
						num9 = array.Length * 4;
						num5 += 1.0;
						num2 += item2.DataLength;
					}
					else if (!item2.WriteOnly)
					{
						array = [1, item2.Address, item2.DataLength, num6];
						num9 = array.Length * 4;
						num4 += 1.0;
						num += item2.DataLength;
						long fileAddress = num8 + num9;
						item2.FileAddress = fileAddress;
					}
					if (array != null)
					{
						if (num8 - num7 + num9 + item2.DataLength + 4 > MaximumDataSize)
						{
							throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_SIZE, "The amount of data requested exceeded the maximum allowed in one Process().");
						}
						byte[] array2 = new byte[array[2]];
						if (item2.Write)
						{
							array2 = item2.DataValue;
							if (array2.Length != item2.DataLength)
							{
								throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_WRITE_OVERFLOW, "Offset " + item2.Address.ToString("X") + " is declared with length of " + item2.DataLength + " Bytes. The data you are trying to write is different from this. (" + array2.Length + " bytes)");
							}
						}
						Marshal.Copy(array, 0, new IntPtr(num8), array.Length);
						num8 += num9;
						Marshal.Copy(array2, 0, new IntPtr(num8), array2.Length);
						num8 += array2.Length;
					}
					num10++;
				}
				Marshal.WriteInt32(new IntPtr(num8), 0);
				num3 = num8 - num7;
				IntPtr result = IntPtr.Zero;
				int num11 = 0;

				Stopwatch stopwatch2 = new();
				stopwatch2.Start();

				while (Win32.SendMessageTimeout(connectionInfo.hWnd, connectionInfo.messageID, connectionInfo.atomFileName, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_BLOCK, 2000u, out result) == IntPtr.Zero && num11 < 10)
				{
					num11++;
					Thread.Sleep(50);
				}

				stopwatch2.Stop();

				double timeForIPC = stopwatch2.ElapsedMilliseconds;

				if (num11 >= 10)
				{
					Close(ClassInstance);
					if (Marshal.GetLastWin32Error() == 0)
					{
						throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_TIMEOUT, "SendMessage timed-out.  Tried 10 times.");
					}
					throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_SENDMSG, "Error sending message to FSUIPC.");
				}
				num7 = connectionInfo.pView.ToInt64();
				num8 = num7;
				num10 = 0;
				foreach (Offset item3 in list2)
				{
					if (!item3.Write && !item3.WriteOnly)
					{
						byte[] array2 = new byte[item3.DataLength];
						Marshal.Copy(new IntPtr(item3.FileAddress), array2, 0, array2.Length);
						item3.DataValue = array2;
					}
					item3.CheckForChanges();
					item3.lastReadValue.CopyTo(item3.previousValue, 0);
					item3.dataValue.CopyTo(item3.lastReadValue, 0);
					item3.Write = false;
					num10++;
				}
				foreach (Offset item4 in list)
				{
					item4.OnceOnly = false;
					RemoveOffset(item4);
				}


				return;

			}
			if (ClassInstance == 0)
			{
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to FSUIPC is not open.");
			}
			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to class instance " + ClassInstance.ToString("D2") + " of WideClient.exe is not open.");
		}
	}




 

	public static double ReadLVar(string LVAR)
	{

		byte ClassInstance = 0;

		lock (dataGroups)
		{
			if (LVAR.StartsWith("l:", StringComparison.CurrentCultureIgnoreCase))
			{
				LVAR = LVAR.Substring(2);
			}
			if (LVAR.StartsWith(":", StringComparison.CurrentCultureIgnoreCase))
			{
				LVAR = LVAR.Substring(1);
			}

			//if (!IsConnectionOpenForClass(ClassInstance))
			//{
			//	if (ClassInstance == 0)
			//	{
			//		throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to FSUIPC is not open.");
			//	}
			//	throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to class instance " + ClassInstance.ToString("D2") + " of WideClient.exe is not open.");
			//}

			string text = ClassInstance + "~~LVARGroup~~";
			Offset<int> offset = new(text, 3436, WriteOnly: true);
			int arrayOrStringLength = (FSUIPCVersionForClass(ClassInstance).Major >= 5) ? 128 : 40;
			Offset<string> offset2 = new(text, 3440, arrayOrStringLength, WriteOnly: true);
			Offset<double> offset3 = new(text, 26360);
			offset.Value = 26360;
			offset2.Value = ":" + LVAR;
			Process(ClassInstance, text);
			DeleteGroup(text);
			return offset3.Value;
		}
	}

	public static void WriteLVar(string LVAR, double NewValue)
	{
		WriteLVar(0, LVAR, NewValue);
	}

	public static void WriteLVar(byte ClassInstance, string LVAR, double NewValue)
	{
		lock (dataGroups)
		{
			if (LVAR.ToLower().StartsWith("l:"))
			{
				LVAR = LVAR.Substring(2);
			}
			if (LVAR.ToLower().StartsWith(":"))
			{
				LVAR = LVAR.Substring(1);
			}

			if (!IsConnectionOpenForClass(ClassInstance))
			{
				if (ClassInstance == 0)
				{
					throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to FSUIPC is not open.");
				}
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to class instance " + ClassInstance.ToString("D2") + " of WideClient.exe is not open.");
			}

			string text = ClassInstance + "~~LVARGroup~~";
			Offset<double> offset = new(text, 26360, WriteOnly: true);
			Offset<int> offset2 = new(text, 3436, WriteOnly: true);
			int arrayOrStringLength = ((FSUIPCVersionForClass(ClassInstance).Major >= 5) ? 128 : 40);
			Offset<string> offset3 = new(text, 3440, arrayOrStringLength, WriteOnly: true);
			offset.Value = NewValue;
			offset2.Value = 26360;
			offset3.Value = "::" + LVAR;
			Process(ClassInstance, text);
			DeleteGroup(text);
			return;
		}
	}




	//public static void SendControlToFS(byte ClassInstance, int ControlNumber, int ParameterValue)
	//{
	//	lock (dataGroups)
	//	{
	//		if (IsConnectionOpenForClass(ClassInstance))
	//		{
	//			string text = ClassInstance + "~~SendControl~~";
	//			Offset<int> offset = new Offset<int>(text, 12564, WriteOnly: true);
	//			new Offset<int>(text, 12560, WriteOnly: true).Value = ControlNumber;
	//			offset.Value = ParameterValue;
	//			Process(ClassInstance, text);
	//			DeleteGroup(text);
	//			return;
	//		}
	//		if (ClassInstance == 0)
	//		{
	//			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to FSUIPC is not open.");
	//		}
	//		throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to class instance " + ClassInstance.ToString("D2") + " of WideClient.exe is not open.");
	//	}
	//}


}
