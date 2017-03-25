public class ThreadPoolCompletedEventArgs
{
	public bool Cancelled;
	public object Result;

	public ThreadPoolCompletedEventArgs(bool cancelled, object result)
	{
		Cancelled = cancelled;
		Result = result;
	}
}