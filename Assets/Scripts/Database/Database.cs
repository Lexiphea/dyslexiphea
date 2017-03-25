using System;
using UnityEngine;

[Serializable]
public class Database : ScriptableObject
{
	[Serializable]
	public class DatabaseDictionary : SerializableDictionary<string, DatabaseItem> { }

	[SerializeField]
	private DatabaseDictionary items = new DatabaseDictionary();

	public DatabaseDictionary Items { get { return this.items; } }

	public DatabaseItem Get(string identifier)
	{
		DatabaseItem item;
		this.items.TryGetValue(identifier, out item);
		return item;
	}

	public void Add(DatabaseItem item)
	{
		if (item != null && !this.items.ContainsKey(item.Identifier))
		{
			this.items.Add(item.Identifier, item);
		}
	}

	public void Remove(string identifier)
	{
		this.items.Remove(identifier);
	}
}