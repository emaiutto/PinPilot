namespace FSUIPC;

internal interface IStructField
{
	int DataLength { get; }

	fsuipcDataType DataType { get; }

	bool Write { get; set; }

	object OldValue { get; set; }

	object Value { get; set; }

	bool IsFixedLengthString { get; }
}
