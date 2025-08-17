using System;

namespace FSUIPC;

[Flags]
internal enum PageProtection : uint
{
	NoAccess = 1u,
	Readonly = 2u,
	ReadWrite = 4u,
	WriteCopy = 8u,
	Execute = 0x10u,
	ExecuteRead = 0x20u,
	ExecuteReadWrite = 0x40u,
	ExecuteWriteCopy = 0x80u,
	Guard = 0x100u,
	NoCache = 0x200u,
	WriteCombine = 0x400u
}
