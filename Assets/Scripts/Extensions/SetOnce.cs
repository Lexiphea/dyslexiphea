public class SetOnce<T>
{
	private bool isSet = false;
	private T value;

	public T Value
	{
		get
		{
			return this.value;
		}
		set
		{
			if (this.isSet)
			{
				return;
			}
			this.isSet = true;
			this.value = value;
		}
	}

	public static implicit operator T(SetOnce<T> convert)
	{
		return convert.value;
	}
}