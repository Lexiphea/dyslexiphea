using System;
using System.Text;

public static class ByteUtility
{
	/// <summary>
	/// Ensures the array is capable of fitting the requested length starting at the writeOffset. The array is resized to the exact required length if too small.
	/// </summary>
	public static bool EnsureCapacity(ref byte[] array, int writeOffset, int requiredCapacity)
	{
		return EnsureCapacity(ref array, writeOffset, requiredCapacity, true);
	}

	/// <summary>
	/// Ensures the array is capable of fitting the requested length starting at the writeOffset. The array is resized to the exact required length if too small. If expandToExactFit is true it will resize the array to the exact required size otherwise it expands by 256.
	/// </summary>
	public static bool EnsureCapacity(ref byte[] array, int writeOffset, int requiredCapacity, bool expandToExactFit)
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
			int newSize;
			if (expandToExactFit)
			{
				newSize = array.Length + requiredCapacity - remainingBytes;
			}
			else
			{
				newSize = array.Length + ExpansionSize;
				while (newSize < writeOffset + requiredCapacity)
				{
					newSize += ExpansionSize;
				}
			}
			Array.Resize<byte>(ref array, newSize);
		}
		return true;
	}

	#region RawBytes
	public static void WriteRawBytes(byte[] source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteRawBytes(source, 0, source.Length, ref destination, ref writeOffset);
	}

	public static void WriteRawBytes(byte[] source, ref byte[] destination, ref int writeOffset)
	{
		WriteRawBytes(source, 0, source.Length, ref destination, ref writeOffset);
	}

	public static void WriteRawBytes(byte[] source, int start, int length, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteRawBytes(source, start, length, ref destination, ref writeOffset);
	}

	public static void WriteRawBytes(byte[] source, int start, int length, ref byte[] destination, ref int writeOffset)
	{
		if (length < 1 || (length - start) < 1)
		{
			return;
		}
		if (EnsureCapacity(ref destination, writeOffset, length))
		{
			Buffer.BlockCopy(source, start, destination, writeOffset, length);
			writeOffset += length;
		}
	}

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
	public static void WriteBytes(byte[] source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteBytes(source, 0, source.Length, ref destination, ref writeOffset);
	}

	public static void WriteBytes(byte[] source, ref byte[] destination, ref int writeOffset)
	{
		WriteBytes(source, 0, source.Length, ref destination, ref writeOffset);
	}

	public static void WriteBytes(byte[] source, int start, int length, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteBytes(source, 0, source.Length, ref destination, ref writeOffset);
	}

	public static void WriteBytes(byte[] source, int start, int length, ref byte[] destination, ref int writeOffset)
	{
		if (source == null)
		{
			WriteInt(0, ref destination);
			return;
		}
		WriteInt(length, ref destination, ref writeOffset);
		if (length > 0)
		{
			WriteRawBytes(source, start, length, ref destination, ref writeOffset);
		}
	}

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
	public static void WriteBool(bool source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteBool(source, ref destination, ref writeOffset);
	}

	public static void WriteBool(bool source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 1))
		{
			destination[writeOffset++] = (byte)(source ? 1 : 0);
		}
	}

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
	public static void WriteByte(byte source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteByte(source, ref destination, ref writeOffset);
	}

	public static void WriteByte(byte source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 1))
		{
			destination[writeOffset++] = source;
		}
	}

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
	public static void WriteSByte(sbyte source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteSByte(source, ref destination, ref writeOffset);
	}

	public static void WriteSByte(sbyte source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 1))
		{
			destination[writeOffset++] = (byte)source;
		}
	}

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
	public static void WriteChar(char source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteChar(source, ref destination, ref writeOffset);
	}

	public static void WriteChar(char source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 2))
		{
			destination[writeOffset++] = (byte)source;
			destination[writeOffset++] = (byte)(source >> 8);
		}
	}

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
	public static void WriteUShort(ushort source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteUShort(source, ref destination, ref writeOffset);
	}

	public static void WriteUShort(ushort source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 2))
		{
			destination[writeOffset++] = (byte)source;
			destination[writeOffset++] = (byte)(source >> 8);
		}
	}

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
	public static void WriteShort(short source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteShort(source, ref destination, ref writeOffset);
	}

	public static void WriteShort(short source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 2))
		{
			destination[writeOffset++] = (byte)source;
			destination[writeOffset++] = (byte)(source >> 8);
		}
	}

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
	public static void WriteUInt(uint source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteUInt(source, ref destination, ref writeOffset);
	}

	public static void WriteUInt(uint source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 4))
		{
			destination[writeOffset++] = (byte)source;
			destination[writeOffset++] = (byte)(source >> 8);
			destination[writeOffset++] = (byte)(source >> 16);
			destination[writeOffset++] = (byte)(source >> 24);
		}
	}

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
	public static void WriteUIntArray(uint[] source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteUIntArray(source, ref destination, ref writeOffset);
	}

	public static void WriteUIntArray(uint[] source, ref byte[] destination, ref int writeOffset)
	{
		if (source == null || source.Length < 1)
		{
			WriteInt(0, ref destination, ref writeOffset);
			return;
		}
		WriteInt(source.Length, ref destination, ref writeOffset);
		for (int i = 0; i < source.Length; ++i)
		{
			WriteUInt(source[i], ref destination, ref writeOffset);
		}
	}

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
	public static void WriteInt(int source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteInt(source, ref destination, ref writeOffset);
	}

	public static void WriteInt(int source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 4))
		{
			destination[writeOffset++] = (byte)source;
			destination[writeOffset++] = (byte)(source >> 8);
			destination[writeOffset++] = (byte)(source >> 16);
			destination[writeOffset++] = (byte)(source >> 24);
		}
	}

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
	public static void WriteIntArray(int[] source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteIntArray(source, ref destination, ref writeOffset);
	}

	public static void WriteIntArray(int[] source, ref byte[] destination, ref int writeOffset)
	{
		if (source == null || source.Length < 1)
		{
			WriteInt(0, ref destination, ref writeOffset);
			return;
		}
		WriteInt(source.Length, ref destination, ref writeOffset);
		for (int i = 0; i < source.Length; ++i)
		{
			WriteInt(source[i], ref destination, ref writeOffset);
		}
	}

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
	public static void WriteULong(ulong source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteULong(source, ref destination, ref writeOffset);
	}

	public static void WriteULong(ulong source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 8))
		{
			destination[writeOffset++] = (byte)source;
			destination[writeOffset++] = (byte)(source >> 8);
			destination[writeOffset++] = (byte)(source >> 16);
			destination[writeOffset++] = (byte)(source >> 24);
			destination[writeOffset++] = (byte)(source >> 32);
			destination[writeOffset++] = (byte)(source >> 40);
			destination[writeOffset++] = (byte)(source >> 48);
			destination[writeOffset++] = (byte)(source >> 56);
		}
	}

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
	public static void WriteLong(long source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteLong(source, ref destination, ref writeOffset);
	}

	public static void WriteLong(long source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 8))
		{
			destination[writeOffset++] = (byte)(source & 0xFF);
			destination[writeOffset++] = (byte)((source >> 8) & 0xFF);
			destination[writeOffset++] = (byte)((source >> 16) & 0xFF);
			destination[writeOffset++] = (byte)((source >> 24) & 0xFF);
			destination[writeOffset++] = (byte)((source >> 32) & 0xFF);
			destination[writeOffset++] = (byte)((source >> 40) & 0xFF);
			destination[writeOffset++] = (byte)((source >> 48) & 0xFF);
			destination[writeOffset++] = (byte)((source >> 56) & 0xFF);
		}
	}

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
	public static void WriteFloat(float source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteFloat(source, ref destination, ref writeOffset);
	}

	public static unsafe void WriteFloat(float source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 4))
		{
			uint tmp = *(uint*)&source;
			destination[writeOffset++] = (byte)tmp;
			destination[writeOffset++] = (byte)(tmp >> 8);
			destination[writeOffset++] = (byte)(tmp >> 16);
			destination[writeOffset++] = (byte)(tmp >> 24);
		}
	}

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
	public static void WriteDouble(double source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteDouble(source, ref destination, ref writeOffset);
	}

	public static unsafe void WriteDouble(double source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 8))
		{
			ulong tmp = *(ulong*)&source;
			destination[writeOffset++] = (byte)tmp;
			destination[writeOffset++] = (byte)(tmp >> 8);
			destination[writeOffset++] = (byte)(tmp >> 16);
			destination[writeOffset++] = (byte)(tmp >> 24);
			destination[writeOffset++] = (byte)(tmp >> 32);
			destination[writeOffset++] = (byte)(tmp >> 40);
			destination[writeOffset++] = (byte)(tmp >> 48);
			destination[writeOffset++] = (byte)(tmp >> 56);
		}
	}

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

	#region Double
	public static void WriteDecimal(decimal source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteDecimal(source, ref destination, ref writeOffset);
	}

	public static void WriteDecimal(decimal source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 16))
		{
			int[] decimalInts = decimal.GetBits(source);
			//Low
			destination[writeOffset++] = (byte)decimalInts[0];
			destination[writeOffset++] = (byte)(decimalInts[0] >> 8);
			destination[writeOffset++] = (byte)(decimalInts[0] >> 16);
			destination[writeOffset++] = (byte)(decimalInts[0] >> 24);
			//Mid
			destination[writeOffset++] = (byte)decimalInts[1];
			destination[writeOffset++] = (byte)(decimalInts[1] >> 8);
			destination[writeOffset++] = (byte)(decimalInts[1] >> 16);
			destination[writeOffset++] = (byte)(decimalInts[1] >> 24);
			//High
			destination[writeOffset++] = (byte)decimalInts[2];
			destination[writeOffset++] = (byte)(decimalInts[2] >> 8);
			destination[writeOffset++] = (byte)(decimalInts[2] >> 16);
			destination[writeOffset++] = (byte)(decimalInts[2] >> 24);
			//Flags
			destination[writeOffset++] = (byte)decimalInts[3];
			destination[writeOffset++] = (byte)(decimalInts[3] >> 8);
			destination[writeOffset++] = (byte)(decimalInts[3] >> 16);
			destination[writeOffset++] = (byte)(decimalInts[3] >> 24);
		}
	}

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
	public static void WriteString(string source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteString(source, ref destination, ref writeOffset);
	}

	public static void WriteString(string source, ref byte[] destination, ref int writeOffset)
	{
		WriteString(source, ref destination, ref writeOffset, Encoding.Unicode);
	}

	public static void WriteString(string source, ref byte[] destination, Encoding encoding)
	{
		int writeOffset = 0;
		WriteString(source, ref destination, ref writeOffset, encoding);
	}

	public static void WriteString(string source, ref byte[] destination, ref int writeOffset, Encoding encoding)
	{
		if (source == null || source.Length < 1)
		{
			WriteInt(0, ref destination, ref writeOffset);
			return;
		}
		byte[] raw = encoding.GetBytes(source);
		WriteInt(raw.Length, ref destination, ref writeOffset);
		WriteRawBytes(raw, 0, raw.Length, ref destination, ref writeOffset);
	}

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
	public static void WriteStringArray(string[] source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteStringArray(source, ref destination, ref writeOffset, Encoding.Unicode);
	}

	public static void WriteStringArray(string[] source, ref byte[] destination, ref int writeOffset)
	{
		WriteStringArray(source, ref destination, ref writeOffset, Encoding.Unicode);
	}

	public static void WriteStringArray(string[] source, ref byte[] destination, Encoding encoding)
	{
		int writeOffset = 0;
		WriteStringArray(source, ref destination, ref writeOffset, encoding);
	}

	public static void WriteStringArray(string[] source, ref byte[] destination, ref int writeOffset, Encoding encoding)
	{
		if (source == null || source.Length < 1)
		{
			WriteInt(0, ref destination, ref writeOffset);
			return;
		}
		WriteInt(source.Length, ref destination, ref writeOffset);
		for (int i = 0; i < source.Length; ++i)
		{
			if (source[i] == null)
			{
				WriteString("", ref destination, ref writeOffset, encoding);
			}
			else
			{
				WriteString(source[i], ref destination, ref writeOffset, encoding);
			}
		}
	}

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
	public static void WritePaddedString(string source, int length, ref byte[] destination)
	{
		int writeOffset = 0;
		WritePaddedString(source, length, ref destination, ref writeOffset, Encoding.Unicode);
	}

	public static void WritePaddedString(string source, int length, ref byte[] destination, ref int writeOffset)
	{
		WritePaddedString(source, length, ref destination, ref writeOffset, Encoding.Unicode);
	}

	public static void WritePaddedString(string source, int length, ref byte[] destination, Encoding encoding)
	{
		int writeOffset = 0;
		WritePaddedString(source, length, ref destination, ref writeOffset, encoding);
	}

	public static void WritePaddedString(string source, int length, ref byte[] destination, ref int writeOffset, Encoding encoding)
	{
		byte[] raw = encoding.GetBytes(source);
		Array.Resize(ref raw, length);
		WriteRawBytes(raw, ref destination, ref writeOffset);
	}

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
	public static void WriteGuid(Guid source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteGuid(source, ref destination, ref writeOffset);
	}

	public static void WriteGuid(Guid source, ref byte[] destination, ref int writeOffset)
	{
		WriteRawBytes(source.ToByteArray(), ref destination, ref writeOffset);
	}

	public static bool ReadGuid(byte[] source, out Guid destination)
	{
		int readOffset = 0;
		return ReadGuid(source, ref readOffset, out destination);
	}

	public static bool ReadGuid(byte[] source, ref int readOffset, out Guid destination)
	{
		byte[] buffer;
		if (!ReadRawBytes(source, ref readOffset, 16, out buffer))
		{
			destination = Guid.Empty;
			return false;
		}
		destination = new Guid(buffer);
		return true;
	}
	#endregion

	#region GuidArray
	public static void WriteGuidArray(Guid[] source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteGuidArray(source, ref destination, ref writeOffset);
	}

	public static void WriteGuidArray(Guid[] source, ref byte[] destination, ref int writeOffset)
	{
		if (source == null || source.Length < 1)
		{
			WriteInt(0, ref destination, ref writeOffset);
			return;
		}
		WriteInt(source.Length, ref destination, ref writeOffset);
		for (int i = 0; i < source.Length; ++i)
		{
			if (source[i] == Guid.Empty)
			{
				WriteGuid(Guid.Empty, ref destination, ref writeOffset);
			}
			else
			{
				WriteGuid(source[i], ref destination, ref writeOffset);
			}
		}
	}

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
	public static void WriteDateTime(DateTime source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteDateTime(source, ref destination, ref writeOffset);
	}

	public static void WriteDateTime(DateTime source, ref byte[] destination, ref int writeOffset)
	{
		long s = source.ToBinary();
		WriteLong(s, ref destination, ref writeOffset);
	}

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