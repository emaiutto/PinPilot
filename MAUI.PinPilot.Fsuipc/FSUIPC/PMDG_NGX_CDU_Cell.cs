namespace FSUIPC;

public struct PMDG_NGX_CDU_Cell
{
	public char Symbol;

	public PMDG_NGX_CDU_COLOR Color;

	public PMDG_NGX_CDU_FLAG Flags;

	public override string ToString()
	{
		return Symbol.ToString();
	}
}
