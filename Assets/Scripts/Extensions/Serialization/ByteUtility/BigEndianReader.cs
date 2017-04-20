using System;
using System.Text;

public static class BigEndianReader
{
	public static bool ToInt16(byte[] source, ref int readOffset, out short result)
	{
		if (source == null || readOffset + 2 > source.Length)
		{
			result = 0;
			return false;
		}
		int d = source[readOffset++] << 8;
		d |= source[readOffset++];
		result = (short)d;
		return true;
	}

	public static bool ToInt32(byte[] source, ref int readOffset, out int result)
	{
		if (source == null || readOffset + 4 > source.Length)
		{
			result = 0;
			return false;
		}
		int d = source[readOffset++] << 24;
		d |= source[readOffset++] << 16;
		d |= source[readOffset++] << 8;
		d |= source[readOffset++];
		result = d;
		return true;
	}

	public static bool ToInt64(byte[] source, ref int readOffset, out long result)
	{
		if (source == null || readOffset + 8 > source.Length)
		{
			result = 0;
			return false;
		}
		int d = source[readOffset++] << 24;
		d |= source[readOffset++] << 16;
		d |= source[readOffset++] << 8;
		d |= source[readOffset++];
		int d2 = source[readOffset++] << 24;
		d2 |= source[readOffset++] << 16;
		d2 |= source[readOffset++] << 8;
		d2 |= source[readOffset++];
		result = (uint)d2 | ((long)d << 32);
		return true;
	}

	public static bool ToUInt16(byte[] source, ref int readOffset, out ushort result)
	{
		if (source == null || readOffset + 2 > source.Length)
		{
			result = 0;
			return false;
		}
		int d = source[readOffset++] << 8;
		d |= source[readOffset++];
		result = (ushort)d;
		return true;
	}

	public static bool ToUInt32(byte[] source, ref int readOffset, out uint result)
	{
		if (source == null || readOffset + 4 > source.Length)
		{
			result = 0;
			return false;
		}
		int d = source[readOffset++] << 24;
		d |= source[readOffset++] << 16;
		d |= source[readOffset++] << 8;
		d |= source[readOffset++];
		result = (uint)d;
		return true;
	}

	public static bool ToUInt64(byte[] source, ref int readOffset, out ulong result)
	{
		if (source == null || readOffset + 8 > source.Length)
		{
			result = 0;
			return false;
		}
		int d = source[readOffset++] << 24;
		d |= source[readOffset++] << 16;
		d |= source[readOffset++] << 8;
		d |= source[readOffset++];
		int d2 = source[readOffset++] << 24;
		d2 |= source[readOffset++] << 16;
		d2 |= source[readOffset++] << 8;
		d2 |= source[readOffset++];
		result = (uint)d2 | ((ulong)d << 32);
		return true;
	}

	#region RawBytes
	public static bool ReadRawBytes(byte[] source, int length, out byte[] destination)
	{
		int offset = 0;
		return ReadRawBytes(source, ref offset, length, out destination);
	}

	public static bool ReadRawBytes(byte[] source, ref int readOffset, int length, out byte[] destination)
	{
		if (length < 1 || readOffset + length > source.Length)
		{
			destination = default(byte[]);
			return false;
		}
		destination = new byte[length];
		Buffer.BlockCopy(source, readOffset, destination, 0, length);
		readOffset += length;
		return true;
	}
	#endregion

	#region Bytes
	public static bool ReadBytes(byte[] source, out byte[] destination)
	{
		int readOffset = 0;
		return ReadBytes(source, ref readOffset, out destination);
	}

	public static bool ReadBytes(byte[] source, ref int readOffset, out byte[] destination)
	{
		int length;
		if (!ToInt32(source, ref readOffset, out length) || readOffset + length > source.Length)
		{
			destination = default(byte[]);
			return false;
		}
		return ReadRawBytes(source, ref readOffset, length, out destination);
	}
	#endregion

	#region Bool
	public static bool ReadBool(byte[] source, out bool destination)
	{
		int readOffset = 0;
		return ReadBool(source, ref readOffset, out destination);
	}

	public static bool ReadBool(byte[] source, ref int readOffset, out bool destination)
	{
		if (readOffset + 1 > source.Length)
		{
			destination = default(bool);
			return false;
		}
		destination = source[readOffset++] != 0;
		return true;
	}
	#endregion

