using System;
using System.Text;

public static class ByteUtility
{
	private static bool EnsureCapacity(ref byte[] array, int writeOffset, int requiredCapacity)
	{
		const int ExpansionSize = 256;

		if (array == null)
		{
			array = new byte[ExpansionSize > requiredCapacity ? ExpansionSize : requiredCapacity];
			return true;
		}
		int remainingBytes = array.Length - writeOffset;
		if (remainingBytes < requiredCapacity)
		{
			int newSize = array.Length + ExpansionSize;
			while (newSize < writeOffset + requiredCapacity)
			{
				newSize += ExpansionSize;
			}
			Array.Resize<byte>(ref array, newSize);
		}
		return true;
	}

	#region RawBytes
	public static void WriteRawBytes(byte[] source, ref byte[] destination, ref int writeOffset)
	{
		WriteRawBytes(source, 0, source.Length, ref destination, ref writeOffset);
	}

	public static void WriteRawBytes(byte[] source, int start, int length, ref byte[] destination, ref int writeOffset)
	{
		if (length < 1)
		{
			return;
		}
		if (EnsureCapacity(ref destination, writeOffset, length))
		{
			Buffer.BlockCopy(source, start, destination, writeOffset, length);
			writeOffset += length;
		}
	}

	public static bool ReadRawBytes(ref byte[] source, ref int readOffset, out byte[] destination, int length)
	{
		if (length < 1 || readOffset + length > source.Length)
		{
			destination = null;
			return false;
		}
		destination = new byte[length];
		Buffer.BlockCopy(source, readOffset, destination, 0, length);
		readOffset += length;
		return true;
	}
	#endregion

	#region Bytes
	public static void WriteBytes(byte[] source, ref byte[] destination, ref int writeOffset)
	{
		WriteBytes(source, 0, source.Length, ref destination, ref writeOffset);
	}

	public static void WriteBytes(byte[] source, int start, int length, ref byte[] destination, ref int writeOffset)
	{
		WriteInt(length, ref destination, ref writeOffset);
		if (length > 0)
		{
			WriteRawBytes(source, start, length, ref destination, ref writeOffset);
		}
	}

	public static bool ReadBytes(ref byte[] source, ref int readOffset, out byte[] destination)
	{
		int length;
		if (!ReadInt(ref source, ref readOffset, out length) || readOffset + length > source.Length)
		{
			destination = null;
			return false;
		}
		return ReadRawBytes(ref source, ref readOffset, out destination, length);
	}
	#endregion

