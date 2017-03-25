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

	/*public static bool Read(DataBuffer pBuffer, out BitArray64<T> pArray)
	{
		long flags;
		if (!pBuffer.ReadLong(out flags))
		{
			pArray = null;
			return false;
		}
		pArray = new BitArray64<T>(flags);
		return true;
	}

	public void Write(DataBuffer pBuffer)
	{
		pBuffer.WriteLong(Flags);
	}*/
}