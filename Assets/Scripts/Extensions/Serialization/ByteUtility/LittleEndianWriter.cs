using System;
using System.Text;

public static class LittleEndianWriter
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
			array = new byte[ExpansionSize > requiredCapacity && !expandToExactFit ? ExpansionSize : requiredCapacity];
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
	#endregion

	#region Decimal
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
	#endregion

	#region Guid
	public static void WriteGuid(Guid source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteGuid(source, ref destination, ref writeOffset);
	}

	public static void WriteGuid(Guid source, ref byte[] destination, ref int writeOffset)
	{
		byte[] raw = source.ToByteArray();
		WriteRawBytes(raw, ref destination, ref writeOffset);
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
	#endregion
}