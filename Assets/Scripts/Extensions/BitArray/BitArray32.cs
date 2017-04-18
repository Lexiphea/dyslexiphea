using System;

public class BitArray32<T>
{
	private int flags = 0;

	public BitArray32(int flags)
	{
		this.flags = flags;
	}

	public bool IsFlagged(int flagType)
	{
		int f = Convert.ToInt32(flagType);
		return ((this.flags & f) == f);
	}

	public bool IsFlagged(T flagType)
	{
		int f = Convert.ToInt32(flagType);
		return ((this.flags & f) == f);
	}

	public void Disable(T flagType)
	{
		this.flags &= ~(Convert.ToInt32(flagType));
	}

	public void Disable(int flagType)
	{
		this.flags &= ~flagType;
	}

	public void Enable(T flagType)
	{
		this.flags |= (Convert.ToInt32(flagType));
	}

	public void Enable(int flagType)
	{
		this.flags |= flagType;
	}

	public static bool Read(ByteBuffer buffer, out BitArray32<T> array)
	{
		int flags = 0;
		if (!buffer.ReadInt(out flags))
		{
			array = null;
			return false;
		}
		array = new BitArray32<T>(flags);
		return true;
	}

	public void Write(ByteBuffer buffer)
	{
		buffer.WriteInt((int)this.flags);
	}
}