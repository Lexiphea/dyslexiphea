using System;
using System.Text;
using System.IO;
using UnityEngine;

public class ByteBuffer
{
	private const int DEFAULT_SIZE = 256;
	//protected static MessageLog Log = new MessageLog("[ByteBuffer]");

	private byte[] data = null;
	private int writeCursor = 0;
	private int readCursor = 0;
	private bool isFixedSize = false;

	public ByteBuffer()
	{
		this.data = new byte[DEFAULT_SIZE];
	}

	public ByteBuffer(byte[] data)
	{
		this.data = new byte[data.Length];
		WriteRawBytes(data, 0, this.data.Length);
	}

	internal ByteBuffer(byte[] data, int start, int length)
	{
		this.data = new byte[length];
		WriteRawBytes(data, start, length);
	}

	public byte[] Data { get { return this.data; } set { this.data = value; this.writeCursor = this.data.Length; } }

	public int Length { get { return this.writeCursor; } set { this.writeCursor = value; } }

	public int WriteCursor { get { return this.writeCursor; } set { this.writeCursor = value; } }
	public int WriteRemaining { get { return Length - this.writeCursor; } }
	public int ReadCursor { get { return this.readCursor; } set { this.readCursor = value; } }
	public int ReadRemaining { get { return Length - this.readCursor; } }
	public int Remaining { get { return this.writeCursor - this.readCursor; } }

	public bool IsFixedSize
	{
		get
		{
			return this.isFixedSize;
		}
	}

	private void Prepare(int length)
	{
		if (this.isFixedSize)
		{
			return;
		}
		if (this.data.Length - this.writeCursor >= length)
		{
			return;
		}
		int newSize = this.data.Length + DEFAULT_SIZE;
		while (newSize < this.writeCursor + length)
		{
			newSize += DEFAULT_SIZE;
		}
		Array.Resize<byte>(ref this.data, newSize);
	}

	public void Rewind(int position)
	{
		if (position < 0)
		{
			position = 0;
		}
		else if (position > this.writeCursor)
		{
			position = this.writeCursor;
		}
		this.readCursor = position;
	}

	public void WriteSkip(int length)
	{
		Prepare(length);
		this.writeCursor += length;
	}

	public bool ReadSkip(int length)
	{
		if (this.readCursor + length > this.writeCursor)
		{
			return false;
		}
		this.readCursor += length;
		return true;
	}

	public void Flush(byte[] buffer, int start)
	{
		Buffer.BlockCopy(this.data, 0, buffer, start, this.writeCursor);
	}

	#region Bool
	public void WriteBool(bool value)
	{
		Prepare(1);
		this.data[this.writeCursor++] = (byte)(value ? 1 : 0);
	}

	public bool ReadBool(out bool value)
	{
		value = false;
		if (this.readCursor + 1 > this.writeCursor)
		{
			return false;
		}
		value = this.data[this.readCursor++] != 0;
		return true;
	}
	#endregion

	#region Byte
	public void WriteByte(byte value)
	{
		Prepare(1);
		this.data[this.writeCursor++] = value;
	}

	public void WriteSByte(sbyte value)
	{
		Prepare(1);
		this.data[this.writeCursor++] = (byte)value;
	}

	public void WriteRawBytes(byte[] bytes)
	{
		WriteRawBytes(bytes, 0, bytes.Length);
	}

	public void WriteRawBytes(byte[] bytes, int start, int length)
	{
		if (length < 1)
		{
			return;
		}
		Prepare(length);
		Buffer.BlockCopy(bytes, start, this.data, this.writeCursor, length);
		this.writeCursor += length;
	}

	public void WriteBytes(byte[] bytes)
	{
		if (bytes == null)
		{
			WriteInt(0);
			return;
		}
		WriteBytes(bytes, 0, bytes.Length);
	}

	public void WriteBytes(byte[] bytes, int start, int length)
	{
		WriteInt(length);
		if (length < 1)
		{
			return;
		}
		WriteRawBytes(bytes, start, length);
	}

	public bool ReadByte(out byte value)
	{
		value = 0;
		if (this.readCursor + 1 > this.writeCursor)
		{
			return false;
		}
		value = this.data[this.readCursor++];
		return true;
	}

	public bool ReadSByte(out sbyte value)
	{
		value = 0;
		if (this.readCursor + 1 > this.writeCursor)
		{
			return false;
		}
		value = (sbyte)this.data[this.readCursor++];
		return true;
	}

