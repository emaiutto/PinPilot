using System;

namespace FSUIPC;

public class LVarEvent : EventArgs
{
	public FsLVar LVar { get; private set; }

	internal LVarEvent(FsLVar LVar)
	{
		this.LVar = LVar;
	}
}
