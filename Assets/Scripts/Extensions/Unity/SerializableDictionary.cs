using System;
using System.Collections.Generic;
using UnityEngine;

public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	[SerializeField]
	private List<TKey> keys = new List<TKey>();
	[SerializeField]
	private List<TValue> values = new List<TValue>();

	public void OnBeforeSerialize()
	{
		this.keys.Clear();
		this.values.Clear();
		foreach (KeyValuePair<TKey, TValue> pair in this)
		{
			this.keys.Add(pair.Key);
			this.values.Add(pair.Value);
		}
	}

	public void OnAfterDeserialize()
	{
		this.Clear();
		if (this.keys.Count != this.values.Count)
		{
			throw new UnityException("SerializableDictionary key and value count do not match!");
		}
		for (int i = 0; i < this.keys.Count; ++i)
		{
			this.Add(this.keys[i], this.values[i]);
		}
	}
}