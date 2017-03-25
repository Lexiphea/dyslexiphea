using System;

public class BitArray32<T>
{
	public int Flags = 0;

	public BitArray32(int pFlags)
	{
		Flags = pFlags;
	}

	public bool IsFlagged(int pFlagType)
	{
		int f = Convert.ToInt32(pFlagType);
		return ((Flags & f) == f);
	}

	public bool IsFlagged(T pFlagType)
	{
		int f = Convert.ToInt32(pFlagType);
		return ((Flags & f) == f);
	}

	public void Disable(T pFlagType)
	{
		Flags &= ~(Convert.ToInt32(pFlagType));
	}

	public void Disable(int pFlagType)
	{
		Flags &= ~pFlagType;
	}

	public void Enable(T pFlagType)
	{
		Flags |= (Convert.ToInt32(pFlagType));
	}

	public void Enable(int pFlagType)
	{
		Flags |= pFlagType;
	}

	/*public static bool Read(DataBuffer pBuffer, out BitArray32<T> pArray)
	{
		int flags = 0;
		if (!pBuffer.ReadInt(out flags))
		{
			pArray = null;
			return false;
		}
		pArray = new BitArray32<T>(flags);
		return true;
	}

	public void Write(DataBuffer pBuffer)
	{
		pBuffer.WriteInt((int)Flags);
	}*/
}