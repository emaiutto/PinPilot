using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace FSUIPC;

public class Offset
{
	public Guid ID => id;

	internal Guid id;

	internal long fileAddress;

	internal bool write;

	internal bool connected;

	internal string group;

	internal bool writeOnly;

	internal int dataLength;

	internal byte[] dataValue;

	internal byte[] lastReadValue;

	internal byte[] previousValue;

	internal bool valueChanged;

	internal OffsetAction actionAtNextProcess;

	internal bool isFixedLengthString;


	public int Address { get; set; }


 

	public OffsetAction ActionAtNextProcess
	{
		get
		{
			return actionAtNextProcess;
		}
		set
		{
			if (writeOnly && value == OffsetAction.Read)
			{
				throw new Exception("Cannot set WriteOnly offset to READ on next action.");
			}
			actionAtNextProcess = value;
		}
	}


	internal virtual byte[] DataValue { get; set; }

 

	public bool IsConnected => connected;

	public bool WriteOnly
	{
		get
		{
			return writeOnly;
		}
		set
		{
			writeOnly = value;
			if (value)
			{
				actionAtNextProcess = OffsetAction.Write;
			}
		}
	}


 

	public bool ValueChanged => valueChanged;

	public bool IsFixedLengthString { get; set; }

	internal bool Connected { get; set; }

	internal bool Write { get; set; }

	internal long FileAddress { get; set; }

	public int DataLength => dataLength;

	internal string Group => group;



	public Offset() { }

	public Offset(int Address, int Length)
		: this("", Address, Length, WriteOnly: false)
	{
	}

	public Offset(int Address, int Length, bool WriteOnly)
		: this("", Address, Length, WriteOnly)
	{
	}

	public Offset(string DataGroupName, int Address, int Length)
		: this(DataGroupName, Address, Length, WriteOnly: false)
	{
	}

	public Offset(string DataGroupName, int Address, int Length, bool WriteOnly)
	{
		InitDataInfo(DataGroupName, Address, Length, WriteOnly);
	}


	internal void InitDataInfo(string DataGroupName, int address, int length, bool WriteOnly)
	{
		group = DataGroupName;

		Address = address;

		writeOnly = WriteOnly;
		dataLength = length;

		dataValue = new byte[dataLength];

		lastReadValue = new byte[dataLength];
		previousValue = new byte[dataLength];

		if (length == 0) throw new Exception("Size cannot be 0 bytes.");

		isFixedLengthString = length < 0;
		

		id = Guid.NewGuid();

		if (this.WriteOnly) actionAtNextProcess = OffsetAction.Write;

		connected = true;

		FSUIPCConnection.AddOffset(this);
	}

	public T GetValue<T>() => (T)GetValue(typeof(T));

	 


	public object GetValue(Type asType)
	{
		var buffer = dataValue; // cache para evitar tocar el campo varias veces

		return asType switch
		{
			Type t when t == typeof(byte) => buffer[0],
			Type t when t == typeof(sbyte) => (sbyte)buffer[0],
			Type t when t == typeof(short) => BitConverter.ToInt16(buffer, 0),
			Type t when t == typeof(int) => BitConverter.ToInt32(buffer, 0),
			Type t when t == typeof(long) => BitConverter.ToInt64(buffer, 0),
			Type t when t == typeof(ushort) => BitConverter.ToUInt16(buffer, 0),
			Type t when t == typeof(uint) => BitConverter.ToUInt32(buffer, 0),
			Type t when t == typeof(ulong) => BitConverter.ToUInt64(buffer, 0),
			Type t when t == typeof(double) => BitConverter.ToDouble(buffer, 0),
			Type t when t == typeof(float) => BitConverter.ToSingle(buffer, 0),
			Type t when t == typeof(byte[]) => buffer,
			Type t when t == typeof(BitArray) => new BitArray(buffer),
			Type t when t == typeof(FsBitArray) => CreateFsBitArray(buffer),
			Type t when t == typeof(string) => GetString(buffer),
			Type t when t.IsSubclassOf(typeof(FSUIPCStruct))
				=> CreateFSUIPCStruct(t, buffer),
			_ => throw new FSUIPCException(
					FSUIPCError.FSUIPC_TYPE_NOT_SUPPORTED,
					$"Type {asType.Name} is not supported. " +
					"Valid types: Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, Double, Single, Byte[], String, BitArray, FsBitArray or subclasses of FSUIPCStruct."
				 )
		};
	}

	private FsBitArray CreateFsBitArray(byte[] buffer)
	{
		var fsBitArray = new FsBitArray(buffer);
		fsBitArray.compare(new BitArray(previousValue)); 
		return fsBitArray;
	}

	private string GetString(byte[] buffer)
	{
		var text = Encoding.Latin1.GetString(buffer);
		if (!IsFixedLengthString)
		{
			int idx = text.IndexOf('\0');
			return idx > -1 ? text[..idx] : text;
		}
		return text;
	}

	private static FSUIPCStruct CreateFSUIPCStruct(Type t, byte[] buffer)
	{
		var instance = (FSUIPCStruct)Activator.CreateInstance(t)!;
		instance.fromByteArray(buffer);
		return instance;
	}





