namespace FSUIPC;

public class FsHVar
{
	internal int ID { get; set; }

	public string Name { get; private set; }

	public void Set()
	{
		WAPI.fsuipcw_setHvar(ID);
	}

	internal FsHVar(int ID, string Name)
	{
		this.ID = ID;
		this.Name = Name;
	}
}
