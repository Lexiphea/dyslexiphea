using System.Threading;

public class BlockingQueue<T>
{
	private QueueNode<T> head;
	private QueueNode<T> tail;
	private readonly object lockObj = new object();
	private volatile bool shutdown = false;

	public BlockingQueue()
	{
		this.head = this.tail = new QueueNode<T>(null, default(T));
	}

	public void Enqueue(T item)
	{
		if (this.shutdown)
		{
			return;
		}
		QueueNode<T> node = new QueueNode<T>(null, item);
		lock (this.lockObj)
		{
			this.tail.Next = node;
			this.tail = node;
			Monitor.PulseAll(this.lockObj);
		}
	}

	public bool TryDequeue(out T item)
	{
		lock (this.lockObj)
		{
			while (this.head.Next == null)
			{
				if (this.shutdown)
				{
					item = default(T);
					return false;
				}
				Monitor.Wait(this.lockObj);
			}
			if (this.head.Next != null)
			{
				item = this.head.Next.Item;
				this.head = this.head.Next;
				return true;
			}
		}
		item = default(T);
		return false;
	}

	public void Shutdown()
	{
		if (!this.shutdown)
		{
			lock (this.lockObj)
			{
				this.shutdown = true;
				Monitor.PulseAll(this.lockObj);
			}
		}
	}
}