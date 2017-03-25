using System.Collections.Generic;
using UnityEngine;

public class SerializableHashSet<T> : HashSet<T>, ISerializationCallbackReceiver
{
	[SerializeField]
	private List<T> values = new List<T>();

	public void OnBeforeSerialize()
	{
		this.values.Clear();
		foreach (T value in this)
		{
			this.values.Add(value);
		}
	}

	public void OnAfterDeserialize()
	{
		this.Clear();
		for (int i = 0; i < this.values.Count; ++i)
		{
			this.Add(this.values[i]);
		}
	}
}