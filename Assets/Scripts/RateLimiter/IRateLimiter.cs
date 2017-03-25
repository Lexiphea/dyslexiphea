using System;

public interface IRateLimiter
{
	void Reset();
	void SetNextTick(float value);
	void TryTick(Action<float> action);
}