using System;
using System.Text;
using System.IO;
using UnityEngine;

public class ByteBuffer
{
	private const int DefaultBufferSize = 256;
	//protected static MessageLog Log = new MessageLog("[ByteBuffer]");

	private byte[] data = null;
	private int writeOffset = 0;
	private int readOffset = 0;
	private bool isFixedSize = false;

	public ByteBuffer()
	{
		this.data = new byte[DefaultBufferSize];
	}

	public ByteBuffer(byte[] bytes)
	{
		this.data = new byte[bytes.Length];
		ByteUtility.WriteRawBytes(bytes, 0, bytes.Length, ref this.data, ref this.writeOffset);
	}

	internal ByteBuffer(byte[] bytes, int start, int length)
	{
		this.data = new byte[length];
		ByteUtility.WriteRawBytes(bytes, start, length, ref this.data, ref this.writeOffset);
	}

	public byte[] Data
	{
		get
		{
			return this.data;
		}
		set
		{
			this.data = value; this.writeOffset = this.data.Length;
		}
	}

	public int Length
	{
		get
		{
			return this.writeOffset;
		}
		set
		{
			this.writeOffset = value;
		}
	}

	public int WriteOffset
	{
		get
		{
			return this.writeOffset;
		}
		set
		{
			this.writeOffset = value;
		}
	}

	public int ReadOffset
	{
		get
		{
			return this.readOffset;
		}
		set
		{
			this.readOffset = value;
		}
	}

	public int ReadRemaining
	{
		get
		{
			return this.writeOffset - this.readOffset;
		}
	}

	public bool IsFixedSize
	{
		get
		{
			return this.isFixedSize;
		}
	}

	public void Rewind(int position)
	{
		if (position < 0)
		{
			position = 0;
		}
		else if (position > this.writeOffset)
		{
			position = this.writeOffset;
		}
		this.readOffset = position;
	}

	public void WriteSkip(int length)
	{
		ByteUtility.EnsureCapacity(ref this.data, this.writeOffset, length);
		this.writeOffset += length;
	}

	public bool ReadSkip(int length)
	{
		if (this.readOffset + length > this.writeOffset)
		{
			return false;
		}
		this.readOffset += length;
		return true;
	}

	/// <summary>
	/// Writes all of the currently written ByteBuffer data to a new byte array and clears the ByteBuffer. The new array is returned.
	/// </summary>
	public byte[] Flush()
	{
		return Flush(true);
	}

	/// <summary>
	/// Writes all of the currently written ByteBuffer data to a new byte array and clears the ByteBuffer if clearBuffer is true. The new array is returned.
	/// </summary>
	public byte[] Flush(bool clearBuffer)
	{
		byte[] newBuffer = new byte[this.writeOffset];
		Buffer.BlockCopy(this.data, 0, newBuffer, 0, this.writeOffset);
		if (clearBuffer)
		{
			this.writeOffset = 0;
			this.readOffset = 0;
		}
		return newBuffer;
	}

