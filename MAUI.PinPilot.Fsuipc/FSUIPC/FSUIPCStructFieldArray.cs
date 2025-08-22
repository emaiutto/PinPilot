namespace FSUIPC;

public sealed class FSUIPCStructFieldArray<T> : IStructFieldArray
{
	private FSUIPCStructField<T>[] fields;

	public FSUIPCStructField<T> this[int fieldNo] => fields[fieldNo];

	IStructField[] IStructFieldArray.fields => fields;

	public FSUIPCStructFieldArray(int NumberOfItems)
	{
		fields = new FSUIPCStructField<T>[NumberOfItems];
		for (int i = 0; i < NumberOfItems; i++)
		{
			fields[i] = new FSUIPCStructField<T>();
		}
	}
}
