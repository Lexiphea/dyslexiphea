public class ConcurrentQueue<T>
{
	private QueueNode<T> head;
	private QueueNode<T> tail;
	private readonly object enqueueLock = new object();
	private readonly object dequeueLock = new object();

	public ConcurrentQueue()
	{
		this.head = this.tail = new QueueNode<T>(null, default(T));
	}

	public void Enqueue(T item)
	{
		QueueNode<T> node = new QueueNode<T>(null, item);
		lock (this.enqueueLock)
		{
			this.tail.Next = node;
			this.tail = node;
		}
	}

	public bool TryDequeue(out T item)
	{
		lock (this.dequeueLock)
		{
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
}