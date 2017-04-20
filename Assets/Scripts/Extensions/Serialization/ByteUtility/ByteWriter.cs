using System;
using System.Text;

public class ByteWriter
{
	public static readonly ByteWriter LittleEndian = new ByteWriter(Endianness.LittleEndian);
	public static readonly ByteWriter BigEndian = new ByteWriter(Endianness.BigEndian);

	private delegate void WriteInt16Delegate(short source, ref byte[] destination, ref int writeOffset);
	private delegate void WriteInt32Delegate(int source, ref byte[] destination, ref int writeOffset);
	private delegate void WriteInt64Delegate(long source, ref byte[] destination, ref int writeOffset);
	private delegate void WriteUInt16Delegate(ushort source, ref byte[] destination, ref int writeOffset);
	private delegate void WriteUInt32Delegate(uint source, ref byte[] destination, ref int writeOffset);
	private delegate void WriteUInt64Delegate(ulong source, ref byte[] destination, ref int writeOffset);

	private WriteInt16Delegate writeInt16 = null;
	private WriteInt32Delegate writeInt32 = null;
	private WriteInt64Delegate writeInt64 = null;
	private WriteUInt16Delegate writeUInt16 = null;
	private WriteUInt32Delegate writeUInt32 = null;
	private WriteUInt64Delegate writeUInt64 = null;

	private Endianness endianness = Endianness.LittleEndian;

	public Endianness Endianness
	{
		get
		{
			return this.endianness;
		}
		set
		{
			switch (value)
			{
				case Endianness.BigEndian:
					this.writeInt16 = WriteBigEndianInt16;
					this.writeInt32 = WriteBigEndianInt32;
					this.writeInt64 = WriteBigEndianInt64;
					this.writeUInt16 = WriteBigEndianUInt16;
					this.writeUInt32 = WriteBigEndianUInt32;
					this.writeUInt64 = WriteBigEndianUInt64;
					break;
				case Endianness.LittleEndian:
				default:
					this.writeInt16 = WriteLittleEndianInt16;
					this.writeInt32 = WriteLittleEndianInt32;
					this.writeInt64 = WriteLittleEndianInt64;
					this.writeUInt16 = WriteLittleEndianUInt16;
					this.writeUInt32 = WriteLittleEndianUInt32;
					this.writeUInt64 = WriteLittleEndianUInt64;
					break;
			}
		}
	}

	/// <summary>
	/// Default constructor assumes little endian.
	/// </summary>
	public ByteWriter()
	{
		Endianness = Endianness.LittleEndian;
	}

	public ByteWriter(Endianness endianness)
	{
		Endianness = endianness;
	}

	/// <summary>
	/// Ensures the array is capable of fitting the requested length starting at the writeOffset. The array is resized to the exact required length if too small.
	/// </summary>
	public bool EnsureCapacity(ref byte[] array, int writeOffset, int requiredCapacity)
	{
		return EnsureCapacity(ref array, writeOffset, requiredCapacity, true);
	}

	/// <summary>
	/// Ensures the array is capable of fitting the requested length starting at the writeOffset. The array is resized to the exact required length if too small. If expandToExactFit is true it will resize the array to the exact required size otherwise it expands by 256.
	/// </summary>
	public bool EnsureCapacity(ref byte[] array, int writeOffset, int requiredCapacity, bool expandToExactFit)
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

