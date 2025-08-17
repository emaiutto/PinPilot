using System.Collections;
using System.Collections.Generic;

namespace FSUIPC;

public class HVarCollection : IEnumerable
{
	private List<FsHVar> hvars = [];

	private Dictionary<string, FsHVar> nameIndex = [];

	public List<string> Names { get; } = [];

	public int Count => hvars.Count;

	public FsHVar? this[string Name]
	{
		get
		{
			if (nameIndex.TryGetValue(Name, out FsHVar? value)) return value;

			return null;
		}
	}

	public IEnumerator GetEnumerator() => hvars.GetEnumerator();

	public bool Exists(string Name) => nameIndex.ContainsKey(Name);

	internal void Add(FsHVar HVar)
	{
		hvars.Add(HVar);
		nameIndex.Add(HVar.Name, HVar);
		Names.Add(HVar.Name);
	}

	internal void SortNames() => Names.Sort();

	internal void Clear()
	{
		hvars.Clear();
		nameIndex.Clear();
		Names.Clear();
	}
}
