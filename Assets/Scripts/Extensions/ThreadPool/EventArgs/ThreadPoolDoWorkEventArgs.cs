public class ThreadPoolDoWorkEventArgs
{
	private ThreadPoolEventProgressUpdate onUpdateProgress = null;
	private ThreadPoolReportProgress onReportProgress = null;
	private ThreadPoolCancellationToken cancellationToken = null;

	public object Argument = null;
	public object Result = null;

	public bool IsCancellationPending { get { return this.cancellationToken != null && this.cancellationToken.IsCancellationPending; } }

	public ThreadPoolDoWorkEventArgs(object argument)
	{
		Argument = argument;
	}

	internal ThreadPoolDoWorkEventArgs(ThreadPoolEventProgressUpdate onUpdateProgress, ThreadPoolReportProgress onReportProgress, ThreadPoolCancellationToken cancellationToken, object argument)
	{
		this.onUpdateProgress = onUpdateProgress;
		this.onReportProgress = onReportProgress;
		this.cancellationToken = cancellationToken;

		Argument = argument;
	}

	public void Cancel()
	{
		if (this.cancellationToken != null)
		{
			this.cancellationToken.Cancel();
		}
	}
	public void ReportProgress(uint value)
	{
		if (this.onReportProgress != null && this.onUpdateProgress != null)
		{
			this.onReportProgress(this.onUpdateProgress, value);
		}
	}
}