using System;

namespace FSUIPC;

public class LogEventArgs : EventArgs
{
	public string LogEntry { get; private set; }

	internal LogEventArgs(string LogEntry)
	{
		this.LogEntry = LogEntry;
	}
}