	#region Byte
	public static bool ReadByte(byte[] source, out byte destination)
	{
		int readOffset = 0;
		return ReadByte(source, ref readOffset, out destination);
	}

	public static bool ReadByte(byte[] source, ref int readOffset, out byte destination)
	{
		if (readOffset + 1 > source.Length)
		{
			destination = default(byte);
			return false;
		}
		destination = source[readOffset++];
		return true;
	}
	#endregion

	#region SByte
	public static bool ReadSByte(byte[] source, out sbyte destination)
	{
		int readOffset = 0;
		return ReadSByte(source, ref readOffset, out destination);
	}

	public static bool ReadSByte(byte[] source, ref int readOffset, out sbyte destination)
	{
		if (readOffset + 1 > source.Length)
		{
			destination = default(sbyte);
			return false;
		}
		destination = (sbyte)source[readOffset++];
		return true;
	}
	#endregion

	#region Char
	public static bool ReadChar(byte[] source, out char destination)
	{
		int readOffset = 0;
		return ReadChar(source, ref readOffset, out destination);
	}

	public static bool ReadChar(byte[] source, ref int readOffset, out char destination)
	{
		short tmp;
		if (!ToInt16(source, ref readOffset, out tmp))
		{
			destination = default(char);
			return false;
		}
		destination = (char)tmp;
		return true;
	}
	#endregion

	#region UShort
	public static bool ReadUShort(byte[] source, out ushort destination)
	{
		int readOffset = 0;
		return ReadUShort(source, ref readOffset, out destination);
	}

	public static bool ReadUShort(byte[] source, ref int readOffset, out ushort destination)
	{
		if (!ToUInt16(source, ref readOffset, out destination))
		{
			destination = default(ushort);
			return false;
		}
		return true;
	}
	#endregion

	#region Short
	public static bool ReadShort(byte[] source, out short destination)
	{
		int readOffset = 0;
		return ReadShort(source, ref readOffset, out destination);
	}

	public static bool ReadShort(byte[] source, ref int readOffset, out short destination)
	{
		if (!ToInt16(source, ref readOffset, out destination))
		{
			destination = default(short);
			return false;
		}
		return true;
	}
	#endregion

	#region UInt
	public static bool ReadUInt(byte[] source, out uint destination)
	{
		int readOffset = 0;
		return ReadUInt(source, ref readOffset, out destination);
	}

	public static bool ReadUInt(byte[] source, ref int readOffset, out uint destination)
	{
		if (!ToUInt32(source, ref readOffset, out destination))
		{
			destination = default(uint);
			return false;
		}
		return true;
	}
	#endregion

	#region UIntArray
	public static bool ReadUIntArray(byte[] source, out uint[] destination)
	{
		int readOffset = 0;
		return ReadUIntArray(source, ref readOffset, out destination);
	}

	public static bool ReadUIntArray(byte[] source, ref int readOffset, out uint[] destination)
	{
		int length;
		if (!ToInt32(source, ref readOffset, out length))
		{
			destination = default(uint[]);
			return false;
		}
		destination = new uint[length];
		for (int i = 0; i < length; ++i)
		{
			if (!ToUInt32(source, ref readOffset, out destination[i]))
			{
				return false;
			}
		}
		return true;
	}
	#endregion

	#region Int
	public static bool ReadInt(byte[] source, out int destination)
	{
		int readOffset = 0;
		return ReadInt(source, ref readOffset, out destination);
	}

	public static bool ReadInt(byte[] source, ref int readOffset, out int destination)
	{
		if (!ToInt32(source, ref readOffset, out destination))
		{
			destination = default(int);
			return false;
		}
		return true;
	}
	#endregion

	#region IntArray
	public static bool ReadIntArray(byte[] source, out int[] destination)
	{
		int readOffset = 0;
		return ReadIntArray(source, ref readOffset, out destination);
	}

	public static bool ReadIntArray(byte[] source, ref int readOffset, out int[] destination)
	{
		int length;
		if (!ToInt32(source, ref readOffset, out length))
		{
			destination = default(int[]);
			return false;
		}
		destination = new int[length];
		for (int i = 0; i < length; ++i)
		{
			if (!ToInt32(source, ref readOffset, out destination[i]))
			{
				return false;
			}
		}
		return true;
	}
	#endregion

