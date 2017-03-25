using UnityEngine;

public class Buff : MonoBehaviour
{
	private BuffTemplate template;
	private float startTime = -1.0f;
	private float lastTick;
	private float remainingDuration;
	private int stacks;

	void Update()
	{
		if (this.startTime > 0.0f)
		{

		}
	}

	private void OnApply(GameObject obj)
	{
		this.startTime = Time.time;
		this.lastTick = Time.time;
	}

	private void OnRemove(GameObject obj)
	{

	}

	private void OnReset()
	{

	}
}