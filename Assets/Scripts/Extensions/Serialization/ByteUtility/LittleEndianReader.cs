using System;
using System.Text;

public static class LittleEndianReader
{
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
		if (!ReadInt(source, ref readOffset, out length) || readOffset + length > source.Length)
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
		if (readOffset + 2 > source.Length)
		{
			destination = default(char);
			return false;
		}
		destination = (char)source[readOffset++];
		destination |= (char)(source[readOffset++] << 8);
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
		if (readOffset + 2 > source.Length)
		{
			destination = default(ushort);
			return false;
		}
		destination = source[readOffset++];
		destination |= (ushort)(source[readOffset++] << 8);
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
		if (readOffset + 2 > source.Length)
		{
			destination = default(short);
			return false;
		}
		destination = source[readOffset++];
		destination |= (short)(source[readOffset++] << 8);
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
		if (readOffset + 4 > source.Length)
		{
			destination = default(uint);
			return false;
		}
		destination = source[readOffset++];
		destination |= (uint)source[readOffset++] << 8;
		destination |= (uint)source[readOffset++] << 16;
		destination |= (uint)source[readOffset++] << 24;
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
		if (!ReadInt(source, ref readOffset, out length))
		{
			destination = default(uint[]);
			return false;
		}
		destination = new uint[length];
		for (int i = 0; i < length; ++i)
		{
			if (!ReadUInt(source, ref readOffset, out destination[i]))
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
		if (readOffset + 4 > source.Length)
		{
			destination = default(int);
			return false;
		}
		destination = source[readOffset++];
		destination |= source[readOffset++] << 8;
		destination |= source[readOffset++] << 16;
		destination |= source[readOffset++] << 24;
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
		if (!ReadInt(source, ref readOffset, out length))
		{
			destination = default(int[]);
			return false;
		}
		destination = new int[length];
		for (int i = 0; i < length; ++i)
		{
			if (!ReadInt(source, ref readOffset, out destination[i]))
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
		if (readOffset + 8 > source.Length)
		{
			destination = default(ulong);
			return false;
		}
		destination = source[readOffset++];
		destination |= (ulong)source[readOffset++] << 8;
		destination |= (ulong)source[readOffset++] << 16;
		destination |= (ulong)source[readOffset++] << 24;
		destination |= (ulong)source[readOffset++] << 32;
		destination |= (ulong)source[readOffset++] << 40;
		destination |= (ulong)source[readOffset++] << 48;
		destination |= (ulong)source[readOffset++] << 56;
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
		if (readOffset + 8 > source.Length)
		{
			destination = default(long);
			return false;
		}
		destination = source[readOffset++];
		destination |= (long)source[readOffset++] << 8;
		destination |= (long)source[readOffset++] << 16;
		destination |= (long)source[readOffset++] << 24;
		destination |= (long)source[readOffset++] << 32;
		destination |= (long)source[readOffset++] << 40;
		destination |= (long)source[readOffset++] << 48;
		destination |= (long)source[readOffset++] << 56;
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
		if (readOffset + 4 > source.Length)
		{
			destination = default(float);
			return false;
		}
		uint tmp = source[readOffset++];
		tmp |= (uint)source[readOffset++] << 8;
		tmp |= (uint)source[readOffset++] << 16;
		tmp |= (uint)source[readOffset++] << 24;
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
		if (readOffset + 8 > source.Length)
		{
			destination = default(double);
			return false;
		}
		ulong tmp = source[readOffset++];
		tmp |= (ulong)source[readOffset++] << 8;
		tmp |= (ulong)source[readOffset++] << 16;
		tmp |= (ulong)source[readOffset++] << 24;
		tmp |= (ulong)source[readOffset++] << 32;
		tmp |= (ulong)source[readOffset++] << 40;
		tmp |= (ulong)source[readOffset++] << 48;
		tmp |= (ulong)source[readOffset++] << 56;
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
		if (readOffset + 16 > source.Length)
		{
			destination = default(decimal);
			return false;
		}
		int[] decimalInts = new int[4];
		//Low
		decimalInts[0] |= source[readOffset++];
		decimalInts[0] |= source[readOffset++] << 8;
		decimalInts[0] |= source[readOffset++] << 16;
		decimalInts[0] |= source[readOffset++] << 24;
		//Mid
		decimalInts[1] |= source[readOffset++];
		decimalInts[1] |= source[readOffset++] << 8;
		decimalInts[1] |= source[readOffset++] << 16;
		decimalInts[1] |= source[readOffset++] << 24;
		//High
		decimalInts[2] |= source[readOffset++];
		decimalInts[2] |= source[readOffset++] << 8;
		decimalInts[2] |= source[readOffset++] << 16;
		decimalInts[2] |= source[readOffset++] << 24;
		//Flags
		decimalInts[3] |= source[readOffset++];
		decimalInts[3] |= source[readOffset++] << 8;
		decimalInts[3] |= source[readOffset++] << 16;
		decimalInts[3] |= source[readOffset++] << 24;
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
		if (!ReadInt(source, ref readOffset, out length))
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
		if (!ReadInt(source, ref readOffset, out length))
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
		if (!ReadInt(source, ref readOffset, out length))
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