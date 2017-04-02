internal class ThreadPoolReportProgressEventArgs
{
	public ThreadPoolEventProgressUpdate OnProgressUpdate;
	public uint Value;

	public ThreadPoolReportProgressEventArgs(ThreadPoolEventProgressUpdate onProgressUpdate, uint value)
	{
		OnProgressUpdate = onProgressUpdate;
		Value = value;
	}
}