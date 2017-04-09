using System;
using UnityEngine;

[Serializable]
public class Database : ScriptableObject
{
	[Serializable]
	public class DatabaseDictionary : SerializableDictionary<string, DatabaseTemplate> { }

	[SerializeField]
	private DatabaseDictionary items = new DatabaseDictionary();

	public DatabaseDictionary Items { get { return this.items; } }

	public DatabaseTemplate Get(string identifier)
	{
		DatabaseTemplate item;
		this.items.TryGetValue(identifier, out item);
		return item;
	}

	public void Add(DatabaseTemplate item)
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