	#region Bool
	public static void WriteBool(bool value, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 1))
		{
			destination[writeOffset++] = (byte)(value ? 1 : 0);
		}
	}

	public static bool ReadBool(ref byte[] source, ref int readOffset, out bool value)
	{
		if (readOffset + 1 > source.Length)
		{
			value = false;
			return false;
		}
		value = source[readOffset++] != 0;
		return true;
	}
	#endregion

	#region Byte
	public static void WriteByte(byte value, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 1))
		{
			destination[writeOffset++] = value;
		}
	}

	public static bool ReadByte(ref byte[] source, ref int readOffset, out byte value)
	{
		if (readOffset + 1 > source.Length)
		{
			value = 0;
			return false;
		}
		value = source[readOffset++];
		return true;
	}
	#endregion

	#region SByte
	public static void WriteSByte(sbyte value, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 1))
		{
			destination[writeOffset++] = (byte)value;
		}
	}

	public static bool ReadSByte(ref byte[] source, ref int readOffset, out sbyte value)
	{
		if (readOffset + 1 > source.Length)
		{
			value = 0;
			return false;
		}
		value = (sbyte)source[readOffset++];
		return true;
	}
	#endregion

	#region UShort
	public static void WriteUShort(ushort value, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 2))
		{
			destination[writeOffset++] = (byte)(value & 0xFF);
			destination[writeOffset++] = (byte)((value >> 8) & 0xFF);
		}
	}

	public static bool ReadUShort(ref byte[] source, ref int readOffset, out ushort value)
	{
		if (readOffset + 2 > source.Length)
		{
			value = 0;
			return false;
		}
		value = source[readOffset++];
		value |= (ushort)(source[readOffset++] << 8);
		return true;
	}
	#endregion

	#region Short
	public static void WriteShort(short value, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 2))
		{
			destination[writeOffset++] = (byte)(value & 0xFF);
			destination[writeOffset++] = (byte)((value >> 8) & 0xFF);
		}
	}

	public static bool ReadShort(ref byte[] source, ref int readOffset, out short value)
	{
		if (readOffset + 2 > source.Length)
		{
			value = 0;
			return false;
		}
		value = source[readOffset++];
		value |= (short)(source[readOffset++] << 8);
		return true;
	}
	#endregion

	#region Float
	public static unsafe void WriteFloat(float value, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 4))
		{
			uint tmp = *(uint*)(&value);
			destination[writeOffset++] = (byte)(tmp & 0xFF);
			destination[writeOffset++] = (byte)((tmp >> 8) & 0xFF);
			destination[writeOffset++] = (byte)((tmp >> 16) & 0xFF);
			destination[writeOffset++] = (byte)((tmp >> 24) & 0xFF);
		}
	}

	public static unsafe bool ReadFloat(ref byte[] source, ref int readOffset, out float value)
	{
		if (readOffset + 4 > source.Length)
		{
			value = 0.0f;
			return false;
		}
		uint tmp = source[readOffset++];
		tmp |= (uint)(source[readOffset++] << 8);
		tmp |= (uint)(source[readOffset++] << 16);
		tmp |= (uint)(source[readOffset++] << 24);
		value = *(float*)(&tmp);
		return true;
	}
	#endregion

	#region UInt
	public static void WriteUInt(uint value, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 4))
		{
			destination[writeOffset++] = (byte)(value & 0xFF);
			destination[writeOffset++] = (byte)((value >> 8) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 16) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 24) & 0xFF);
		}
	}

	public static bool ReadUInt(ref byte[] source, ref int readOffset, out uint value)
	{
		if (readOffset + 4 > source.Length)
		{
			value = 0;
			return false;
		}
		value = source[readOffset++];
		value |= (uint)(source[readOffset++] << 8);
		value |= (uint)(source[readOffset++] << 16);
		value |= (uint)(source[readOffset++] << 24);
		return true;
	}
	#endregion

	#region UIntArray
	public static void WriteUIntArray(uint[] value, ref byte[] destination, ref int writeOffset)
	{
		if (value == null || value.Length < 1)
		{
			WriteInt(0, ref destination, ref writeOffset);
			return;
		}
		WriteInt(value.Length, ref destination, ref writeOffset);
		for (int i = 0; i < value.Length; ++i)
		{
			WriteUInt(value[i], ref destination, ref writeOffset);
		}
	}

	public static bool ReadUIntArray(ref byte[] source, ref int readOffset, out uint[] value)
	{
		int length;
		if (!ReadInt(ref source, ref readOffset, out length))
		{
			value = null;
			return false;
		}
		value = new uint[length];
		for (int i = 0; i < length; ++i)
		{
			if (!ReadUInt(ref source, ref readOffset, out value[i]))
			{
				return false;
			}
		}
		return true;
	}
	#endregion

	#region Int
	public static void WriteInt(int value, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 4))
		{
			destination[writeOffset++] = (byte)(value & 0xFF);
			destination[writeOffset++] = (byte)((value >> 8) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 16) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 24) & 0xFF);
		}
	}

	public static bool ReadInt(ref byte[] source, ref int readOffset, out int value)
	{
		if (readOffset + 4 > source.Length)
		{
			value = 0;
			return false;
		}
		value = source[readOffset++];
		value |= (source[readOffset++] << 8);
		value |= (source[readOffset++] << 16);
		value |= (source[readOffset++] << 24);
		return true;
	}
	#endregion

	#region IntArray
	public static void WriteIntArray(int[] value, ref byte[] destination, ref int writeOffset)
	{
		if (value == null || value.Length < 1)
		{
			WriteInt(0, ref destination, ref writeOffset);
			return;
		}
		WriteInt(value.Length, ref destination, ref writeOffset);
		for (int i = 0; i < value.Length; ++i)
		{
			WriteInt(value[i], ref destination, ref writeOffset);
		}
	}

	public static bool ReadIntArray(ref byte[] source, ref int readOffset, out int[] value)
	{
		int length;
		if (!ReadInt(ref source, ref readOffset, out length))
		{
			value = null;
			return false;
		}
		value = new int[length];
		for (int i = 0; i < length; ++i)
		{
			if (!ReadInt(ref source, ref readOffset, out value[i]))
			{
				return false;
			}
		}
		return true;
	}
	#endregion

	#region ULong
	public static void WriteULong(ulong value, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 8))
		{
			destination[writeOffset++] = (byte)(value & 0xFF);
			destination[writeOffset++] = (byte)((value >> 8) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 16) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 24) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 32) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 40) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 48) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 56) & 0xFF);
		}
	}

	public static bool ReadULong(ref byte[] source, ref int readOffset, out ulong value)
	{
		if (readOffset + 8 > source.Length)
		{
			value = 0;
			return false;
		}
		value = source[readOffset++];
		value |= ((ulong)source[readOffset++] << 8);
		value |= ((ulong)source[readOffset++] << 16);
		value |= ((ulong)source[readOffset++] << 24);
		value |= ((ulong)source[readOffset++] << 32);
		value |= ((ulong)source[readOffset++] << 40);
		value |= ((ulong)source[readOffset++] << 48);
		value |= ((ulong)source[readOffset++] << 56);
		return true;
	}
	#endregion

	#region Long
	public static void WriteLong(long value, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 8))
		{
			destination[writeOffset++] = (byte)(value & 0xFF);
			destination[writeOffset++] = (byte)((value >> 8) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 16) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 24) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 32) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 40) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 48) & 0xFF);
			destination[writeOffset++] = (byte)((value >> 56) & 0xFF);
		}
	}

	public static bool ReadLong(ref byte[] source, ref int readOffset, out long value)
	{
		if (readOffset + 8 > source.Length)
		{
			value = 0;
			return false;
		}
		value = source[readOffset++];
		value |= ((long)source[readOffset++] << 8);
		value |= ((long)source[readOffset++] << 16);
		value |= ((long)source[readOffset++] << 24);
		value |= ((long)source[readOffset++] << 32);
		value |= ((long)source[readOffset++] << 40);
		value |= ((long)source[readOffset++] << 48);
		value |= ((long)source[readOffset++] << 56);
		return true;
	}
	#endregion

	#region Double
	public static unsafe void WriteDouble(double value, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 8))
		{
			ulong tmp = *(ulong*)(&value);
			destination[writeOffset++] = (byte)(tmp & 0xFF);
			destination[writeOffset++] = (byte)((tmp >> 8) & 0xFF);
			destination[writeOffset++] = (byte)((tmp >> 16) & 0xFF);
			destination[writeOffset++] = (byte)((tmp >> 24) & 0xFF);
			destination[writeOffset++] = (byte)((tmp >> 32) & 0xFF);
			destination[writeOffset++] = (byte)((tmp >> 40) & 0xFF);
			destination[writeOffset++] = (byte)((tmp >> 48) & 0xFF);
			destination[writeOffset++] = (byte)((tmp >> 56) & 0xFF);
		}
	}

	public static unsafe bool ReadDouble(ref byte[] source, ref int readOffset, out double value)
	{
		if (readOffset + 8 > source.Length)
		{
			value = 0.0f;
			return false;
		}
		ulong tmp = source[readOffset++];
		tmp |= (uint)(source[readOffset++] << 8);
		tmp |= (uint)(source[readOffset++] << 16);
		tmp |= (uint)(source[readOffset++] << 24);
		tmp |= (uint)(source[readOffset++] << 32);
		tmp |= (uint)(source[readOffset++] << 40);
		tmp |= (uint)(source[readOffset++] << 48);
		tmp |= (uint)(source[readOffset++] << 56);
		value = *(double*)(&tmp);
		return true;
	}
	#endregion

	#region Guid
	public static void WriteGuid(Guid value, ref byte[] destination, ref int writeOffset)
	{
		WriteRawBytes(value.ToByteArray(), ref destination, ref writeOffset);
	}

	public static bool ReadGuid(ref byte[] source, ref int readOffset, out Guid value)
	{
		byte[] buffer;
		if (!ReadRawBytes(ref source, ref readOffset, out buffer, 16))
		{
			value = Guid.Empty;
			return false;
		}
		value = new Guid(buffer);
		return true;
	}
	#endregion

	#region GuidArray
	public static void WriteGuidArray(Guid[] value, ref byte[] destination, ref int writeOffset)
	{
		if (value == null || value.Length < 1)
		{
			WriteInt(0, ref destination, ref writeOffset);
			return;
		}
		WriteInt(value.Length, ref destination, ref writeOffset);
		for (int i = 0; i < value.Length; ++i)
		{
			if (value[i] == Guid.Empty)
			{
				WriteGuid(Guid.Empty, ref destination, ref writeOffset);
			}
			else
			{
				WriteGuid(value[i], ref destination, ref writeOffset);
			}
		}
	}

	public static bool ReadGuidArray(ref byte[] source, ref int readOffset, out Guid[] value)
	{
		int length;
		if (!ReadInt(ref source, ref readOffset, out length))
		{
			value = null;
			return false;
		}
		value = new Guid[length];
		for (int i = 0; i < length; ++i)
		{
			if (!ReadGuid(ref source, ref readOffset, out value[i]))
			{
				return false;
			}
		}
		return true;
	}
	#endregion

	#region String
	public static void WriteString(string value, ref byte[] destination, ref int writeOffset)
	{
		WriteString(value, ref destination, ref writeOffset, Encoding.Unicode);
	}

	public static void WriteString(string value, ref byte[] destination, ref int writeOffset, Encoding encoding)
	{
		if (value == null || value.Length < 1)
		{
			WriteInt(0, ref destination, ref writeOffset);
			return;
		}
		byte[] source = encoding.GetBytes(value);
		WriteInt(source.Length, ref destination, ref writeOffset);
		WriteRawBytes(source, 0, source.Length, ref destination, ref writeOffset);
	}

	public static bool ReadString(ref byte[] source, ref int readOffset, out string value)
	{
		return ReadString(ref source, ref readOffset, out value, Encoding.Unicode);
	}

	public static bool ReadString(ref byte[] source, ref int readOffset, out string value, Encoding encoding)
	{
		int length;
		if (!ReadInt(ref source, ref readOffset, out length))
		{
			value = null;
			return false;
		}
		if (length > 0)
		{
			byte[] bytes;
			if (readOffset + length > source.Length || !ReadRawBytes(ref source, ref readOffset, out bytes, length))
			{
				value = null;
				return false;
			}
			value = encoding.GetString(bytes, 0, bytes.Length);
		}
		else
		{
			value = "";
		}
		return true;
	}
	#endregion

	#region StringArray
	public static void WriteStringArray(string[] value, ref byte[] destination, ref int writeOffset)
	{
		WriteStringArray(value, ref destination, ref writeOffset, Encoding.Unicode);
	}

	public static void WriteStringArray(string[] value, ref byte[] destination, ref int writeOffset, Encoding encoding)
	{
		if (value == null || value.Length < 1)
		{
			WriteInt(0, ref destination, ref writeOffset);
			return;
		}
		WriteInt(value.Length, ref destination, ref writeOffset);
		for (int i = 0; i < value.Length; ++i)
		{
			if (value[i] == null)
			{
				WriteString("", ref destination, ref writeOffset, encoding);
			}
			else
			{
				WriteString(value[i], ref destination, ref writeOffset, encoding);
			}
		}
	}

	public static bool ReadStringArray(ref byte[] source, ref int readOffset, out string[] value)
	{
		return ReadStringArray(ref source, ref readOffset, out value, Encoding.Unicode);
	}

	public static bool ReadStringArray(ref byte[] source, ref int readOffset, out string[] value, Encoding encoding)
	{
		int length;
		if (!ReadInt(ref source, ref readOffset, out length))
		{
			value = null;
			return false;
		}
		value = new string[length];
		for (int i = 0; i < length; ++i)
		{
			if (!ReadString(ref source, ref readOffset, out value[i], encoding))
			{
				return false;
			}
		}
		return true;
	}
	#endregion

	#region PaddedString
	public static void WritePaddedString(string value, int length, ref byte[] destination, ref int writeOffset)
	{
		WritePaddedString(value, length, ref destination, ref writeOffset, Encoding.Unicode);
	}

	public static void WritePaddedString(string value, int length, ref byte[] destination, ref int writeOffset, Encoding encoding)
	{
		byte[] source = encoding.GetBytes(value);
		Array.Resize(ref source, length);
		WriteRawBytes(source, ref destination, ref writeOffset);
	}

	public static bool ReadPaddedString(ref byte[] source, ref int readOffset, out string value, int length)
	{
		return ReadPaddedString(ref source, ref readOffset, out value, length, Encoding.Unicode);
	}

	public static bool ReadPaddedString(ref byte[] source, ref int readOffset, out string value, int length, Encoding encoding)
	{
		byte[] bytes;
		if (!ReadRawBytes(ref source, ref readOffset, out bytes, length))
		{
			value = null;
			return false;
		}
		int len = 0;
		while (bytes[len] != 0x00 && len < length)
		{
			++len;
		}
		if (len > 0)
		{
			value = encoding.GetString(bytes, 0, len);
		}
		else
		{
			value = "";
		}
		return true;
	}
	#endregion

	#region DateTime
	public static void WriteDateTime(ref byte[] destination, ref int writeOffset, DateTime dateTime)
	{
		long s = dateTime.ToBinary();
		WriteLong(s, ref destination, ref writeOffset);
	}

	public static bool ReadDateTime(ref byte[] source, ref int readOffset, out DateTime dateTime)
	{
		long tmp;
		if (readOffset + 8 > source.Length || !ReadLong(ref source, ref readOffset, out tmp))
		{
			dateTime = DateTime.Now;
			return false;
		}
		dateTime = DateTime.FromBinary(tmp);
		return true;
	}
	#endregion
}