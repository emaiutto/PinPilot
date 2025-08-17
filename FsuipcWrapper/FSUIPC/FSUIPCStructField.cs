using System;
using System.Collections;
using System.Reflection;

namespace FSUIPC;

public sealed class FSUIPCStructField<T> : IStructField
{
	internal int dataLength;

	internal fsuipcDataType dataType;

	internal T dataValue;

	internal T oldValue;

	internal bool write;

	internal bool isFixedLengthString;

	int IStructField.DataLength => dataLength;

	fsuipcDataType IStructField.DataType => dataType;

	bool IStructField.Write
	{
		get
		{
			return write;
		}
		set
		{
			write = value;
		}
	}

	object IStructField.Value
	{
		get
		{
			return dataValue;
		}
		set
		{
			dataValue = (T)value;
		}
	}

	object IStructField.OldValue
	{
		get
		{
			return oldValue;
		}
		set
		{
			oldValue = (T)value;
		}
	}

	bool IStructField.IsFixedLengthString => isFixedLengthString;

	public T Value
	{
		get
		{
			return dataValue;
		}
		set
		{
			dataValue = value;
			write = true;
		}
	}

	public FSUIPCStructField()
		: this(0)
	{
	}

	public FSUIPCStructField(int ArrayOrStringLength)
	{
		initDataInfo(ArrayOrStringLength);
	}

	private void initDataInfo(int length)
	{
		dataType = fsuipcDataType.TypeUnknown;
		switch (typeof(T).Name)
		{
		case "Byte":
			dataType = fsuipcDataType.TypeByte;
			dataLength = 1;
			break;
		case "SByte":
			dataType = fsuipcDataType.TypeSByte;
			dataLength = 1;
			break;
		case "Int16":
			dataType = fsuipcDataType.TypeInt16;
			dataLength = 2;
			break;
		case "Int32":
			dataType = fsuipcDataType.TypeInt32;
			dataLength = 4;
			break;
		case "Int64":
			dataType = fsuipcDataType.TypeInt64;
			dataLength = 8;
			break;
		case "UInt16":
			dataType = fsuipcDataType.TypeUInt16;
			dataLength = 2;
			break;
		case "UInt32":
			dataType = fsuipcDataType.TypeUInt32;
			dataLength = 4;
			break;
		case "UInt64":
			dataType = fsuipcDataType.TypeUInt64;
			dataLength = 8;
			break;
		case "Double":
			dataType = fsuipcDataType.TypeDouble;
			dataLength = 8;
			break;
		case "Single":
			dataType = fsuipcDataType.TypeFloat;
			dataLength = 4;
			break;
		case "Byte[]":
			dataType = fsuipcDataType.TypeByteArray;
			dataValue = (T)(object)new byte[length];
			oldValue = (T)(object)new byte[length];
			dataLength = Math.Abs(length);
			break;
		case "String":
			dataType = fsuipcDataType.TypeString;
			dataLength = Math.Abs(length);
			isFixedLengthString = length < 0;
			break;
		case "BitArray":
			dataType = fsuipcDataType.TypeBitArray;
			dataLength = Math.Abs(length);
			dataValue = (T)(object)new BitArray(length * 8);
			oldValue = (T)(object)new BitArray(length * 8);
			break;
		case "FsBitArray":
			dataType = fsuipcDataType.TypeFsBitArray;
			dataLength = Math.Abs(length);
			dataValue = (T)(object)new FsBitArray(length * 8);
			oldValue = (T)(object)new FsBitArray(length * 8);
			break;
		default:
		{
			Type typeFromHandle = typeof(T);
			if (typeFromHandle.IsSubclassOf(typeof(FSUIPCStruct)))
			{
				dataType = fsuipcDataType.TypeStruct;
				Assembly executingAssembly = Assembly.GetExecutingAssembly();
				try
				{
					dataValue = (T)executingAssembly.CreateInstance(typeFromHandle.FullName);
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				dataLength = (dataValue as FSUIPCStruct).getStuctLength();
				break;
			}
			throw new Exception("Offsets of type " + typeof(T).Name.ToString() + " are not supported.  You can only use classed derived from FSUIPCStruct or one of the following types: Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, Double, Byte[], String, BitArray, FsBitArray.");
		}
		}
		if (dataType == fsuipcDataType.TypeByteArray || dataType == fsuipcDataType.TypeString || dataType == fsuipcDataType.TypeBitArray || dataType == fsuipcDataType.TypeFsBitArray)
		{
			if (length == 0)
			{
				throw new Exception("Must set a size for fields in FSUIPCStucts for Byte[], BitArray, FsBitArray or String");
			}
		}
		else if (length != 0)
		{
			throw new Exception("Cannot specify a size for datatypes other than Byte[], BitArray, FsBitArray or String");
		}
	}
}