	#region Bool
	public void WriteBool(bool value)
	{
		ByteUtility.WriteBool(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadBool(out bool value)
	{
		return ByteUtility.ReadBool(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region Byte
	public void WriteByte(byte value)
	{
		ByteUtility.WriteByte(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadByte(out byte value)
	{
		return ByteUtility.ReadByte(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region SByte
	public void WriteSByte(sbyte value)
	{
		ByteUtility.WriteSByte(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadSByte(out sbyte value)
	{
		return ByteUtility.ReadSByte(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region RawBytes
	public void WriteRawBytes(byte[] bytes)
	{
		ByteUtility.WriteRawBytes(bytes, ref this.data, ref this.writeOffset);
	}

	public void WriteRawBytes(byte[] bytes, int start, int length)
	{
		ByteUtility.WriteRawBytes(bytes, start, length, ref this.data, ref this.writeOffset);
	}

	public bool ReadRawBytes(out byte[] bytes, int length)
	{
		return ByteUtility.ReadRawBytes(ref this.data, ref this.readOffset, out bytes, length);
	}
	#endregion

	#region Bytes
	public void WriteBytes(byte[] bytes)
	{
		ByteUtility.WriteBytes(bytes, ref this.data, ref this.writeOffset);
	}

	public void WriteBytes(byte[] bytes, int start, int length)
	{
		ByteUtility.WriteBytes(bytes, start, length, ref this.data, ref this.writeOffset);
	}

	public bool ReadBytes(out byte[] bytes)
	{
		return ByteUtility.ReadBytes(ref this.data, ref this.readOffset, out bytes);
	}
	#endregion

	#region UShort
	public void WriteUShort(ushort value)
	{
		ByteUtility.WriteUShort(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadUShort(out ushort value)
	{
		return ByteUtility.ReadUShort(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region Short
	public void WriteShort(short value)
	{
		ByteUtility.WriteShort(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadShort(out short value)
	{
		return ByteUtility.ReadShort(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region Float
	public void WriteFloat(float value)
	{
		ByteUtility.WriteFloat(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadFloat(out float value)
	{
		return ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region UInt
	public void WriteUIntArray(uint[] value)
	{
		ByteUtility.WriteUIntArray(value, ref this.data, ref this.writeOffset);
	}

	public void WriteUInt(uint value)
	{
		ByteUtility.WriteUInt(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadUInt(out uint value)
	{
		return ByteUtility.ReadUInt(ref this.data, ref this.readOffset, out value);
	}

	public bool ReadUIntArray(out uint[] value)
	{
		return ByteUtility.ReadUIntArray(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region Int
	public void WriteInt(int value)
	{
		ByteUtility.WriteInt(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadInt(out int value)
	{
		return ByteUtility.ReadInt(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region ULong
	public void WriteULong(ulong value)
	{
		ByteUtility.WriteULong(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadULong(out ulong value)
	{
		return ByteUtility.ReadULong(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region Long
	public void WriteLong(long value)
	{
		ByteUtility.WriteLong(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadLong(out long value)
	{
		return ByteUtility.ReadLong(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region Double
	public void WriteDouble(double value)
	{
		ByteUtility.WriteDouble(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadDouble(out double value)
	{
		return ByteUtility.ReadDouble(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region Guid
	public void WriteGuid(Guid value)
	{
		ByteUtility.WriteGuid(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadGuid(out Guid value)
	{
		return ByteUtility.ReadGuid(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region GuidArray
	public void WriteGuidArray(Guid[] value)
	{
		ByteUtility.WriteGuidArray(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadGuidArray(out Guid[] value)
	{
		return ByteUtility.ReadGuidArray(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region String
	public void WriteString(string value)
	{
		ByteUtility.WriteString(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadString(out string value)
	{
		return ByteUtility.ReadString(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region StringArray
	public void WriteStringArray(string[] value)
	{
		ByteUtility.WriteStringArray(value, ref this.data, ref this.writeOffset);
	}

	public bool ReadStringArray(out string[] value)
	{
		return ByteUtility.ReadStringArray(ref this.data, ref this.readOffset, out value);
	}
	#endregion

	#region PaddedString
	public void WritePaddedString(string value, int length)
	{
		ByteUtility.WritePaddedString(value, length, ref this.data, ref this.writeOffset);
	}

	public bool ReadPaddedString(out string value, int length)
	{
		return ByteUtility.ReadPaddedString(ref this.data, ref this.readOffset, out value, length);
	}
	#endregion

	#region DateTime
	public void WriteDateTime(DateTime dateTime)
	{
		long s = dateTime.ToBinary();
		ByteUtility.WriteLong(s, ref this.data, ref this.writeOffset);
	}

	public bool ReadDateTime(out DateTime dateTime)
	{
		long tmp;
		if (!ByteUtility.ReadLong(ref this.data, ref this.readOffset, out tmp))
		{
			dateTime = DateTime.Now;
			return false;
		}
		dateTime = DateTime.FromBinary(tmp);
		return true;
	}
	#endregion

	#region Vector2
	public void WriteVector2(Vector2 vector)
	{
		ByteUtility.WriteFloat(vector.x, ref this.data, ref this.writeOffset);
		ByteUtility.WriteFloat(vector.y, ref this.data, ref this.writeOffset);
	}

	public bool ReadVector2(out Vector2 vector)
	{
		vector = Vector2.zero;
		if (!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out vector.x) ||
			!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out vector.y))
		{
			return false;
		}
		return true;
	}
	#endregion

	#region Vector3
	public void WriteVector3(Vector3 vector)
	{
		ByteUtility.WriteFloat(vector.x, ref this.data, ref this.writeOffset);
		ByteUtility.WriteFloat(vector.y, ref this.data, ref this.writeOffset);
		ByteUtility.WriteFloat(vector.z, ref this.data, ref this.writeOffset);
	}

	public bool ReadVector3(out Vector3 vector)
	{
		vector = Vector3.zero;
		if (!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out vector.x) ||
			!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out vector.y) ||
			!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out vector.z))
		{
			return false;
		}
		return true;
	}
	#endregion

	#region Vector4
	public void WriteVector4(Vector4 vector)
	{
		ByteUtility.WriteFloat(vector.x, ref this.data, ref this.writeOffset);
		ByteUtility.WriteFloat(vector.y, ref this.data, ref this.writeOffset);
		ByteUtility.WriteFloat(vector.z, ref this.data, ref this.writeOffset);
		ByteUtility.WriteFloat(vector.w, ref this.data, ref this.writeOffset);
	}

	public bool ReadVector4(out Vector4 vector)
	{
		vector = Vector4.zero;
		if (!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out vector.x) ||
			!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out vector.y) ||
			!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out vector.z) ||
			!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out vector.w))
		{
			return false;
		}
		return true;
	}
	#endregion

	#region Quaternion
	public void WriteQuaternion(Quaternion quaternion)
	{
		ByteUtility.WriteFloat(quaternion.x, ref this.data, ref this.writeOffset);
		ByteUtility.WriteFloat(quaternion.y, ref this.data, ref this.writeOffset);
		ByteUtility.WriteFloat(quaternion.z, ref this.data, ref this.writeOffset);
		ByteUtility.WriteFloat(quaternion.w, ref this.data, ref this.writeOffset);
	}
	public bool ReadQuaternion(out Quaternion quaternion)
	{
		quaternion = Quaternion.identity;
		if (!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out quaternion.x) ||
			!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out quaternion.y) ||
			!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out quaternion.z) ||
			!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out quaternion.w))
		{
			return false;
		}
		return true;
	}
	#endregion

	#region Color
	public void WriteColor(Color color)
	{
		ByteUtility.WriteFloat(color.r, ref this.data, ref this.writeOffset);
		ByteUtility.WriteFloat(color.g, ref this.data, ref this.writeOffset);
		ByteUtility.WriteFloat(color.b, ref this.data, ref this.writeOffset);
		ByteUtility.WriteFloat(color.a, ref this.data, ref this.writeOffset);
	}
	public bool ReadColor(out Color color)
	{
		color = Color.white;
		if (!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out color.r) ||
			!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out color.g) ||
			!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out color.b) ||
			!ByteUtility.ReadFloat(ref this.data, ref this.readOffset, out color.a))
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