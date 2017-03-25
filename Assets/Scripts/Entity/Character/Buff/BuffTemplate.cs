using UnityEngine;

public class BuffTemplate : ScriptableObject
{
	private string name = "NO_NAME";
	private string description = "NO_DESCRIPTION";
	private float duration = 0.0f;
	private int maxStacks = 1;

	void OnApply(GameObject obj)
	{

	}

	void OnRemove(GameObject obj)
	{

	}
}