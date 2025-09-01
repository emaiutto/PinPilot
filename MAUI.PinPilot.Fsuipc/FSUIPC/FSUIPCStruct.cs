using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FSUIPC;

public abstract class FSUIPCStruct
{
	//internal bool write
	//{
	//	get
	//	{
	//		bool flag = false;
	//		FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
	//		foreach (FieldInfo fieldInfo in fields)
	//		{
	//			List<IStructField> list = new List<IStructField>();
	//			if (fieldInfo.FieldType.GetInterface("IStructFieldArray") != null)
	//			{
	//				list.AddRange(((IStructFieldArray)fieldInfo.GetValue(this)).fields);
	//			}
	//			else if (fieldInfo.FieldType.GetInterface("IStructField") != null)
	//			{
	//				list.Add((IStructField)fieldInfo.GetValue(this));
	//			}
	//			foreach (IStructField item in list)
	//			{
	//				switch (item.DataType)
	//				{
	//				case fsuipcDataType.TypeBitArray:
	//				{
	//					BitArray bitArray = (BitArray)item.Value;
	//					if (bitArray != null)
	//					{
	//						BitArray bitArray2 = (BitArray)item.OldValue;
	//						bool flag3 = true;
	//						for (int k = 0; k < bitArray2.Length; k++)
	//						{
	//							flag3 &= bitArray2[k] == bitArray[k];
	//						}
	//						flag = flag || !flag3;
	//					}
	//					break;
	//				}
	//				case fsuipcDataType.TypeFsBitArray:
	//				{
	//					FsBitArray fsBitArray = (FsBitArray)item.Value;
	//					if (fsBitArray != null)
	//					{
	//						FsBitArray fsBitArray2 = (FsBitArray)item.OldValue;
	//						bool flag4 = true;
	//						for (int l = 0; l < fsBitArray2.Length; l++)
	//						{
	//							flag4 &= fsBitArray2[l] == fsBitArray[l];
	//						}
	//						flag = flag || !flag4;
	//					}
	//					break;
	//				}
	//				case fsuipcDataType.TypeByteArray:
	//				{
	//					byte[] array = (byte[])item.Value;
	//					if (array != null)
	//					{
	//						byte[] array2 = (byte[])item.OldValue;
	//						bool flag2 = true;
	//						for (int j = 0; j < array2.Length; j++)
	//						{
	//							flag2 &= array2[j] == array[j];
	//						}
	//						flag = flag || !flag2;
	//					}
	//					break;
	//				}
	//				default:
	//					flag |= item.Write;
	//					break;
	//				}
	//			}
	//		}
	//		return flag;
	//	}
	//}

	internal int getStuctLength()
	{
		int num = 0;
		FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		foreach (FieldInfo fieldInfo in fields)
		{
			List<IStructField> list = new List<IStructField>();
			if (fieldInfo.FieldType.GetInterface("IStructFieldArray") != null)
			{
				list.AddRange(((IStructFieldArray)fieldInfo.GetValue(this)).fields);
			}
			else if (fieldInfo.FieldType.GetInterface("IStructField") != null)
			{
				list.Add((IStructField)fieldInfo.GetValue(this));
			}
			foreach (IStructField item in list)
			{
				num += item.DataLength;
			}
		}
		return num;
	}

