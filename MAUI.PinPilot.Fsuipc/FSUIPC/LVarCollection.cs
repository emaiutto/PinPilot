using System.Collections;

namespace FSUIPC;

public class LVarCollection : IEnumerable
{
	private List<FsLVar> lvars = [];

	private Dictionary<string, FsLVar> nameIndex = [];

	public List<string> Names { get; } = [];

	public int Count => lvars.Count;

	public FsLVar this[string Name]
	{
		get
		{
			if (nameIndex.ContainsKey(Name))
			{
				return nameIndex[Name];
			}
			return null;
		}
	}

	public IEnumerator GetEnumerator() => lvars.GetEnumerator();

	public bool Exists(string Name) => nameIndex.ContainsKey(Name);

	internal void Add(FsLVar lvar)
	{
		lvars.Add(lvar);
		nameIndex.Add(lvar.Name, lvar);
		Names.Add(lvar.Name);
	}

	internal void SortNames() => Names.Sort();

	internal void Clear()
	{
		lvars.Clear();
		nameIndex.Clear();
		Names.Clear();
	}

}
