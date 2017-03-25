using System;

public class BitArray64<T>
{
	public long Flags = 0;

	public BitArray64(long pFlags)
	{
		Flags = pFlags;
	}

	public bool IsFlagged(long pFlagType)
	{
		long f = Convert.ToInt64(pFlagType);
		return ((Flags & f) == f);
	}

	public bool IsFlagged(T pFlagType)
	{
		long f = Convert.ToInt64(pFlagType);
		return ((Flags & f) == f);
	}

	public void Disable(T pFlagType)
	{
		Flags &= ~(Convert.ToInt64(pFlagType));
	}

	public void Disable(long pFlagType)
	{
		Flags &= ~pFlagType;
	}

	public void Enable(T pFlagType)
	{
		Flags |= (Convert.ToInt64(pFlagType));
	}

	public void Enable(long pFlagType)
	{
		Flags |= pFlagType;
	}

	public static bool Read(DataBuffer buffer, out BitArray64<T> array)
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

	public void Write(DataBuffer buffer)
	{
		buffer.WriteLong(Flags);
	}
}