	internal void copyTo(FSUIPCStruct DestinationStruct)
	{
		FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		foreach (FieldInfo fieldInfo in fields)
		{
			List<IStructField> list = new List<IStructField>();
			if (fieldInfo.FieldType.GetInterface("IStructFieldArray") != null)
			{
				list.AddRange(((IStructFieldArray)fieldInfo.GetValue(this)).fields);
			}
			else if (fieldInfo.FieldType.GetInterface("IStructField") != null)
			{
				list.Add((IStructField)fieldInfo.GetValue(this));
			}
			foreach (IStructField item in list)
			{
				switch (item.DataType)
				{
				case fsuipcDataType.TypeByteArray:
					((byte[])item.Value).CopyTo((byte[])item.OldValue, 0);
					break;
				case fsuipcDataType.TypeBitArray:
				{
					BitArray bits2 = (BitArray)item.Value;
					item.OldValue = new BitArray(bits2);
					break;
				}
				case fsuipcDataType.TypeFsBitArray:
				{
					FsBitArray bits = (FsBitArray)item.Value;
					item.OldValue = new FsBitArray(bits);
					break;
				}
				case fsuipcDataType.TypeStruct:
					((FSUIPCStruct)item.Value).copyTo((FSUIPCStruct)item.OldValue);
					break;
				default:
					item.OldValue = item.Value;
					break;
				}
			}
		}
	}

