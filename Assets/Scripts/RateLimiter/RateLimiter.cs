using System;
using UnityEngine;

[Serializable]
public class RateLimiter : IRateLimiter
{
	[SerializeField]
	private float rate;
	private float nextTick;

	public void Reset()
	{
		this.nextTick = Time.time;
	}

	public void Reset(float delay)
	{
		this.nextTick = Time.time + delay;
	}

	public void SetNextTick(float value)
	{
		this.nextTick = value;
	}

	public void TryTick(Action<float> action)
	{
		if (Time.time >= this.nextTick)
		{
			action(this.rate);
			this.nextTick = Time.time + this.rate;
		}
	}
}