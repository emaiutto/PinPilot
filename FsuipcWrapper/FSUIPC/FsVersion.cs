namespace FSUIPC;

public struct FsVersion
{
	public FlightSim Simulator;

	public short VersionCode;

	internal FsVersion(FlightSim Simulator, short VersionCode)
	{
		this.Simulator = Simulator;
		this.VersionCode = VersionCode;
	}

	public override string ToString()
	{
		string result = Simulator.ToString();
		short num = 0;
		short num2 = 0;
		switch (Simulator)
		{
		case FlightSim.FSX:
			switch (VersionCode)
			{
			case 1:
				result = "FSX (RTM)";
				break;
			case 2:
				result = "FSX (SP1)";
				break;
			case 3:
				result = "FSX (SP2)";
				break;
			case 4:
				result = "FSX (Acceleration Pack)";
				break;
			default:
				if (VersionCode > 100)
				{
					result = "FSX-SE (Update " + (VersionCode - 100) + ")";
				}
				break;
			}
			break;
		case FlightSim.Prepar3d:
		case FlightSim.Prepar3dx64:
			num = (short)(VersionCode / 10);
			num2 = (short)(VersionCode - num * 10);
			result = "Prepar3d (V" + num + "." + num2 + ")";
			break;
		case FlightSim.MSFS:
			num = (short)(VersionCode / 10);
			num2 = (short)(VersionCode - num * 10);
			result = "MSFS (V" + num + "." + num2 + ")";
			break;
		}
		return result;
	}
}
