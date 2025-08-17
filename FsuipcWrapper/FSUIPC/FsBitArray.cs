using System;
using System.Collections;

namespace FSUIPC;

public class FsBitArray : ICollection, IEnumerable, ICloneable
{
	private BitArray bitArray;

	private bool[] changed;

	public BitArray BitArray => bitArray;

	public bool[] Changed => changed;

	public bool this[int index]
	{
		get
		{
			return bitArray[index];
		}
		set
		{
			bitArray[index] = value;
		}
	}

	public object SyncRoot => bitArray.SyncRoot;

	public int Count => bitArray.Count;

	public int Length
	{
		get
		{
			return bitArray.Length;
		}
		set
		{
			bitArray.Length = value;
			changed = new bool[bitArray.Length];
		}
	}

	public bool IsSynchronized => bitArray.IsSynchronized;

	public bool IsReadOnly => bitArray.IsReadOnly;

	public bool HasChanged(int Index)
	{
		return changed[Index];
	}

	public FsBitArray(int length)
	{
		bitArray = new BitArray(length);
		changed = new bool[bitArray.Length];
	}

	public FsBitArray(byte[] bytes)
	{
		bitArray = new BitArray(bytes);
		changed = new bool[bitArray.Length];
	}

	public FsBitArray(bool[] values)
	{
		bitArray = new BitArray(values);
		changed = new bool[bitArray.Length];
	}

	public FsBitArray(int[] values)
	{
		bitArray = new BitArray(values);
		changed = new bool[bitArray.Length];
	}

	public FsBitArray(BitArray bits)
	{
		bitArray = new BitArray(bits);
		changed = new bool[bitArray.Length];
	}

	public FsBitArray(FsBitArray bits)
	{
		bitArray = new BitArray(bits.bitArray);
		changed = new bool[bitArray.Length];
	}

	public FsBitArray(int length, bool defaultValue)
	{
		bitArray = new BitArray(length, defaultValue);
		changed = new bool[bitArray.Length];
	}

	public FsBitArray And(FsBitArray value)
	{
		for (int i = 0; i < bitArray.Length; i++)
		{
			Set(i, Get(i) & value[i]);
		}
		return this;
	}

	public object Clone()
	{
		return new FsBitArray((BitArray)bitArray.Clone())
		{
			changed = (bool[])changed.Clone()
		};
	}

	public void CopyTo(Array array, int index)
	{
		bitArray.CopyTo(array, index);
	}

	public bool Get(int index)
	{
		return bitArray.Get(index);
	}

	public IEnumerator GetEnumerator()
	{
		return bitArray.GetEnumerator();
	}

	public FsBitArray Not()
	{
		for (int i = 0; i < bitArray.Length; i++)
		{
			Set(i, !Get(i));
		}
		return this;
	}

	public FsBitArray Or(FsBitArray value)
	{
		for (int i = 0; i < bitArray.Length; i++)
		{
			Set(i, Get(i) | value[i]);
		}
		return this;
	}

	public void Set(int index, bool value)
	{
		if (bitArray[index] != value)
		{
			changed[index] = true;
		}
		bitArray.Set(index, value);
	}

	public void SetAll(bool value)
	{
		for (int i = 0; i < bitArray.Length; i++)
		{
			Set(i, value);
		}
	}

	public FsBitArray Xor(FsBitArray value)
	{
		for (int i = 0; i < bitArray.Length; i++)
		{
			Set(i, Get(i) ^ value[i]);
		}
		return this;
	}

	internal void compare(BitArray oldValues)
	{
		for (int i = 0; i < bitArray.Length; i++)
		{
			changed[i] = bitArray[i] != oldValues[i];
		}
	}
}