	#region ULong
	public static bool ReadULong(byte[] source, out ulong destination)
	{
		int readOffset = 0;
		return ReadULong(source, ref readOffset, out destination);
	}

	public static bool ReadULong(byte[] source, ref int readOffset, out ulong destination)
	{
		int low, high;
		if (!ToInt32(source, ref readOffset, out low) || !ToInt32(source, ref readOffset, out high))
		{
			destination = default(ulong);
			return false;
		}
		destination = (uint)low | ((ulong)high << 32);
		return true;
	}
	#endregion

	#region Long
	public static bool ReadLong(byte[] source, out long destination)
	{
		int readOffset = 0;
		return ReadLong(source, ref readOffset, out destination);
	}

	public static bool ReadLong(byte[] source, ref int readOffset, out long destination)
	{
		int low, high;
		if (!ToInt32(source, ref readOffset, out low) || !ToInt32(source, ref readOffset, out high))
		{
			destination = default(long);
			return false;
		}
		destination = (uint)low | ((long)high << 32);
		return true;
	}
	#endregion

	#region Float
	public static bool ReadFloat(byte[] source, out float destination)
	{
		int readOffset = 0;
		return ReadFloat(source, ref readOffset, out destination);
	}

	public static unsafe bool ReadFloat(byte[] source, ref int readOffset, out float destination)
	{
		int tmp;
		if (!ToInt32(source, ref readOffset, out tmp))
		{
			destination = default(ulong);
			return false;
		}
		destination = *(float*)(&tmp);
		return true;
	}
	#endregion

	#region Double
	public static bool ReadDouble(byte[] source, out double destination)
	{
		int readOffset = 0;
		return ReadDouble(source, ref readOffset, out destination);
	}

	public static unsafe bool ReadDouble(byte[] source, ref int readOffset, out double destination)
	{
		int low, high;
		if (!ToInt32(source, ref readOffset, out low) || !ToInt32(source, ref readOffset, out high))
		{
			destination = default(ulong);
			return false;
		}
		ulong tmp = (uint)low | ((ulong)high << 32);
		destination = *(double*)&tmp;
		return true;
	}
	#endregion

	#region Decimal
	public static bool ReadDecimal(byte[] source, out decimal destination)
	{
		int readOffset = 0;
		return ReadDecimal(source, ref readOffset, out destination);
	}

	public static bool ReadDecimal(byte[] source, ref int readOffset, out decimal destination)
	{
		int[] decimalInts = new int[4];
		if (!ToInt32(source, ref readOffset, out decimalInts[0]) ||
			!ToInt32(source, ref readOffset, out decimalInts[1]) ||
			!ToInt32(source, ref readOffset, out decimalInts[2]) ||
			!ToInt32(source, ref readOffset, out decimalInts[3]))
		{
			destination = default(decimal);
			return false;
		}
		destination = new decimal(decimalInts);
		return true;
	}
	#endregion

	#region String
	public static bool ReadString(byte[] source, out string destination)
	{
		int readOffset = 0;
		return ReadString(source, ref readOffset, out destination);
	}

	public static bool ReadString(byte[] source, ref int readOffset, out string destination)
	{
		return ReadString(source, ref readOffset, Encoding.Unicode, out destination);
	}

	public static bool ReadString(byte[] source, Encoding encoding, out string destination)
	{
		int readOffset = 0;
		return ReadString(source, ref readOffset, encoding, out destination);
	}

	public static bool ReadString(byte[] source, ref int readOffset, Encoding encoding, out string destination)
	{
		int length;
		if (!ToInt32(source, ref readOffset, out length))
		{
			destination = default(string);
			return false;
		}
		if (length > 0)
		{
			byte[] bytes;
			if (readOffset + length > source.Length || !ReadRawBytes(source, ref readOffset, length, out bytes))
			{
				destination = default(string);
				return false;
			}
			destination = encoding.GetString(bytes, 0, bytes.Length);
		}
		else
		{
			destination = "";
		}
		return true;
	}
	#endregion

	#region StringArray
	public static bool ReadStringArray(byte[] source, out string[] destination)
	{
		int readOffset = 0;
		return ReadStringArray(source, ref readOffset, Encoding.Unicode, out destination);
	}

