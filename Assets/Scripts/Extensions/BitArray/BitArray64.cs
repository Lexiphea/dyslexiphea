using System;

/// <summary>
/// TEnum should be an enum with a maximum element count of 64 for ease of use.
/// </summary>
public class BitArray64<TEnum>
{
	private ulong flags = 0;

	public BitArray64(ulong flags)
	{
		this.flags = flags;
	}

	public bool IsFlagged(ulong flagType)
	{
		ulong f = Convert.ToUInt64(flagType);
		return ((this.flags & f) == f);
	}

	public bool IsFlagged(TEnum flagType)
	{
		ulong f = Convert.ToUInt64(flagType);
		return ((this.flags & f) == f);
	}

	public void Disable(TEnum flagType)
	{
		this.flags &= ~(Convert.ToUInt64(flagType));
	}

	public void Disable(ulong flagType)
	{
		this.flags &= ~flagType;
	}

	public void Enable(TEnum flagType)
	{
		this.flags |= (Convert.ToUInt64(flagType));
	}

	public void Enable(ulong flagType)
	{
		this.flags |= flagType;
	}

	public static bool Read(byte[] buffer, ref int readOffset, out BitArray64<TEnum> array)
	{
		ulong flags = 0;
		if (!ByteReader.LittleEndian.ReadULong(buffer, ref readOffset, out flags))
		{
			array = null;
			return false;
		}
		array = new BitArray64<TEnum>(flags);
		return true;
	}

	public void Write(ref byte[] buffer, ref int writeOffset)
	{
		ByteWriter.LittleEndian.WriteULong(this.flags, ref buffer, ref writeOffset);
	}
}