using System.Threading;

public sealed class LockFreeQueue<T> where T : class
{
	private class SingleLinkNode
	{
		public SingleLinkNode Next;
		public T Item;
	}
	private SingleLinkNode head = null;
	private SingleLinkNode tail = null;
	public LockFreeQueue()
	{
		this.head = new SingleLinkNode();
		this.tail = this.head;
	}

	private static bool CompareAndExchange(ref SingleLinkNode location, SingleLinkNode comparand, SingleLinkNode newValue)
	{
		return (object)comparand == (object)Interlocked.CompareExchange<SingleLinkNode>(ref location, newValue, comparand);
	}

	public T Next { get { return this.head.Next == null ? null : this.head.Next.Item; } }

	public void Enqueue(T item)
	{
		SingleLinkNode oldTail = null;
		SingleLinkNode oldTailNext;
		SingleLinkNode newNode = new SingleLinkNode();
		newNode.Item = item;
		bool newNodeWasAdded = false;
		while (!newNodeWasAdded)
		{
			oldTail = this.tail;
			oldTailNext = oldTail.Next;

			if (this.tail == oldTail)
			{
				if (oldTailNext == null) newNodeWasAdded = CompareAndExchange(ref this.tail.Next, null, newNode);
				else CompareAndExchange(ref this.tail, oldTail, oldTailNext);
			}
		}

		CompareAndExchange(ref this.tail, oldTail, newNode);
	}

	public bool Dequeue(out T item)
	{
		item = default(T);
		SingleLinkNode oldHead = null;
		bool haveAdvancedHead = false;
		while (!haveAdvancedHead)
		{
			oldHead = this.head;
			SingleLinkNode oldTail = this.tail;
			SingleLinkNode oldHeadNext = oldHead.Next;
			if (oldHead == this.head)
			{
				if (oldHead == oldTail)
				{
					if (oldHeadNext == null)
					{
						return false;
					}
					CompareAndExchange(ref this.tail, oldTail, oldHeadNext);
				}
				else
				{
					item = oldHeadNext.Item;
					haveAdvancedHead = CompareAndExchange(ref this.head, oldHead, oldHeadNext);
				}
			}
		}
		return true;
	}

	public T Dequeue()
	{
		T result;
		Dequeue(out result);
		return result;
	}
}