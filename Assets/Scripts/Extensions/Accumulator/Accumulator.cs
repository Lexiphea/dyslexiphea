using System;
using UnityEngine;

public class Accumulator : IAccumulator
{
	private float time = 0.0f;
	private float tickRate = 0.0f;

	public Accumulator(float startTime, float tickRate)
	{
		this.time = startTime;
		this.tickRate = tickRate;
	}

	public void SetTime(float time)
	{
		this.time = time;
	}

	public void SetTickRate(float tickRate)
	{
		this.tickRate = tickRate;
	}

	public void TryUpdate(Action action)
	{
		float accumulatedTime = Time.time - this.time;
		while (accumulatedTime >= this.tickRate)
		{
			action();
			accumulatedTime -= this.tickRate;
		}
		this.time = accumulatedTime + Time.time;
	}
}