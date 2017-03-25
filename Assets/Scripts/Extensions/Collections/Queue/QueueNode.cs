public class QueueNode<T>
{
	public QueueNode<T> Next;
	public T Item;

	public QueueNode(QueueNode<T> next, T item)
	{
		Next = next;
		Item = item;
	}
}