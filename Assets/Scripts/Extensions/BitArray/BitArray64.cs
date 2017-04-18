using System;

public class BitArray64<T>
{
	private long flags = 0;

	public BitArray64(long flags)
	{
		this.flags = flags;
	}

	public bool IsFlagged(long flagType)
	{
		long f = Convert.ToInt64(flagType);
		return ((this.flags & f) == f);
	}

	public bool IsFlagged(T flagType)
	{
		long f = Convert.ToInt64(flagType);
		return ((this.flags & f) == f);
	}

	public void Disable(T flagType)
	{
		this.flags &= ~(Convert.ToInt64(flagType));
	}

	public void Disable(long flagType)
	{
		this.flags &= ~flagType;
	}

	public void Enable(T flagType)
	{
		this.flags |= (Convert.ToInt64(flagType));
	}

	public void Enable(long flagType)
	{
		this.flags |= flagType;
	}

	public static bool Read(ByteBuffer buffer, out BitArray64<T> array)
	{
		long flags;
		if (!buffer.ReadLong(out flags))
		{
			array = null;
			return false;
		}
		array = new BitArray64<T>(flags);
		return true;
	}

	public void Write(ByteBuffer buffer)
	{
		buffer.WriteLong(this.flags);
	}
}