	internal void toByteArray(byte[] datablock)
	{
		int num = 0;
		FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		foreach (FieldInfo fieldInfo in fields)
		{
			List<IStructField> list = new List<IStructField>();
			if (fieldInfo.FieldType.GetInterface("IStructFieldArray") != null)
			{
				list.AddRange(((IStructFieldArray)fieldInfo.GetValue(this)).fields);
			}
			else if (fieldInfo.FieldType.GetInterface("IStructField") != null)
			{
				list.Add((IStructField)fieldInfo.GetValue(this));
			}
			foreach (IStructField item in list)
			{
				string name = fieldInfo.Name;
				int dataLength = item.DataLength;
				byte[] array = new byte[dataLength];
				switch (item.DataType)
				{
				case fsuipcDataType.TypeByte:
					array[0] = (byte)item.Value;
					break;
				case fsuipcDataType.TypeSByte:
					array[0] = (byte)item.Value;
					break;
				case fsuipcDataType.TypeInt16:
					array = BitConverter.GetBytes((short)item.Value);
					break;
				case fsuipcDataType.TypeInt32:
					array = BitConverter.GetBytes((int)item.Value);
					break;
				case fsuipcDataType.TypeInt64:
					array = BitConverter.GetBytes((long)item.Value);
					break;
				case fsuipcDataType.TypeUInt16:
					array = BitConverter.GetBytes((ushort)item.Value);
					break;
				case fsuipcDataType.TypeUInt32:
					array = BitConverter.GetBytes((uint)item.Value);
					break;
				case fsuipcDataType.TypeUInt64:
					array = BitConverter.GetBytes((ulong)item.Value);
					break;
				case fsuipcDataType.TypeDouble:
					array = BitConverter.GetBytes((double)item.Value);
					break;
				case fsuipcDataType.TypeFloat:
					array = BitConverter.GetBytes((float)item.Value);
					break;
				case fsuipcDataType.TypeByteArray:
					array = (byte[])item.Value;
					if (array.Length != dataLength)
					{
						throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_WRITE_OVERFLOW, "Field " + name + " is a ByteArray with a declared length of " + dataLength + " Bytes. The array you are trying to write is different from this. (" + datablock.Length + " bytes)");
					}
					array.CopyTo((byte[])item.OldValue, 0);
					break;
				case fsuipcDataType.TypeBitArray:
				{
					BitArray bitArray = (BitArray)item.Value;
					if (bitArray.Length != dataLength * 8)
					{
						throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_WRITE_OVERFLOW, "Field " + name + " is a BitArray with a declared length of " + dataLength + " Bytes. The BitArray you are trying to write is different from this. (" + bitArray.Length + " bits)");
					}
					item.OldValue = new BitArray(bitArray);
					bitArray.CopyTo(array, 0);
					break;
				}
				case fsuipcDataType.TypeFsBitArray:
				{
					FsBitArray fsBitArray = (FsBitArray)item.Value;
					if (fsBitArray.Length != dataLength * 8)
					{
						throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_WRITE_OVERFLOW, "Field " + name + " is an FsBitArray with a declared length of " + dataLength + " Bytes. The FsBitArray you are trying to write is different from this. (" + fsBitArray.Length + " bits)");
					}
					item.OldValue = new FsBitArray(fsBitArray);
					fsBitArray.CopyTo(array, 0);
					break;
				}
				case fsuipcDataType.TypeString:
				{
					string text = (string)item.Value;
					int num2 = dataLength - ((!item.IsFixedLengthString) ? 1 : 0);
					if (text.Length > num2)
					{
						text = text.Substring(0, num2);
					}
					array = new byte[dataLength];
					Encoding.ASCII.GetBytes(text).CopyTo(array, 0);
					break;
				}
				case fsuipcDataType.TypeStruct:
					((FSUIPCStruct)item.Value).toByteArray(array);
					break;
				}
				array.CopyTo(datablock, num);
				num += dataLength;
			}
		}
	}

	internal void fromByteArray(byte[] datablock)
	{
		int num = 0;
		FieldInfo[] fields = GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		foreach (FieldInfo fieldInfo in fields)
		{
			List<IStructField> list = new List<IStructField>();
			if (fieldInfo.FieldType.GetInterface("IStructFieldArray") != null)
			{
				list.AddRange(((IStructFieldArray)fieldInfo.GetValue(this)).fields);
			}
			else if (fieldInfo.FieldType.GetInterface("IStructField") != null)
			{
				list.Add((IStructField)fieldInfo.GetValue(this));
			}
			foreach (IStructField item in list)
			{
				_ = fieldInfo.Name;
				int dataLength = item.DataLength;
				byte[] array = new byte[dataLength];
				Array.Copy(datablock, num, array, 0, dataLength);
				switch (item.DataType)
				{
				case fsuipcDataType.TypeByte:
					item.Value = array[0];
					break;
				case fsuipcDataType.TypeSByte:
					item.Value = (sbyte)array[0];
					break;
				case fsuipcDataType.TypeInt16:
					item.Value = BitConverter.ToInt16(array, 0);
					break;
				case fsuipcDataType.TypeInt32:
					item.Value = BitConverter.ToInt32(array, 0);
					break;
				case fsuipcDataType.TypeInt64:
					item.Value = BitConverter.ToInt64(array, 0);
					break;
				case fsuipcDataType.TypeUInt16:
					item.Value = BitConverter.ToUInt16(array, 0);
					break;
				case fsuipcDataType.TypeUInt32:
					item.Value = BitConverter.ToUInt32(array, 0);
					break;
				case fsuipcDataType.TypeUInt64:
					item.Value = BitConverter.ToUInt64(array, 0);
					break;
				case fsuipcDataType.TypeDouble:
					item.Value = BitConverter.ToDouble(array, 0);
					break;
				case fsuipcDataType.TypeFloat:
					item.Value = BitConverter.ToSingle(array, 0);
					break;
				case fsuipcDataType.TypeByteArray:
					item.Value = array;
					item.OldValue = array;
					break;
				case fsuipcDataType.TypeBitArray:
					item.Value = new BitArray(array);
					item.OldValue = new BitArray(array);
					break;
				case fsuipcDataType.TypeFsBitArray:
					item.Value = new FsBitArray(array);
					item.OldValue = new FsBitArray(array);
					break;
				case fsuipcDataType.TypeString:
				{
					string text = Encoding.ASCII.GetString(array);
					int num2 = text.Length;
					if (!item.IsFixedLengthString)
					{
						num2 = text.IndexOf('\0');
					}
					if (num2 > 0)
					{
						item.Value = text.Substring(0, num2);
					}
					else if (num2 == -1)
					{
						item.Value = text;
					}
					else
					{
						item.Value = "";
					}
					break;
				}
				case fsuipcDataType.TypeStruct:
					(item.Value as FSUIPCStruct).fromByteArray(array);
					break;
				}
				item.Write = false;
				num += dataLength;
			}
		}
	}
}