	#region LittleEndian
	public void WriteLittleEndianInt16(short source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 2))
		{
			destination[writeOffset++] = (byte)source;
			destination[writeOffset++] = (byte)(source >> 8);
		}
	}

	public void WriteLittleEndianInt32(int source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 4))
		{
			destination[writeOffset++] = (byte)source;
			destination[writeOffset++] = (byte)(source >> 8);
			destination[writeOffset++] = (byte)(source >> 16);
			destination[writeOffset++] = (byte)(source >> 24);
		}
	}

	public void WriteLittleEndianInt64(long source, ref byte[] destination, ref int writeOffset)
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

	public void WriteLittleEndianUInt16(ushort source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 2))
		{
			destination[writeOffset++] = (byte)source;
			destination[writeOffset++] = (byte)(source >> 8);
		}
	}

	public void WriteLittleEndianUInt32(uint source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 4))
		{
			destination[writeOffset++] = (byte)source;
			destination[writeOffset++] = (byte)(source >> 8);
			destination[writeOffset++] = (byte)(source >> 16);
			destination[writeOffset++] = (byte)(source >> 24);
		}
	}

	public void WriteLittleEndianUInt64(ulong source, ref byte[] destination, ref int writeOffset)
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

	#region BigEndian
	public void WriteBigEndianInt16(short source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 2))
		{
			destination[writeOffset++] = (byte)(source >> 8);
			destination[writeOffset++] = (byte)source;
		}
	}

	public void WriteBigEndianInt32(int source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 4))
		{
			destination[writeOffset++] = (byte)(source >> 24);
			destination[writeOffset++] = (byte)(source >> 16);
			destination[writeOffset++] = (byte)(source >> 8);
			destination[writeOffset++] = (byte)source;
		}
	}

	public void WriteBigEndianInt64(long source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 8))
		{
			destination[writeOffset++] = (byte)(source >> 56);
			destination[writeOffset++] = (byte)(source >> 48);
			destination[writeOffset++] = (byte)(source >> 40);
			destination[writeOffset++] = (byte)(source >> 32);
			destination[writeOffset++] = (byte)(source >> 24);
			destination[writeOffset++] = (byte)(source >> 16);
			destination[writeOffset++] = (byte)(source >> 8);
			destination[writeOffset++] = (byte)source;
		}
	}

	public void WriteBigEndianUInt16(ushort source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 2))
		{
			destination[writeOffset++] = (byte)(source >> 8);
			destination[writeOffset++] = (byte)source;
		}
	}

	public void WriteBigEndianUInt32(uint source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 4))
		{
			destination[writeOffset++] = (byte)(source >> 24);
			destination[writeOffset++] = (byte)(source >> 16);
			destination[writeOffset++] = (byte)(source >> 8);
			destination[writeOffset++] = (byte)source;
		}
	}

	public void WriteBigEndianUInt64(ulong source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 8))
		{
			destination[writeOffset++] = (byte)(source >> 56);
			destination[writeOffset++] = (byte)(source >> 48);
			destination[writeOffset++] = (byte)(source >> 40);
			destination[writeOffset++] = (byte)(source >> 32);
			destination[writeOffset++] = (byte)(source >> 24);
			destination[writeOffset++] = (byte)(source >> 16);
			destination[writeOffset++] = (byte)(source >> 8);
			destination[writeOffset++] = (byte)source;
		}
	}
	#endregion

	#region RawBytes
	public void WriteRawBytes(byte[] source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteRawBytes(source, 0, source.Length, ref destination, ref writeOffset);
	}

	public void WriteRawBytes(byte[] source, ref byte[] destination, ref int writeOffset)
	{
		WriteRawBytes(source, 0, source.Length, ref destination, ref writeOffset);
	}

	public void WriteRawBytes(byte[] source, int start, int length, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteRawBytes(source, start, length, ref destination, ref writeOffset);
	}

	public void WriteRawBytes(byte[] source, int start, int length, ref byte[] destination, ref int writeOffset)
	{
		if (source == null || length < 1 || (length - start) < 1)
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
	public void WriteBytes(byte[] source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteBytes(source, 0, source.Length, ref destination, ref writeOffset);
	}

	public void WriteBytes(byte[] source, ref byte[] destination, ref int writeOffset)
	{
		WriteBytes(source, 0, source.Length, ref destination, ref writeOffset);
	}

	public void WriteBytes(byte[] source, int start, int length, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteBytes(source, 0, source.Length, ref destination, ref writeOffset);
	}

	public void WriteBytes(byte[] source, int start, int length, ref byte[] destination, ref int writeOffset)
	{
		if (source == null || length < 1 || (length - start) < 1)
		{
			this.writeInt32(0, ref destination, ref writeOffset);
			return;
		}
		this.writeInt32(length, ref destination, ref writeOffset);
		WriteRawBytes(source, start, length, ref destination, ref writeOffset);
	}
	#endregion

	#region Bool
	public void WriteBool(bool source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteBool(source, ref destination, ref writeOffset);
	}

	public void WriteBool(bool source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 1))
		{
			destination[writeOffset++] = (byte)(source ? 1 : 0);
		}
	}
	#endregion

	#region Byte
	public void WriteByte(byte source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteByte(source, ref destination, ref writeOffset);
	}

	public void WriteByte(byte source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 1))
		{
			destination[writeOffset++] = source;
		}
	}
	#endregion

	#region SByte
	public void WriteSByte(sbyte source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteSByte(source, ref destination, ref writeOffset);
	}

	public void WriteSByte(sbyte source, ref byte[] destination, ref int writeOffset)
	{
		if (EnsureCapacity(ref destination, writeOffset, 1))
		{
			destination[writeOffset++] = (byte)source;
		}
	}
	#endregion

	#region Char
	public void WriteChar(char source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteChar(source, ref destination, ref writeOffset);
	}

	public void WriteChar(char source, ref byte[] destination, ref int writeOffset)
	{
		this.writeInt16((short)source, ref destination, ref writeOffset);
	}
	#endregion

	#region UShort
	public void WriteUShort(ushort source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteUShort(source, ref destination, ref writeOffset);
	}

	public void WriteUShort(ushort source, ref byte[] destination, ref int writeOffset)
	{
		this.writeUInt16(source, ref destination, ref writeOffset);
	}
	#endregion

	#region Short
	public void WriteShort(short source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteShort(source, ref destination, ref writeOffset);
	}

	public void WriteShort(short source, ref byte[] destination, ref int writeOffset)
	{
		this.writeInt16(source, ref destination, ref writeOffset);
	}
	#endregion

	#region UInt
	public void WriteUInt(uint source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteUInt(source, ref destination, ref writeOffset);
	}

	public void WriteUInt(uint source, ref byte[] destination, ref int writeOffset)
	{
		this.writeUInt32(source, ref destination, ref writeOffset);
	}
	#endregion

	#region UIntArray
	public void WriteUIntArray(uint[] source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteUIntArray(source, ref destination, ref writeOffset);
	}

	public void WriteUIntArray(uint[] source, ref byte[] destination, ref int writeOffset)
	{
		if (source == null || source.Length < 1)
		{
			this.writeInt32(0, ref destination, ref writeOffset);
			return;
		}
		this.writeInt32(source.Length, ref destination, ref writeOffset);
		for (int i = 0; i < source.Length; ++i)
		{
			this.writeUInt32(source[i], ref destination, ref writeOffset);
		}
	}
	#endregion

	#region Int
	public void WriteInt(int source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteInt(source, ref destination, ref writeOffset);
	}

	public void WriteInt(int source, ref byte[] destination, ref int writeOffset)
	{
		this.writeInt32(source, ref destination, ref writeOffset);
	}
	#endregion

	#region IntArray
	public void WriteIntArray(int[] source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteIntArray(source, ref destination, ref writeOffset);
	}

	public void WriteIntArray(int[] source, ref byte[] destination, ref int writeOffset)
	{
		if (source == null || source.Length < 1)
		{
			this.writeInt32(0, ref destination, ref writeOffset);
			return;
		}
		this.writeInt32(source.Length, ref destination, ref writeOffset);
		for (int i = 0; i < source.Length; ++i)
		{
			this.writeInt32(source[i], ref destination, ref writeOffset);
		}
	}
	#endregion

	#region ULong
	public void WriteULong(ulong source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteULong(source, ref destination, ref writeOffset);
	}

	public void WriteULong(ulong source, ref byte[] destination, ref int writeOffset)
	{
		this.writeUInt64(source, ref destination, ref writeOffset);
	}
	#endregion

	#region Long
	public void WriteLong(long source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteLong(source, ref destination, ref writeOffset);
	}

	public void WriteLong(long source, ref byte[] destination, ref int writeOffset)
	{
		this.writeInt64(source, ref destination, ref writeOffset);
	}
	#endregion

	#region Float
	public void WriteFloat(float source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteFloat(source, ref destination, ref writeOffset);
	}

	public unsafe void WriteFloat(float source, ref byte[] destination, ref int writeOffset)
	{
		uint tmp = *(uint*)&source;
		this.writeUInt32(tmp, ref destination, ref writeOffset);
	}
	#endregion

	#region Double
	public void WriteDouble(double source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteDouble(source, ref destination, ref writeOffset);
	}

	public unsafe void WriteDouble(double source, ref byte[] destination, ref int writeOffset)
	{
		ulong tmp = *(ulong*)&source;
		this.writeUInt64(tmp, ref destination, ref writeOffset);
	}
	#endregion

	#region Decimal
	public void WriteDecimal(decimal source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteDecimal(source, ref destination, ref writeOffset);
	}

	public void WriteDecimal(decimal source, ref byte[] destination, ref int writeOffset)
	{
		int[] decimalInts = decimal.GetBits(source);
		this.writeInt32(decimalInts[0], ref destination, ref writeOffset);
		this.writeInt32(decimalInts[1], ref destination, ref writeOffset);
		this.writeInt32(decimalInts[2], ref destination, ref writeOffset);
		this.writeInt32(decimalInts[3], ref destination, ref writeOffset);
	}
	#endregion

	#region String
	public void WriteString(string source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteString(source, ref destination, ref writeOffset);
	}

	public void WriteString(string source, ref byte[] destination, ref int writeOffset)
	{
		WriteString(source, ref destination, ref writeOffset, Encoding.Unicode);
	}

	public void WriteString(string source, ref byte[] destination, Encoding encoding)
	{
		int writeOffset = 0;
		WriteString(source, ref destination, ref writeOffset, encoding);
	}

	public void WriteString(string source, ref byte[] destination, ref int writeOffset, Encoding encoding)
	{
		if (source == null || source.Length < 1)
		{
			this.writeInt32(0, ref destination, ref writeOffset);
			return;
		}
		WriteBytes(encoding.GetBytes(source), ref destination, ref writeOffset);
	}
	#endregion

	#region StringArray
	public void WriteStringArray(string[] source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteStringArray(source, ref destination, ref writeOffset, Encoding.Unicode);
	}

	public void WriteStringArray(string[] source, ref byte[] destination, ref int writeOffset)
	{
		WriteStringArray(source, ref destination, ref writeOffset, Encoding.Unicode);
	}

	public void WriteStringArray(string[] source, ref byte[] destination, Encoding encoding)
	{
		int writeOffset = 0;
		WriteStringArray(source, ref destination, ref writeOffset, encoding);
	}

	public void WriteStringArray(string[] source, ref byte[] destination, ref int writeOffset, Encoding encoding)
	{
		if (source == null || source.Length < 1)
		{
			this.writeInt32(0, ref destination, ref writeOffset);
			return;
		}
		this.writeInt32(source.Length, ref destination, ref writeOffset);
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
	public void WritePaddedString(string source, int length, ref byte[] destination)
	{
		int writeOffset = 0;
		WritePaddedString(source, length, ref destination, ref writeOffset, Encoding.Unicode);
	}

	public void WritePaddedString(string source, int length, ref byte[] destination, ref int writeOffset)
	{
		WritePaddedString(source, length, ref destination, ref writeOffset, Encoding.Unicode);
	}

	public void WritePaddedString(string source, int length, ref byte[] destination, Encoding encoding)
	{
		int writeOffset = 0;
		WritePaddedString(source, length, ref destination, ref writeOffset, encoding);
	}

	public void WritePaddedString(string source, int length, ref byte[] destination, ref int writeOffset, Encoding encoding)
	{
		byte[] raw = encoding.GetBytes(source);
		Array.Resize(ref raw, length);
		WriteRawBytes(raw, ref destination, ref writeOffset);
	}
	#endregion

	#region Guid
	public void WriteGuid(Guid source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteGuid(source, ref destination, ref writeOffset);
	}

	public void WriteGuid(Guid source, ref byte[] destination, ref int writeOffset)
	{
		byte[] raw = source.ToByteArray();
		if (this.endianness == Endianness.BigEndian)
		{
			//Guid.ToByteArray() returns the first few bytes as little endian instead of big endian.
			byte[] tmp = new byte[8];
			tmp[0] = raw[3];
			tmp[1] = raw[2];
			tmp[2] = raw[1];
			tmp[3] = raw[0];
			tmp[4] = raw[5];
			tmp[5] = raw[4];
			tmp[6] = raw[6];
			tmp[7] = raw[7];
			for (int i = 0; i < tmp.Length; ++i)
			{
				raw[i] = tmp[i];
			}
		}
		WriteRawBytes(raw, ref destination, ref writeOffset);
	}
	#endregion

	#region GuidArray
	public void WriteGuidArray(Guid[] source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteGuidArray(source, ref destination, ref writeOffset);
	}

	public void WriteGuidArray(Guid[] source, ref byte[] destination, ref int writeOffset)
	{
		if (source == null || source.Length < 1)
		{
			this.writeInt32(0, ref destination, ref writeOffset);
			return;
		}
		this.writeInt32(source.Length, ref destination, ref writeOffset);
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
	public void WriteDateTime(DateTime source, ref byte[] destination)
	{
		int writeOffset = 0;
		WriteDateTime(source, ref destination, ref writeOffset);
	}

	public void WriteDateTime(DateTime source, ref byte[] destination, ref int writeOffset)
	{
		long s = source.ToBinary();
		this.writeInt64(s, ref destination, ref writeOffset);
	}
	#endregion
}