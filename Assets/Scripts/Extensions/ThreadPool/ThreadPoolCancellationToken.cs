public class ThreadPoolCancellationToken
{
	private volatile bool cancel = false;

	public bool IsCancellationPending { get { return this.cancel; } }

	public void Cancel()
	{
		if (this.cancel)
		{
			return;
		}
		this.cancel = true;
	}
}