	public bool ReadRawBytes(out byte[] bytes, int length)
	{
		bytes = null;
		if (length < 1)
		{
			return false;
		}
		if (this.readCursor + length > this.writeCursor)
		{
			return false;
		}
		bytes = new byte[length];
		Buffer.BlockCopy(this.data, this.readCursor, bytes, 0, length);
		this.readCursor += length;
		return true;
	}

	public bool ReadBytes(out byte[] bytes)
	{
		int length;
		bytes = null;
		if (!ReadInt(out length))
		{
			return false;
		}
		if (this.readCursor + length > this.writeCursor)
		{
			return false;
		}
		return ReadRawBytes(out bytes, length);
	}
	#endregion

	#region UShort
	public void WriteUShort(ushort value)
	{
		Prepare(2);
		this.data[this.writeCursor++] = (byte)(value & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 8) & 0xFF);
	}

	public bool ReadUShort(out ushort value)
	{
		value = 0;
		if (this.readCursor + 2 > this.writeCursor)
		{
			return false;
		}
		value = this.data[this.readCursor++];
		value |= (ushort)(this.data[this.readCursor++] << 8);
		return true;
	}
	#endregion

	#region Short
	public void WriteShort(short value)
	{
		Prepare(2);
		this.data[this.writeCursor++] = (byte)(value & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 8) & 0xFF);
	}

	public bool ReadShort(out short value)
	{
		value = 0;
		if (this.readCursor + 2 > this.writeCursor)
		{
			return false;
		}
		value = this.data[this.readCursor++];
		value |= (short)(this.data[this.readCursor++] << 8);
		return true;
	}
	#endregion

	#region Float
	public unsafe void WriteFloat(float value)
	{
		Prepare(4);
		uint tmp = *(uint*)(&value);
		this.data[this.writeCursor++] = (byte)(tmp & 0xFF);
		this.data[this.writeCursor++] = (byte)((tmp >> 8) & 0xFF);
		this.data[this.writeCursor++] = (byte)((tmp >> 16) & 0xFF);
		this.data[this.writeCursor++] = (byte)((tmp >> 24) & 0xFF);
	}

	public unsafe bool ReadFloat(out float value)
	{
		value = 0;
		byte[] buffer;
		if (!ReadRawBytes(out buffer, 4))
		{
			return false;
		}
		uint tmp = this.data[this.readCursor++];
		tmp |= (uint)(this.data[this.readCursor++] << 8);
		tmp |= (uint)(this.data[this.readCursor++] << 16);
		tmp |= (uint)(this.data[this.readCursor++] << 24);
		value = *(float*)(&tmp);
		return true;
	}
	#endregion

	#region UInt
	public void WriteUIntArray(uint[] value)
	{
		if (value == null || value.Length < 1)
		{
			WriteInt(0);
			return;
		}
		WriteInt(value.Length);
		for (int i = 0; i < value.Length; ++i)
		{
			WriteUInt(value[i]);
		}
	}

	public void WriteUInt(uint value)
	{
		Prepare(4);
		this.data[this.writeCursor++] = (byte)(value & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 8) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 16) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 24) & 0xFF);
	}

	public bool ReadUInt(out uint value)
	{
		value = 0;
		if (this.readCursor + 4 > this.writeCursor)
		{
			return false;
		}
		value = this.data[this.readCursor++];
		value |= (uint)(this.data[this.readCursor++] << 8);
		value |= (uint)(this.data[this.readCursor++] << 16);
		value |= (uint)(this.data[this.readCursor++] << 24);
		return true;
	}

	public bool ReadUIntArray(out uint[] value)
	{
		value = null;
		int length;
		if (!ReadInt(out length))
		{
			return false;
		}
		value = new uint[length];
		for (int i = 0; i < length; ++i)
		{
			if (!ReadUInt(out value[i]))
			{
				return false;
			}
		}
		return true;
	}
	#endregion

	#region Int
	public void WriteInt(int value)
	{
		Prepare(4);
		this.data[this.writeCursor++] = (byte)(value & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 8) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 16) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 24) & 0xFF);
	}

	public bool ReadInt(out int value)
	{
		value = 0;
		if (this.readCursor + 4 > this.writeCursor)
		{
			return false;
		}
		value = this.data[this.readCursor++];
		value |= (this.data[this.readCursor++] << 8);
		value |= (this.data[this.readCursor++] << 16);
		value |= (this.data[this.readCursor++] << 24);
		return true;
	}
	#endregion

	#region ULong
	public void WriteULong(ulong value)
	{
		Prepare(8);
		this.data[this.writeCursor++] = (byte)(value & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 8) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 16) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 24) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 32) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 40) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 48) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 56) & 0xFF);
	}

	public bool ReadULong(out ulong value)
	{
		value = 0;
		if (this.readCursor + 8 > this.writeCursor)
		{
			return false;
		}
		value = this.data[this.readCursor++];
		value |= ((ulong)this.data[this.readCursor++] << 8);
		value |= ((ulong)this.data[this.readCursor++] << 16);
		value |= ((ulong)this.data[this.readCursor++] << 24);
		value |= ((ulong)this.data[this.readCursor++] << 32);
		value |= ((ulong)this.data[this.readCursor++] << 40);
		value |= ((ulong)this.data[this.readCursor++] << 48);
		value |= ((ulong)this.data[this.readCursor++] << 56);
		return true;
	}
	#endregion

	#region Long
	public void WriteLong(long value)
	{
		Prepare(8);
		this.data[this.writeCursor++] = (byte)(value & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 8) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 16) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 24) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 32) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 40) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 48) & 0xFF);
		this.data[this.writeCursor++] = (byte)((value >> 56) & 0xFF);
	}

	public bool ReadLong(out long value)
	{
		value = 0;
		if (this.readCursor + 8 > this.writeCursor)
		{
			return false;
		}
		value = this.data[this.readCursor++];
		value |= ((long)this.data[this.readCursor++] << 8);
		value |= ((long)this.data[this.readCursor++] << 16);
		value |= ((long)this.data[this.readCursor++] << 24);
		value |= ((long)this.data[this.readCursor++] << 32);
		value |= ((long)this.data[this.readCursor++] << 40);
		value |= ((long)this.data[this.readCursor++] << 48);
		value |= ((long)this.data[this.readCursor++] << 56);
		return true;
	}
	#endregion

	#region Double
	public unsafe void WriteDouble(double value)
	{
		Prepare(8);
		ulong tmp = *(ulong*)(&value);
		this.data[this.writeCursor++] = (byte)(tmp & 0xFF);
		this.data[this.writeCursor++] = (byte)((tmp >> 8) & 0xFF);
		this.data[this.writeCursor++] = (byte)((tmp >> 16) & 0xFF);
		this.data[this.writeCursor++] = (byte)((tmp >> 24) & 0xFF);
		this.data[this.writeCursor++] = (byte)((tmp >> 32) & 0xFF);
		this.data[this.writeCursor++] = (byte)((tmp >> 40) & 0xFF);
		this.data[this.writeCursor++] = (byte)((tmp >> 48) & 0xFF);
		this.data[this.writeCursor++] = (byte)((tmp >> 56) & 0xFF);
	}

	public unsafe bool ReadDouble(out double value)
	{
		value = 0;
		byte[] buffer;
		if (!ReadRawBytes(out buffer, 8))
		{
			return false;
		}
		ulong tmp = this.data[this.readCursor++];
		tmp |= (uint)(this.data[this.readCursor++] << 8);
		tmp |= (uint)(this.data[this.readCursor++] << 16);
		tmp |= (uint)(this.data[this.readCursor++] << 24);
		tmp |= (uint)(this.data[this.readCursor++] << 32);
		tmp |= (uint)(this.data[this.readCursor++] << 40);
		tmp |= (uint)(this.data[this.readCursor++] << 48);
		tmp |= (uint)(this.data[this.readCursor++] << 56);
		value = *(double*)(&tmp);
		return true;
	}
	#endregion

	#region Guid
	public void WriteGuidArray(Guid[] value)
	{
		if (value == null || value.Length < 1)
		{
			WriteInt(0);
			return;
		}
		WriteInt(value.Length);
		for (int i = 0; i < value.Length; ++i)
		{
			if (value[i] == Guid.Empty)
			{
				WriteGuid(Guid.Empty);
			}
			else
			{
				WriteGuid(value[i]);
			}
		}
	}

	public void WriteGuid(Guid value)
	{
		WriteRawBytes(value.ToByteArray());
	}

	public bool ReadGuid(out Guid value)
	{
		value = Guid.Empty;
		byte[] buffer;
		if (!ReadRawBytes(out buffer, 16))
		{
			return false;
		}
		value = new Guid(buffer);
		return true;
	}

	public bool ReadGuidArray(out Guid[] value)
	{
		value = null;
		int length;
		if (!ReadInt(out length))
		{
			return false;
		}
		value = new Guid[length];
		for (int i = 0; i < length; ++i)
		{
			if (!ReadGuid(out value[i]))
			{
				return false;
			}
		}
		return true;
	}
	#endregion

	#region String
	public void WriteString(string value)
	{
		if (value == null || value.Length < 1)
		{
			WriteInt(0);
			return;
		}

		byte[] bytes = Encoding.ASCII.GetBytes(value);
		WriteInt(bytes.Length);
		WriteRawBytes(bytes, 0, bytes.Length);
	}

	public void WriteStringArray(string[] value)
	{
		if (value == null || value.Length < 1)
		{
			WriteInt(0);
			return;
		}
		WriteInt(value.Length);
		for (int i = 0; i < value.Length; ++i)
		{
			if (value[i] == null)
			{
				WriteString("");
			}
			else
			{
				WriteString(value[i]);
			}
		}
	}

	public void WritePaddedString(string value, int length)
	{
		byte[] buffer = Encoding.ASCII.GetBytes(value);
		Array.Resize(ref buffer, length);
		WriteRawBytes(buffer);
	}

	public bool ReadString(out string value)
	{
		value = null;
		int length;
		if (!ReadInt(out length))
		{
			return false;
		}
		if (length > 0)
		{
			byte[] bytes;
			if (this.readCursor + length > this.writeCursor)
			{
				return false;
			}
			if (!ReadRawBytes(out bytes, length))
			{
				return false;
			}
			value = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
		}
		return true;
	}

	public bool ReadStringArray(out string[] value)
	{
		value = null;
		int length;
		if (!ReadInt(out length))
		{
			return false;
		}
		value = new string[length];
		for (int i = 0; i < length; ++i)
		{
			if (!ReadString(out value[i]))
			{
				return false;
			}
		}
		return true;
	}

	public bool ReadPaddedString(out string value, int length)
	{
		byte[] bytes;
		value = null;
		if (!ReadRawBytes(out bytes, length))
		{
			return false;
		}
		int len = 0;
		while (bytes[len] != 0x00 && len < length)
		{
			++len;
		}
		if (len > 0)
		{
			value = Encoding.ASCII.GetString(bytes, 0, len);
		}
		return true;
	}
	#endregion

	#region GenericBits
	public void Write8Bits<T>(T value)
	{
		WriteByte((byte)(object)value);
	}

	public void Write16Bits<T>(T value)
	{
		WriteShort((short)(object)value);
	}

	public void Write32Bits<T>(T value)
	{
		WriteInt((int)(object)value);
	}

	public void Write64Bits<T>(T value)
	{
		WriteLong((long)(object)value);
	}

	public bool Read8Bits<T>(out T value)
	{
		value = default(T);
		byte tmp;
		if (this.readCursor + 1 > this.writeCursor)
		{
			return false;
		}
		if (!ReadByte(out tmp))
		{
			return false;
		}
		value = (T)(object)tmp;
		return true;
	}

	public bool Read16Bits<T>(out T value)
	{
		value = default(T);
		short tmp;
		if (this.readCursor + 2 > this.writeCursor)
		{
			return false;
		}
		if (!ReadShort(out tmp))
		{
			return false;
		}
		value = (T)(object)tmp;
		return true;
	}

	public bool Read32Bits<T>(out T value)
	{
		value = default(T);
		int tmp;
		if (this.readCursor + 4 > this.writeCursor)
		{
			return false;
		}
		if (!ReadInt(out tmp))
		{
			return false;
		}
		value = (T)(object)tmp;
		return true;
	}

	public bool Read64Bits<T>(out T value)
	{
		value = default(T);
		long tmp;
		if (this.readCursor + 8 > this.writeCursor)
		{
			return false;
		}
		if (!ReadLong(out tmp))
		{
			return false;
		}
		value = (T)(object)tmp;
		return true;
	}
	#endregion

	#region DateTime
	public void WriteDateTime(DateTime dateTime)
	{
		long s = dateTime.ToBinary();
		WriteLong(s);
	}

	public bool ReadDateTime(out DateTime dateTime)
	{
		dateTime = DateTime.Now;
		long tmp;
		if (this.readCursor + 8 > this.writeCursor)
		{
			return false;
		}
		if (!ReadLong(out tmp))
		{
			return false;
		}
		dateTime = DateTime.FromBinary(tmp);
		return true;
	}
	#endregion

	#region Vector
	public void WriteVector2(Vector2 vector)
	{
		WriteFloat(vector.x);
		WriteFloat(vector.y);
	}
	public bool ReadVector2(out Vector2 vector)
	{
		vector = Vector2.zero;
		if (this.readCursor + 8 > this.writeCursor)
		{
			return false;
		}
		if (!ReadFloat(out vector.x) ||
			!ReadFloat(out vector.y))
		{
			return false;
		}
		return true;
	}

	public void WriteVector3(Vector3 vector)
	{
		WriteFloat(vector.x);
		WriteFloat(vector.y);
		WriteFloat(vector.z);
	}
	public bool ReadVector3(out Vector3 vector)
	{
		vector = Vector3.zero;
		if (this.readCursor + 8 > this.writeCursor)
		{
			return false;
		}
		if (!ReadFloat(out vector.x) ||
			!ReadFloat(out vector.y) ||
			!ReadFloat(out vector.z))
		{
			return false;
		}
		return true;
	}

	public void WriteVector4(Vector4 vector)
	{
		WriteFloat(vector.x);
		WriteFloat(vector.y);
		WriteFloat(vector.z);
		WriteFloat(vector.w);
	}
	public bool ReadVector4(out Vector4 vector)
	{
		vector = Vector4.zero;
		if (this.readCursor + 8 > this.writeCursor)
		{
			return false;
		}
		if (!ReadFloat(out vector.x) ||
			!ReadFloat(out vector.y) ||
			!ReadFloat(out vector.z) ||
			!ReadFloat(out vector.w))
		{
			return false;
		}
		return true;
	}
	#endregion

	#region Quaternion
	public void WriteQuaternion(Quaternion quaternion)
	{
		WriteFloat(quaternion.x);
		WriteFloat(quaternion.y);
		WriteFloat(quaternion.z);
		WriteFloat(quaternion.w);
	}
	public bool ReadQuaternion(out Quaternion quaternion)
	{
		quaternion = Quaternion.identity;
		if (this.readCursor + 8 > this.writeCursor)
		{
			return false;
		}
		if (!ReadFloat(out quaternion.x) ||
			!ReadFloat(out quaternion.y) ||
			!ReadFloat(out quaternion.z) ||
			!ReadFloat(out quaternion.w))
		{
			return false;
		}
		return true;
	}
	#endregion

	#region Color
	public void WriteColor(Color color)
	{
		WriteFloat(color.r);
		WriteFloat(color.g);
		WriteFloat(color.b);
		WriteFloat(color.a);
	}
	public bool ReadColor(out Color color)
	{
		color = Color.white;
		if (this.readCursor + 8 > this.writeCursor)
		{
			return false;
		}
		if (!ReadFloat(out color.r) ||
			!ReadFloat(out color.g) ||
			!ReadFloat(out color.b) ||
			!ReadFloat(out color.a))
		{
			return false;
		}
		return true;
	}
	#endregion

	//ByteBuffer read/write
	public static bool ReadFromDisk(string path, out ByteBuffer dataBuffer)
	{
		const int STREAM_READ_LENGTH = short.MaxValue;

		dataBuffer = null;
		if (path == null || path.Length < 1)
		{
			return false;
		}

		if (!File.Exists(path))
		{
			return false;
		}

		using (BinaryReader br = new BinaryReader(File.OpenRead(path)))
		{
			int remaining = (int)br.BaseStream.Length;
			int index = 0;
			byte[] buffer = new byte[remaining];
			while (remaining > 0)
			{
				int numBytesToRead = (remaining < STREAM_READ_LENGTH) ? (int)remaining : STREAM_READ_LENGTH;
				while (numBytesToRead > 0)
				{
					int bytesRead = br.Read(buffer, index, numBytesToRead);
					if (bytesRead == 0)
					{
						break;
					}
					index += bytesRead;
					numBytesToRead -= bytesRead;
					remaining -= bytesRead;
				}
			}
			dataBuffer = new ByteBuffer(buffer);
		}

		if (dataBuffer == null || dataBuffer.Length < 1)
		{
			return false;
		}
		return true;
	}
	public void WriteToDisk(string fullPath)
	{
		WriteToDisk(fullPath, false);
	}
	public void WriteToDisk(string path, string fileName)
	{
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		WriteToDisk(path + fileName, false);
	}
	public void WriteToDisk(string path, string fileName, bool overwrite)
	{
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		WriteToDisk(path + fileName, overwrite);
	}
	public void WriteToDisk(string fullPath, bool overwrite)
	{
		if (fullPath == null || fullPath.Length < 1)
		{
			//Log.Message(LogLevel.Error, "FullPath cannot be null");
		}
		if (!overwrite && File.Exists(fullPath))
		{
			File.Delete(fullPath);
		}
		using (BinaryWriter bw = new BinaryWriter(File.Open(fullPath, FileMode.OpenOrCreate)))
		{
			bw.Write(this.data, 0, Length);
		}
	}
}