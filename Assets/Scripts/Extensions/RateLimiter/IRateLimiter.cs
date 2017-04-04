public interface IRateLimiter
{
	bool IsReady { get; }
	void SetDelay(float delay);
	void SetNextTick(float value);
	void Reset();
	void Reset(float delay);
}