	public static bool ReadStringArray(byte[] source, ref int readOffset, out string[] destination)
	{
		return ReadStringArray(source, ref readOffset, Encoding.Unicode, out destination);
	}

	public static bool ReadStringArray(byte[] source, Encoding encoding, out string[] destination)
	{
		int readOffset = 0;
		return ReadStringArray(source, ref readOffset, encoding, out destination);
	}

	public static bool ReadStringArray(byte[] source, ref int readOffset, Encoding encoding, out string[] destination)
	{
		int length;
		if (!ToInt32(source, ref readOffset, out length))
		{
			destination = default(string[]);
			return false;
		}
		destination = new string[length];
		for (int i = 0; i < length; ++i)
		{
			if (!ReadString(source, ref readOffset, encoding, out destination[i]))
			{
				return false;
			}
		}
		return true;
	}
	#endregion

	#region PaddedString
	public static bool ReadPaddedString(byte[] source, int length, out string destination)
	{
		int readOffset = 0;
		return ReadPaddedString(source, ref readOffset, Encoding.Unicode, length, out destination);
	}

	public static bool ReadPaddedString(byte[] source, ref int readOffset, int length, out string destination)
	{
		return ReadPaddedString(source, ref readOffset, Encoding.Unicode, length, out destination);
	}

	public static bool ReadPaddedString(byte[] source, Encoding encoding, int length, out string destination)
	{
		int readOffset = 0;
		return ReadPaddedString(source, ref readOffset, encoding, length, out destination);
	}

	public static bool ReadPaddedString(byte[] source, ref int readOffset, Encoding encoding, int length, out string destination)
	{
		byte[] bytes;
		if (!ReadRawBytes(source, ref readOffset, length, out bytes))
		{
			destination = default(string);
			return false;
		}
		int len = 0;
		while (bytes[len] != 0x00 && len < length)
		{
			++len;
		}
		if (len > 0)
		{
			destination = encoding.GetString(bytes, 0, len);
		}
		else
		{
			destination = "";
		}
		return true;
	}
	#endregion

	#region Guid
	public static bool ReadGuid(byte[] source, out Guid destination)
	{
		int readOffset = 0;
		return ReadGuid(source, ref readOffset, out destination);
	}

	public static bool ReadGuid(byte[] source, ref int readOffset, out Guid destination)
	{
		byte[] raw;
		if (!ReadRawBytes(source, ref readOffset, 16, out raw))
		{
			destination = Guid.Empty;
			return false;
		}
		//Guid.ToByteArray() returns the first few bytes as little endian instead of big endian.
		byte[] tmp = new byte[8];
		for (int i = 0; i < tmp.Length; ++i)
		{
			tmp[i] = raw[i];
		}
		raw[3] = tmp[0];
		raw[2] = tmp[1];
		raw[1] = tmp[2];
		raw[0] = tmp[3];
		raw[5] = tmp[4];
		raw[4] = tmp[5];
		raw[6] = tmp[6];
		raw[7] = tmp[7];
		destination = new Guid(raw);
		return true;
	}
	#endregion

	#region GuidArray
	public static bool ReadGuidArray(byte[] source, out Guid[] destination)
	{
		int readOffset = 0;
		return ReadGuidArray(source, ref readOffset, out destination);
	}

	public static bool ReadGuidArray(byte[] source, ref int readOffset, out Guid[] destination)
	{
		int length;
		if (!ToInt32(source, ref readOffset, out length))
		{
			destination = default(Guid[]);
			return false;
		}
		destination = new Guid[length];
		for (int i = 0; i < length; ++i)
		{
			if (!ReadGuid(source, ref readOffset, out destination[i]))
			{
				return false;
			}
		}
		return true;
	}
	#endregion

	#region DateTime
	public static bool ReadDateTime(byte[] source, out DateTime destination)
	{
		int readOffset = 0;
		return ReadDateTime(source, ref readOffset, out destination);
	}

	public static bool ReadDateTime(byte[] source, ref int readOffset, out DateTime destination)
	{
		long tmp;
		if (readOffset + 8 > source.Length || !ReadLong(source, ref readOffset, out tmp))
		{
			destination = default(DateTime);
			return false;
		}
		destination = DateTime.FromBinary(tmp);
		return true;
	}
	#endregion
}