using System;

namespace FSUIPC;

[Flags]
internal enum DesiredAccess : uint
{
	StandardRights = 0xF0000u,
	Query = 1u,
	MapWrite = 2u,
	MapRead = 4u,
	MapExecute = 8u,
	SectionExtendSize = 0x10u
}
