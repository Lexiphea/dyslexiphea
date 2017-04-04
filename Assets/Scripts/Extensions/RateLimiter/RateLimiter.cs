using System;
using UnityEngine;

[Serializable]
public class RateLimiter : IRateLimiter
{
	[SerializeField]
	private float delay;
	private float nextTick;

	public bool IsReady { get { return Time.time >= this.nextTick; } }

	public RateLimiter(float initialDelay)
	{
		this.delay = initialDelay;
	}

	public void SetDelay(float delay)
	{
		this.delay = delay;
	}

	public void SetNextTick(float value)
	{
		this.nextTick = value;
	}

	public void Reset()
	{
		this.nextTick = Time.time + this.delay;
	}

	public void Reset(float delay)
	{
		this.nextTick = Time.time + delay;
	}
}