using System.Text;

namespace FSUIPC;

public struct PMDG_NGX_CDU_Row
{
	public PMDG_NGX_CDU_Cell[] Cells { get; private set; }

	internal PMDG_NGX_CDU_Row(int RowWidth)
	{
		Cells = new PMDG_NGX_CDU_Cell[RowWidth];
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < Cells.Length; i++)
		{
			stringBuilder.Append(Cells[i].Symbol);
		}
		return stringBuilder.ToString();
	}
}