	public virtual void SetValue(object NewValue)
	{
		Type type = NewValue.GetType();

		if (type == typeof(byte))
		{
			dataValue[0] = (byte)NewValue;
		}
		else if (type == typeof(sbyte))
		{
			dataValue[0] = (byte)(sbyte)NewValue;
		}
		else if (type == typeof(short))
		{
			dataValue = BitConverter.GetBytes((short)NewValue);
		}
		else if (type == typeof(int))
		{
			dataValue = BitConverter.GetBytes((int)NewValue);
		}
		else if (type == typeof(long))
		{
			dataValue = BitConverter.GetBytes((long)NewValue);
		}
		else if (type == typeof(ushort))
		{
			dataValue = BitConverter.GetBytes((ushort)NewValue);
		}
		else if (type == typeof(uint))
		{
			dataValue = BitConverter.GetBytes((uint)NewValue);
		}
		else if (type == typeof(ulong))
		{
			dataValue = BitConverter.GetBytes((ulong)NewValue);
		}
		else if (type == typeof(double))
		{
			dataValue = BitConverter.GetBytes((double)NewValue);
		}
		else if (type == typeof(float))
		{
			dataValue = BitConverter.GetBytes((float)NewValue);
		}
		else if (type == typeof(byte[]))
		{
			dataValue = (byte[])NewValue;
			if (dataValue.Length != DataLength)
			{
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_WRITE_OVERFLOW, "Offset " + Address.ToString("X") + " is a ByteArray with a declared length of " + DataLength + " Bytes. The array you are trying to write is different from this. (" + dataValue.Length + " bytes)");
			}
		}
		else if (type == typeof(BitArray))
		{
			BitArray bitArray = (BitArray)NewValue;
			if (bitArray.Length != DataLength * 8)
			{
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_WRITE_OVERFLOW, "Offset " + Address.ToString("X") + " is a BitArray with a declared length of " + DataLength + " Bytes. The BitArray you are trying to write is different from this. (" + bitArray.Length + " bits)");
			}
			bitArray.CopyTo(dataValue, 0);
		}
		else if (type == typeof(FsBitArray))
		{
			FsBitArray fsBitArray = (FsBitArray)NewValue;
			if (fsBitArray.Length != DataLength * 8)
			{
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_WRITE_OVERFLOW, "Offset " + Address.ToString("X") + " is an FsBitArray with a declared length of " + DataLength + " Bytes. The FsBitArray you are trying to write is different from this. (" + fsBitArray.Length + " bits)");
			}
			fsBitArray.CopyTo(dataValue, 0);
		}
		else if (type == typeof(string))
		{
			string text = (string)NewValue;
			int num = DataLength - ((!IsFixedLengthString) ? 1 : 0);
			if (text.Length > num)
			{
				text = text.Substring(0, num);
			}
			dataValue = new byte[DataLength];
			Encoding.ASCII.GetBytes(text).CopyTo(dataValue, 0);
		}
		else
		{
			if (!type.IsSubclassOf(typeof(FSUIPCStruct)))
			{
				throw new FSUIPCException(FSUIPCError.FSUIPC_TYPE_NOT_SUPPORTED, "Type " + type.Name.ToString() + " is not supported as a value for an Offset.\nYou can only use data deirved from FSUIPCStruct or one of the following types: Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, Double, Byte[], String, BitArray, FsLongitue, FsLatitude.");
			}
			((FSUIPCStruct)NewValue).toByteArray(dataValue);
		}
	}

	

	internal virtual void CheckForChanges()
	{
		byte[] array = DataValue;
		bool flag = true;
		for (int i = 0; i < lastReadValue.Length; i++)
		{
			flag &= lastReadValue[i] == array[i];
		}
		valueChanged = !flag;
	}

	public override string ToString() => "Offset 0x" + Address.ToString("X") + " (" + dataLength + " Bytes)";

}

public class Offset<T> : Offset
{
	private T valueObject;

	
	public T Value
	{
		get => valueObject ??= GetValue<T>();
		set
		{
			valueObject = value;
			base.SetValue(valueObject);
		}
	}


	internal override byte[] DataValue
	{
		get
		{
			if (valueObject != null)
				base.SetValue(valueObject);
			return dataValue;
		}
		set
		{
			dataValue = value;
			valueObject = GetValue<T>();
			base.SetValue(valueObject);
		}
	}



	public Offset(int Address)
		: this("", Address, 0, WriteOnly: false)
	{
	}

	public Offset(string DataGroupName, int Address)
		: this(DataGroupName, Address, 0, WriteOnly: false)
	{
	}

	public Offset(int Address, int ArrayOrStringLength)
		: this("", Address, ArrayOrStringLength, WriteOnly: false)
	{
	}

	public Offset(int Address, bool WriteOnly)
		: this("", Address, 0, WriteOnly)
	{
	}

	public Offset(string DataGroupName, int Address, bool WriteOnly)
		: this(DataGroupName, Address, 0, WriteOnly)
	{
	}

