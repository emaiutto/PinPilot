using System.Text;
 

namespace FSUIPC;

public class PMDG_NGX_CDU_Screen
{
	private int offset;

	private readonly int CDU_COLUMNS = 24;

	private readonly int CDU_ROWS = 14;

	private static int ID;

	private object lockObject = new object();

	private string groupName;

	public bool Powered { get; private set; }

	public PMDG_NGX_CDU_Row[] Rows { get; private set; }

	public PMDG_NGX_CDU_Screen(int Offset)
	{
		ID++;
		groupName = "~~~ PMDG CDU " + ID + " ~~~";
		offset = Offset;
		Rows = new PMDG_NGX_CDU_Row[CDU_ROWS];
		for (int i = 0; i < CDU_ROWS; i++)
		{
			Rows[i] = new PMDG_NGX_CDU_Row(CDU_COLUMNS);
		}
	}

	public void RefreshData()
	{
		RefreshData(0);
	}

	public void RefreshData(byte ClassInstance)
	{
		lock (lockObject)
		{
			if (FSUIPCConnection.IsConnectionOpenForClass(ClassInstance))
			{
				string dataGroupName = ClassInstance + groupName;
				Offset<byte[]> offset = new Offset<byte[]>(dataGroupName, this.offset, 1024);
				FSUIPCConnection.Process(dataGroupName);
				byte[] value = offset.Value;
				Powered = value[1008] > 0;
				int num = 0;
				for (int i = 0; i < CDU_COLUMNS; i++)
				{
					for (int j = 0; j < CDU_ROWS; j++)
					{
						PMDG_NGX_CDU_Cell pMDG_NGX_CDU_Cell = new PMDG_NGX_CDU_Cell
						{
							Symbol = (char)value[num],
							Color = (PMDG_NGX_CDU_COLOR)value[num + 1],
							Flags = (PMDG_NGX_CDU_FLAG)value[num + 2]
						};
						Rows[j].Cells[i] = pMDG_NGX_CDU_Cell;
						num += 3;
					}
				}
				//FSUIPCConnection.DeleteGroup(dataGroupName);
				return;
			}
			if (ClassInstance == 0)
			{
				throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to FSUIPC is not open.");
			}
			throw new FSUIPCException(FSUIPCError.FSUIPC_ERR_NOTOPEN, "The connection to class instance " + ClassInstance.ToString("D2") + " of WideClient.exe is not open.");
		}
	}

	public override string ToString()
	{
		return ToString("");
	}

	public string ToString(string RowDelimiter)
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < CDU_ROWS; i++)
		{
			stringBuilder.Append(Rows[i].ToString());
			stringBuilder.Append(RowDelimiter);
		}
		return stringBuilder.ToString();
	}
}
