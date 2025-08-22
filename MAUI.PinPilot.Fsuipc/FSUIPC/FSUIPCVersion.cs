namespace FSUIPC;

public struct FSUIPCVersion
{
	internal int major;

	internal int minor;

	internal int build;

	public int Major => major;

	public int Minor => minor;

	public int Build => build;

	public string BuildLetter
	{
		get
		{
			if (build > 0)
			{
				return char.ConvertFromUtf32(build + 96);
			}
			return "";
		}
	}

	public double Number => (double)major + (double)minor / 1000.0;

	internal FSUIPCVersion(int value)
	{
		string text = ((value & 0xFFFF0000u) / 65536).ToString("X");
		major = int.Parse(text.Substring(0, 1));
		minor = int.Parse(text.Substring(1));
		build = value & 0xFFFF;
	}

	public override string ToString()
	{
		return major + "." + minor.ToString("000") + BuildLetter;
	}
}
