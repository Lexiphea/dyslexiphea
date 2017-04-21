using System;
using System.Text;

public class ByteReader
{
	public static readonly ByteReader LittleEndian = new ByteReader(Endianness.LittleEndian);
	public static readonly ByteReader BigEndian = new ByteReader(Endianness.BigEndian);

	private delegate bool ReadInt16Delegate(byte[] source, ref int readOffset, out short result);
	private delegate bool ReadInt32Delegate(byte[] source, ref int readOffset, out int result);
	private delegate bool ReadInt64Delegate(byte[] source, ref int readOffset, out long result);
	private delegate bool ReadUInt16Delegate(byte[] source, ref int readOffset, out ushort result);
	private delegate bool ReadUInt32Delegate(byte[] source, ref int readOffset, out uint result);
	private delegate bool ReadUInt64Delegate(byte[] source, ref int readOffset, out ulong result);

	private ReadInt16Delegate readInt16 = null;
	private ReadInt32Delegate readInt32 = null;
	private ReadInt64Delegate readInt64 = null;
	private ReadUInt16Delegate readUInt16 = null;
	private ReadUInt32Delegate readUInt32 = null;
	private ReadUInt64Delegate readUInt64 = null;

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
					this.readInt16 = ReadBigEndianInt16;
					this.readInt32 = ReadBigEndianInt32;
					this.readInt64 = ReadBigEndianInt64;
					this.readUInt16 = ReadBigEndianUInt16;
					this.readUInt32 = ReadBigEndianUInt32;
					this.readUInt64 = ReadBigEndianUInt64;
					break;
				case Endianness.LittleEndian:
				default:
					this.readInt16 = ReadLittleEndianInt16;
					this.readInt32 = ReadLittleEndianInt32;
					this.readInt64 = ReadLittleEndianInt64;
					this.readUInt16 = ReadLittleEndianUInt16;
					this.readUInt32 = ReadLittleEndianUInt32;
					this.readUInt64 = ReadLittleEndianUInt64;
					break;
			}
			this.endianness = value;
		}
	}

	/// <summary>
	/// Default constructor assumes little endian.
	/// </summary>
	public ByteReader()
	{
		Endianness = Endianness.LittleEndian;
	}

	public ByteReader(Endianness endianness)
	{
		Endianness = endianness;
	}

	#region LittleEndian
	public bool ReadLittleEndianInt16(byte[] source, ref int readOffset, out short result)
	{
		if (source == null || readOffset + 2 > source.Length)
		{
			result = 0;
			return false;
		}
		int d = source[readOffset++];
		d |= source[readOffset++] << 8;
		result = (short)d;
		return true;
	}

	public bool ReadLittleEndianInt32(byte[] source, ref int readOffset, out int result)
	{
		if (source == null || readOffset + 4 > source.Length)
		{
			result = 0;
			return false;
		}
		result = source[readOffset++];
		result |= source[readOffset++] << 8;
		result |= source[readOffset++] << 16;
		result |= source[readOffset++] << 24;
		return true;
	}

	public bool ReadLittleEndianInt64(byte[] source, ref int readOffset, out long result)
	{
		if (source == null || readOffset + 8 > source.Length)
		{
			result = 0;
			return false;
		}
		int d = source[readOffset++];
		d |= source[readOffset++] << 8;
		d |= source[readOffset++] << 16;
		d |= source[readOffset++] << 24;
		int d2 = source[readOffset++];
		d2 |= source[readOffset++] << 8;
		d2 |= source[readOffset++] << 16;
		d2 |= source[readOffset++] << 24;
		result = (uint)d | ((long)d2 << 32);
		return true;
	}

	public bool ReadLittleEndianUInt16(byte[] source, ref int readOffset, out ushort result)
	{
		if (source == null || readOffset + 2 > source.Length)
		{
			result = 0;
			return false;
		}
		int d = source[readOffset++];
		d |= source[readOffset++] << 8;
		result = (ushort)d;
		return true;
	}

	public bool ReadLittleEndianUInt32(byte[] source, ref int readOffset, out uint result)
	{
		if (source == null || readOffset + 4 > source.Length)
		{
			result = 0;
			return false;
		}
		int d = source[readOffset++];
		d |= source[readOffset++] << 8;
		d |= source[readOffset++] << 16;
		d |= source[readOffset++] << 24;
		result = (uint)d;
		return true;
	}

	public bool ReadLittleEndianUInt64(byte[] source, ref int readOffset, out ulong result)
	{
		if (source == null || readOffset + 8 > source.Length)
		{
			result = 0;
			return false;
		}
		int d = source[readOffset++];
		d |= source[readOffset++] << 8;
		d |= source[readOffset++] << 16;
		d |= source[readOffset++] << 24;
		int d2 = source[readOffset++];
		d2 |= source[readOffset++] << 8;
		d2 |= source[readOffset++] << 16;
		d2 |= source[readOffset++] << 24;
		result = (uint)d | ((ulong)d2 << 32);
		return true;
	}
	#endregion

	#region BigEndian
	public bool ReadBigEndianInt16(byte[] source, ref int readOffset, out short result)
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

	public bool ReadBigEndianInt32(byte[] source, ref int readOffset, out int result)
	{
		if (source == null || readOffset + 4 > source.Length)
		{
			result = 0;
			return false;
		}
		result = source[readOffset++] << 24;
		result |= source[readOffset++] << 16;
		result |= source[readOffset++] << 8;
		result |= source[readOffset++];
		return true;
	}

	public bool ReadBigEndianInt64(byte[] source, ref int readOffset, out long result)
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

	public bool ReadBigEndianUInt16(byte[] source, ref int readOffset, out ushort result)
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

	public bool ReadBigEndianUInt32(byte[] source, ref int readOffset, out uint result)
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

	public bool ReadBigEndianUInt64(byte[] source, ref int readOffset, out ulong result)
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
	#endregion

	#region RawBytes
	public bool ReadRawBytes(byte[] source, int length, out byte[] destination)
	{
		int offset = 0;
		return ReadRawBytes(source, ref offset, length, out destination);
	}

	public bool ReadRawBytes(byte[] source, ref int readOffset, int length, out byte[] destination)
	{
		if (source == null || length < 1 || readOffset + length > source.Length)
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
	public bool ReadBytes(byte[] source, out byte[] destination)
	{
		int readOffset = 0;
		return ReadBytes(source, ref readOffset, out destination);
	}

	public bool ReadBytes(byte[] source, ref int readOffset, out byte[] destination)
	{
		int length;
		if (!this.readInt32(source, ref readOffset, out length) || readOffset + length > source.Length)
		{
			destination = default(byte[]);
			return false;
		}
		return ReadRawBytes(source, ref readOffset, length, out destination);
	}
	#endregion

	#region Bool
	public bool ReadBool(byte[] source, out bool destination)
	{
		int readOffset = 0;
		return ReadBool(source, ref readOffset, out destination);
	}

	public bool ReadBool(byte[] source, ref int readOffset, out bool destination)
	{
		if (source == null || readOffset + 1 > source.Length)
		{
			destination = default(bool);
			return false;
		}
		destination = source[readOffset++] != 0;
		return true;
	}
	#endregion

	#region Byte
	public bool ReadByte(byte[] source, out byte destination)
	{
		int readOffset = 0;
		return ReadByte(source, ref readOffset, out destination);
	}

	public bool ReadByte(byte[] source, ref int readOffset, out byte destination)
	{
		if (source == null || readOffset + 1 > source.Length)
		{
			destination = default(byte);
			return false;
		}
		destination = source[readOffset++];
		return true;
	}
	#endregion

	#region SByte
	public bool ReadSByte(byte[] source, out sbyte destination)
	{
		int readOffset = 0;
		return ReadSByte(source, ref readOffset, out destination);
	}

	public bool ReadSByte(byte[] source, ref int readOffset, out sbyte destination)
	{
		if (source == null || readOffset + 1 > source.Length)
		{
			destination = default(sbyte);
			return false;
		}
		destination = (sbyte)source[readOffset++];
		return true;
	}
	#endregion

	#region Char
	public bool ReadChar(byte[] source, out char destination)
	{
		int readOffset = 0;
		return ReadChar(source, ref readOffset, out destination);
	}

	public bool ReadChar(byte[] source, ref int readOffset, out char destination)
	{
		short tmp;
		if (!this.readInt16(source, ref readOffset, out tmp))
		{
			destination = default(char);
			return false;
		}
		destination = (char)tmp;
		return true;
	}
	#endregion

	#region UShort
	public bool ReadUShort(byte[] source, out ushort destination)
	{
		int readOffset = 0;
		return ReadUShort(source, ref readOffset, out destination);
	}

	public bool ReadUShort(byte[] source, ref int readOffset, out ushort destination)
	{
		if (!this.readUInt16(source, ref readOffset, out destination))
		{
			destination = default(ushort);
			return false;
		}
		return true;
	}
	#endregion

	#region Short
	public bool ReadShort(byte[] source, out short destination)
	{
		int readOffset = 0;
		return ReadShort(source, ref readOffset, out destination);
	}

	public bool ReadShort(byte[] source, ref int readOffset, out short destination)
	{
		if (!this.readInt16(source, ref readOffset, out destination))
		{
			destination = default(short);
			return false;
		}
		return true;
	}
	#endregion

	#region UInt
	public bool ReadUInt(byte[] source, out uint destination)
	{
		int readOffset = 0;
		return ReadUInt(source, ref readOffset, out destination);
	}

	public bool ReadUInt(byte[] source, ref int readOffset, out uint destination)
	{
		if (!this.readUInt32(source, ref readOffset, out destination))
		{
			destination = default(uint);
			return false;
		}
		return true;
	}
	#endregion

	#region UIntArray
	public bool ReadUIntArray(byte[] source, out uint[] destination)
	{
		int readOffset = 0;
		return ReadUIntArray(source, ref readOffset, out destination);
	}

	public bool ReadUIntArray(byte[] source, ref int readOffset, out uint[] destination)
	{
		int length;
		if (!this.readInt32(source, ref readOffset, out length))
		{
			destination = default(uint[]);
			return false;
		}
		destination = new uint[length];
		for (int i = 0; i < length; ++i)
		{
			if (!this.readUInt32(source, ref readOffset, out destination[i]))
			{
				return false;
			}
		}
		return true;
	}
	#endregion

	#region Int
	public bool ReadInt(byte[] source, out int destination)
	{
		int readOffset = 0;
		return ReadInt(source, ref readOffset, out destination);
	}

	public bool ReadInt(byte[] source, ref int readOffset, out int destination)
	{
		if (!this.readInt32(source, ref readOffset, out destination))
		{
			destination = default(int);
			return false;
		}
		return true;
	}
	#endregion

	#region IntArray
	public bool ReadIntArray(byte[] source, out int[] destination)
	{
		int readOffset = 0;
		return ReadIntArray(source, ref readOffset, out destination);
	}

	public bool ReadIntArray(byte[] source, ref int readOffset, out int[] destination)
	{
		int length;
		if (!this.readInt32(source, ref readOffset, out length))
		{
			destination = default(int[]);
			return false;
		}
		destination = new int[length];
		for (int i = 0; i < length; ++i)
		{
			if (!this.readInt32(source, ref readOffset, out destination[i]))
			{
				return false;
			}
		}
		return true;
	}
	#endregion

	#region ULong
	public bool ReadULong(byte[] source, out ulong destination)
	{
		int readOffset = 0;
		return ReadULong(source, ref readOffset, out destination);
	}

	public bool ReadULong(byte[] source, ref int readOffset, out ulong destination)
	{
		if (!this.readUInt64(source, ref readOffset, out destination))
		{
			destination = default(ulong);
			return false;
		}
		return true;
	}
	#endregion

	#region Long
	public bool ReadLong(byte[] source, out long destination)
	{
		int readOffset = 0;
		return ReadLong(source, ref readOffset, out destination);
	}

	public bool ReadLong(byte[] source, ref int readOffset, out long destination)
	{
		if (!this.readInt64(source, ref readOffset, out destination))
		{
			destination = default(long);
			return false;
		}
		return true;
	}
	#endregion

	#region Float
	public bool ReadFloat(byte[] source, out float destination)
	{
		int readOffset = 0;
		return ReadFloat(source, ref readOffset, out destination);
	}

	public unsafe bool ReadFloat(byte[] source, ref int readOffset, out float destination)
	{
		int tmp;
		if (!this.readInt32(source, ref readOffset, out tmp))
		{
			destination = default(ulong);
			return false;
		}
		destination = *(float*)&tmp;
		return true;
	}
	#endregion

	#region Double
	public bool ReadDouble(byte[] source, out double destination)
	{
		int readOffset = 0;
		return ReadDouble(source, ref readOffset, out destination);
	}

	public unsafe bool ReadDouble(byte[] source, ref int readOffset, out double destination)
	{
		ulong tmp;
		if (!this.readUInt64(source, ref readOffset, out tmp))
		{
			destination = default(ulong);
			return false;
		}
		destination = *(double*)&tmp;
		return true;
	}
	#endregion

	#region Decimal
	public bool ReadDecimal(byte[] source, out decimal destination)
	{
		int readOffset = 0;
		return ReadDecimal(source, ref readOffset, out destination);
	}

	public bool ReadDecimal(byte[] source, ref int readOffset, out decimal destination)
	{
		int[] decimalInts = new int[4];
		if (!this.readInt32(source, ref readOffset, out decimalInts[0]) ||
			!this.readInt32(source, ref readOffset, out decimalInts[1]) ||
			!this.readInt32(source, ref readOffset, out decimalInts[2]) ||
			!this.readInt32(source, ref readOffset, out decimalInts[3]))
		{
			destination = default(decimal);
			return false;
		}
		destination = new decimal(decimalInts);
		return true;
	}
	#endregion

	#region String
	public bool ReadString(byte[] source, out string destination)
	{
		int readOffset = 0;
		return ReadString(source, ref readOffset, out destination);
	}

	public bool ReadString(byte[] source, ref int readOffset, out string destination)
	{
		return ReadString(source, ref readOffset, Encoding.Unicode, out destination);
	}

	public bool ReadString(byte[] source, Encoding encoding, out string destination)
	{
		int readOffset = 0;
		return ReadString(source, ref readOffset, encoding, out destination);
	}

	public bool ReadString(byte[] source, ref int readOffset, Encoding encoding, out string destination)
	{
		int length;
		if (!this.readInt32(source, ref readOffset, out length))
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
	public bool ReadStringArray(byte[] source, out string[] destination)
	{
		int readOffset = 0;
		return ReadStringArray(source, ref readOffset, Encoding.Unicode, out destination);
	}

	public bool ReadStringArray(byte[] source, ref int readOffset, out string[] destination)
	{
		return ReadStringArray(source, ref readOffset, Encoding.Unicode, out destination);
	}

	public bool ReadStringArray(byte[] source, Encoding encoding, out string[] destination)
	{
		int readOffset = 0;
		return ReadStringArray(source, ref readOffset, encoding, out destination);
	}

	public bool ReadStringArray(byte[] source, ref int readOffset, Encoding encoding, out string[] destination)
	{
		int length;
		if (!this.readInt32(source, ref readOffset, out length))
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
	public bool ReadPaddedString(byte[] source, int length, out string destination)
	{
		int readOffset = 0;
		return ReadPaddedString(source, ref readOffset, Encoding.Unicode, length, out destination);
	}

	public bool ReadPaddedString(byte[] source, ref int readOffset, int length, out string destination)
	{
		return ReadPaddedString(source, ref readOffset, Encoding.Unicode, length, out destination);
	}

	public bool ReadPaddedString(byte[] source, Encoding encoding, int length, out string destination)
	{
		int readOffset = 0;
		return ReadPaddedString(source, ref readOffset, encoding, length, out destination);
	}

	public bool ReadPaddedString(byte[] source, ref int readOffset, Encoding encoding, int length, out string destination)
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
	public bool ReadGuid(byte[] source, out Guid destination)
	{
		int readOffset = 0;
		return ReadGuid(source, ref readOffset, out destination);
	}

	public bool ReadGuid(byte[] source, ref int readOffset, out Guid destination)
	{
		byte[] raw;
		if (!ReadRawBytes(source, ref readOffset, 16, out raw))
		{
			destination = Guid.Empty;
			return false;
		}
		if (this.endianness == Endianness.BigEndian)
		{
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
		}
		destination = new Guid(raw);
		return true;
	}
	#endregion

	#region GuidArray
	public bool ReadGuidArray(byte[] source, out Guid[] destination)
	{
		int readOffset = 0;
		return ReadGuidArray(source, ref readOffset, out destination);
	}

	public bool ReadGuidArray(byte[] source, ref int readOffset, out Guid[] destination)
	{
		int length;
		if (!this.readInt32(source, ref readOffset, out length))
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
	public bool ReadDateTime(byte[] source, out DateTime destination)
	{
		int readOffset = 0;
		return ReadDateTime(source, ref readOffset, out destination);
	}

	public bool ReadDateTime(byte[] source, ref int readOffset, out DateTime destination)
	{
		long tmp;
		if (!this.readInt64(source, ref readOffset, out tmp))
		{
			destination = default(DateTime);
			return false;
		}
		destination = DateTime.FromBinary(tmp);
		return true;
	}
	#endregion
}