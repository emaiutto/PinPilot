using System;

namespace FSUIPC;

[Flags]
public enum PMDG_NGX_CDU_FLAG : byte
{
	SMALL_FONT = 1,
	REVERSE = 2,
	DIMMED = 4
}