	public Offset(int Address, int ArrayOrStringLength, bool WriteOnly)
		: this("", Address, ArrayOrStringLength, WriteOnly)
	{
	}

	public Offset(string DataGroupName, int Address, int ArrayOrStringLength)
		: this(DataGroupName, Address, ArrayOrStringLength, WriteOnly: false)
	{
	}

	public Offset(string DataGroupName, int Address, int ArrayOrStringLength, bool WriteOnly)
	{
		int num;
		fsuipcDataType fsuipcDataType2;
		switch (typeof(T).Name)
		{
			case "Byte":
				fsuipcDataType2 = fsuipcDataType.TypeByte;
				num = 1;
				break;
			case "SByte":
				fsuipcDataType2 = fsuipcDataType.TypeSByte;
				num = 1;
				break;
			case "Int16":
				fsuipcDataType2 = fsuipcDataType.TypeInt16;
				num = 2;
				break;
			case "Int32":
				fsuipcDataType2 = fsuipcDataType.TypeInt32;
				num = 4;
				break;
			case "Int64":
				fsuipcDataType2 = fsuipcDataType.TypeInt64;
				num = 8;
				break;
			case "UInt16":
				fsuipcDataType2 = fsuipcDataType.TypeUInt16;
				num = 2;
				break;
			case "UInt32":
				fsuipcDataType2 = fsuipcDataType.TypeUInt32;
				num = 4;
				break;
			case "UInt64":
				fsuipcDataType2 = fsuipcDataType.TypeUInt64;
				num = 8;
				break;
			case "Double":
				fsuipcDataType2 = fsuipcDataType.TypeDouble;
				num = 8;
				break;
			case "Single":
				fsuipcDataType2 = fsuipcDataType.TypeFloat;
				num = 4;
				break;
			case "Byte[]":
				fsuipcDataType2 = fsuipcDataType.TypeByteArray;
				num = ArrayOrStringLength;
				break;
			case "String":
				fsuipcDataType2 = fsuipcDataType.TypeString;
				num = ArrayOrStringLength;
				break;
			case "BitArray":
				fsuipcDataType2 = fsuipcDataType.TypeBitArray;
				num = ArrayOrStringLength;
				break;
			case "FsBitArray":
				fsuipcDataType2 = fsuipcDataType.TypeFsBitArray;
				num = ArrayOrStringLength;
				break;

			default:
				{
					Type typeFromHandle = typeof(T);
					if (typeFromHandle.IsSubclassOf(typeof(FSUIPCStruct)))
					{
						fsuipcDataType2 = fsuipcDataType.TypeStruct;

						Assembly executingAssembly = Assembly.GetExecutingAssembly();
						object? obj;
						try
						{
							obj = (T)executingAssembly.CreateInstance(typeFromHandle.FullName);
						}
						catch (TargetInvocationException ex)
						{
							throw ex.InnerException;
						}
						if ((object)null == null)
						{
							Assembly entryAssembly = Assembly.GetEntryAssembly();
							try
							{
								obj = (T)entryAssembly.CreateInstance(typeFromHandle.FullName);
							}
							catch (TargetInvocationException ex2)
							{
								throw ex2.InnerException;
							}
						}
						num = ((object)null as FSUIPCStruct).getStuctLength();
						break;
					}
					throw new Exception("Offsets of type " + typeof(T).Name.ToString() + " are not supported.  You can only use classes derived from FSUIPCStruct or one of the following types: Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, Double, Byte[], String, BitArray, FsLongitude, FsLatitude.");
				}
		}
		switch (fsuipcDataType2)
		{
		case fsuipcDataType.TypeByteArray:
		case fsuipcDataType.TypeString:
		case fsuipcDataType.TypeBitArray:
		case fsuipcDataType.TypeFsBitArray:
			if (ArrayOrStringLength == 0)
			{
				throw new Exception("Must set a size set for ArrayOrStringLength for Byte[], BitArray, FsBitArray or String");
			}
			break;
		//case fsuipcDataType.TypeFsLatitude:
		//case fsuipcDataType.TypeFsLongitude:
		//	if (ArrayOrStringLength != 4 && ArrayOrStringLength != 8)
		//	{
		//		new Exception("Must set a size for FsLongitude or FsLatitude. Can only be 4 or 8 Bytes.");
		//	}
		//	break;
		default:
			if (ArrayOrStringLength != 0)
			{
				throw new Exception("Cannot specify an ArrayOrStringLength for datatypes other than Byte[], BitArray, FsLatitude, FsLongitude or String");
			}
			break;
		}
		InitDataInfo(DataGroupName, Address, num, WriteOnly);
	}

	public override void SetValue(object NewValue)
	{
		valueObject = (T)NewValue;
		base.SetValue(NewValue);
	}

	internal override void CheckForChanges()
	{
		if (typeof(T).Name == "FsBitArray")
		{
			(valueObject as FsBitArray).compare(new BitArray(lastReadValue));
		}
		base.CheckForChanges();
	}

	public override string ToString() => "Offset 0x" + Address.ToString("X") + " (" + typeof(T).Name + ": " + dataLength + " Bytes)";

}
