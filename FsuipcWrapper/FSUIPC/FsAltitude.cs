namespace FSUIPC;

[Serializable]
public readonly struct FsAltitude(long FSUnits)
{

	private readonly long fsUnits = FSUnits;

	public readonly double Metres => fsUnits / 4294967296.0;

	public readonly double Feet => Metres * 3.28084;

	public readonly long ToFSUnits() => fsUnits;

	public static FsAltitude FromMetres(double Metres) => new((long)(Metres * 4294967296.0));

	public static FsAltitude FromFeet(double Feet) => FromMetres(Feet / 3.28084);

}
