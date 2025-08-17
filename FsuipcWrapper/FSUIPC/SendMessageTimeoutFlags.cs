using System;

namespace FSUIPC;

[Flags]
internal enum SendMessageTimeoutFlags : uint
{
	SMTO_NORMAL = 0u,
	SMTO_BLOCK = 1u,
	SMTO_ABORTIFHUNG = 2u,
	SMTO_NOTIMEOUTIFNOTHUNG = 8u
}
