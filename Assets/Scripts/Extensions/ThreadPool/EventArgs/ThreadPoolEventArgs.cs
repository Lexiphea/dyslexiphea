internal class ThreadPoolEventArgs
{
	public ThreadPoolEventDoWork OnDoWork;
	public ThreadPoolEventProgressUpdate OnUpdateProgress;
	public ThreadPoolEventComplete OnCompleteWork;
	public ThreadPoolCancellationToken CancellationToken;
	public object Argument;
	public object Result;

	public ThreadPoolEventArgs(ThreadPoolEventDoWork onDoWork, ThreadPoolEventProgressUpdate onUpdateProgress, ThreadPoolEventComplete onCompleteWork, ThreadPoolCancellationToken cancellationToken, object argument)
	{
		OnDoWork = onDoWork;
		OnUpdateProgress = onUpdateProgress;
		OnCompleteWork = onCompleteWork;
		CancellationToken = cancellationToken;
		Argument = argument;
		Result = null;
	}
}