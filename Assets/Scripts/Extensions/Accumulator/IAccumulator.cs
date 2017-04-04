using System;

public interface IAccumulator
{
	void SetTime(float time);
	void SetTickRate(float tickRate);
	void TryUpdate(Action action);
}