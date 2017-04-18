using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Serializer : ISerializer
{
	private Dictionary<string, DataPair> data = new Dictionary<string, DataPair>();

	private void Add(string key, DataPair data)
	{
		if (this.data.ContainsKey(key))
		{
			throw new ArgumentException("SerializedData does not support duplicate keys");
		}
		this.data.Add(key, data);
	}

	public int Count
	{
		get
		{
			return this.data.Count;
		}
	}

	private void Add(string name, byte[] value, Type type)
	{
		if (name == null || name.Length < 1)
		{
			throw new ArgumentException("Name cannot be null");
		}
		if (value == null)
		{
			throw new ArgumentException("Value cannot be null");
		}
		if (type == null)
		{
			throw new ArgumentException("Type cannot be null");
		}
		if (this.data.ContainsKey(name))
		{
			throw new ArgumentException("Serializer does not support duplicate keys " + name);
		}
		this.data.Add(name, new DataPair(value, type));
	}

	public void Add(string name, char value)
	{
		Add(name, new byte[] { (byte)value }, typeof(char));
	}

	public void Add(string name, byte value)
	{
		Add(name, new byte[] { value }, typeof(byte));
	}

	public void Add(string name, sbyte value)
	{
		byte[] bytes = new byte[2];
		int writeOffset = 0;
		ByteUtility.WriteSByte(value, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(sbyte));
	}

	public void Add(string name, bool value)
	{
		Add(name, new byte[] { (byte)(value ? 1 : 0) }, typeof(bool));
	}

	public void Add(string name, short value)
	{
		byte[] bytes = new byte[2];
		int writeOffset = 0;
		ByteUtility.WriteShort(value, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(short));
	}

	public void Add(string name, ushort value)
	{
		byte[] bytes = new byte[2];
		int writeOffset = 0;
		ByteUtility.WriteUShort(value, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(ushort));
	}

	public void Add(string name, int value)
	{
		byte[] bytes = new byte[4];
		int writeOffset = 0;
		ByteUtility.WriteInt(value, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(int));
	}

	public void Add(string name, uint value)
	{
		byte[] bytes = new byte[4];
		int writeOffset = 0;
		ByteUtility.WriteUInt(value, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(uint));
	}

	public void Add(string name, float value)
	{
		byte[] bytes = new byte[4];
		int writeOffset = 0;
		ByteUtility.WriteFloat(value, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(float));
	}

	public void Add(string name, long value)
	{
		byte[] bytes = new byte[8];
		int writeOffset = 0;
		ByteUtility.WriteLong(value, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(long));
	}

	public void Add(string name, ulong value)
	{
		byte[] bytes = new byte[8];
		int writeOffset = 0;
		ByteUtility.WriteULong(value, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(ulong));
	}

	public void Add(string name, double value)
	{
		byte[] bytes = new byte[8];
		int writeOffset = 0;
		ByteUtility.WriteDouble(value, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(double));
	}

	public void Add(string name, string value)
	{
		Add(name, value, Encoding.Unicode);
	}

	public void Add(string name, string value, Encoding encoding)
	{
		if (value == null)
		{
			value = "";
		}
		byte[] rawString = encoding.GetBytes(value);
		byte[] bytes = new byte[4 + rawString.Length];
		int writeOffset = 0;
		ByteUtility.WriteBytes(rawString, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(string));
	}

	public void Add(string name, Guid value)
	{
		byte[] bytes = new byte[16];
		int writeOffset = 0;
		ByteUtility.WriteGuid(value, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(Guid));
	}

	public void Add(string name, DateTime value)
	{
		byte[] bytes = new byte[8];
		int writeOffset = 0;
		ByteUtility.WriteDateTime(value, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(DateTime));
	}

	public void Add(string name, Vector2 value)
	{
		byte[] bytes = new byte[8];
		int writeOffset = 0;
		ByteUtility.WriteFloat(value.x, ref bytes, ref writeOffset);
		ByteUtility.WriteFloat(value.y, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(Vector2));
	}

	public void Add(string name, Vector3 value)
	{
		byte[] bytes = new byte[12];
		int writeOffset = 0;
		ByteUtility.WriteFloat(value.x, ref bytes, ref writeOffset);
		ByteUtility.WriteFloat(value.y, ref bytes, ref writeOffset);
		ByteUtility.WriteFloat(value.z, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(Vector3));
	}

	public void Add(string name, Quaternion value)
	{
		byte[] bytes = new byte[16];
		int writeOffset = 0;
		ByteUtility.WriteFloat(value.x, ref bytes, ref writeOffset);
		ByteUtility.WriteFloat(value.y, ref bytes, ref writeOffset);
		ByteUtility.WriteFloat(value.z, ref bytes, ref writeOffset);
		ByteUtility.WriteFloat(value.w, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(Quaternion));
	}

	public void Add(string name, Color value)
	{
		byte[] bytes = new byte[16];
		int writeOffset = 0;
		ByteUtility.WriteFloat(value.r, ref bytes, ref writeOffset);
		ByteUtility.WriteFloat(value.g, ref bytes, ref writeOffset);
		ByteUtility.WriteFloat(value.b, ref bytes, ref writeOffset);
		ByteUtility.WriteFloat(value.a, ref bytes, ref writeOffset);
		Add(name, bytes, typeof(Color));
	}

	public void Add(string name, TinyColor value)
	{
		Add(name, new byte[] { value.r, value.g, value.b, value.a }, typeof(TinyColor));
	}

	public Guid GetGuid(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return Guid.Empty;
		}
		if (pair.Type != typeof(Guid).Name)
		{
			throw new ArgumentException("Type is not typeof(int)");
		}
		return new Guid(pair.Data);
	}

	public DateTime GetDateTime(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return DateTime.Now;
		}
		if (pair.Type != typeof(DateTime).Name)
		{
			throw new ArgumentException("Type is not typeof(DateTime)");
		}
		return DateTime.FromBinary(BitConverter.ToInt64(pair.Data, 0));
	}

	public string GetString(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return "";
		}
		if (pair.Type != typeof(string).Name)
		{
			throw new ArgumentException("Type is not typeof(string)");
		}
		if (pair.Data == null || pair.Data.Length < 1)
		{
			return "";
		}
		return Encoding.ASCII.GetString(pair.Data, 0, pair.Data.Length);
	}

	public char GetChar(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(char);
		}
		if (pair.Type != typeof(char).Name)
		{
			throw new ArgumentException("Type is not typeof(char)");
		}
		return BitConverter.ToChar(pair.Data, 0);
	}

	public byte GetByte(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(byte);
		}
		if (pair.Type != typeof(byte).Name)
		{
			throw new ArgumentException("Type is not typeof(byte)");
		}
		return pair.Data[0];
	}

	public sbyte GetSByte(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(sbyte);
		}
		if (pair.Type != typeof(sbyte).Name)
		{
			throw new ArgumentException("Type is not typeof(sbyte)");
		}
		return (sbyte)pair.Data[0];
	}

	public short GetShort(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(short);
		}
		if (pair.Type != typeof(short).Name)
		{
			throw new ArgumentException("Type is not typeof(short)");
		}
		return BitConverter.ToInt16(pair.Data, 0);
	}

	public ushort GetUShort(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(ushort);
		}
		if (pair.Type != typeof(ushort).Name)
		{
			throw new ArgumentException("Type is not typeof(ushort)");
		}
		return BitConverter.ToUInt16(pair.Data, 0);
	}

	public int GetInt(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(int);
		}
		if (pair.Type != typeof(int).Name)
		{
			throw new ArgumentException("Type is not typeof(int)");
		}
		return BitConverter.ToInt32(pair.Data, 0);
	}

	public uint GetUInt(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(uint);
		}
		if (pair.Type != typeof(uint).Name)
		{
			throw new ArgumentException("Type is not typeof(uint)");
		}
		return BitConverter.ToUInt32(pair.Data, 0);
	}

	public long GetLong(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(long);
		}
		if (pair.Type != typeof(long).Name)
		{
			throw new ArgumentException("Type is not typeof(long)");
		}
		return BitConverter.ToInt64(pair.Data, 0);
	}

	public ulong GetULong(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(ulong);
		}
		if (pair.Type != typeof(ulong).Name)
		{
			throw new ArgumentException("Type is not typeof(ulong)");
		}
		return BitConverter.ToUInt64(pair.Data, 0);
	}

	public bool GetBool(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(bool);
		}
		if (pair.Type != typeof(bool).Name)
		{
			throw new ArgumentException("Type is not typeof(bool)");
		}
		return BitConverter.ToBoolean(pair.Data, 0);
	}

	public float GetFloat(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(float);
		}
		if (pair.Type != typeof(float).Name)
		{
			throw new ArgumentException("Type is not typeof(float)");
		}
		return BitConverter.ToSingle(pair.Data, 0);
	}

	public double GetDouble(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(double);
		}
		if (pair.Type != typeof(double).Name)
		{
			throw new ArgumentException("Type is not typeof(double)");
		}
		return BitConverter.ToDouble(pair.Data, 0);
	}

	public Vector2 GetVector2(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return Vector2.zero;
		}
		if (pair.Type != typeof(Vector2).Name)
		{
			throw new ArgumentException("Type is not typeof(Vector2)");
		}
		return new Vector2(BitConverter.ToSingle(pair.Data, 0), BitConverter.ToSingle(pair.Data, 4));
	}

	public Vector3 GetVector3(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return Vector3.zero;
		}
		if (pair.Type != typeof(Vector3).Name)
		{
			throw new ArgumentException("Type is not typeof(Vector3)");
		}
		return new Vector3(BitConverter.ToSingle(pair.Data, 0), BitConverter.ToSingle(pair.Data, 4), BitConverter.ToSingle(pair.Data, 8));
	}

	public Quaternion GetQuaternion(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return Quaternion.identity;
		}
		if (pair.Type != typeof(Quaternion).Name)
		{
			throw new ArgumentException("Type is not typeof(Quaternion)");
		}
		return new Quaternion(BitConverter.ToSingle(pair.Data, 0), BitConverter.ToSingle(pair.Data, 4), BitConverter.ToSingle(pair.Data, 8), BitConverter.ToSingle(pair.Data, 12));
	}

	public Color GetColor(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return Color.white;
		}
		if (pair.Type != typeof(Color).Name)
		{
			throw new ArgumentException("Type is not typeof(Color)");
		}
		return new Color(BitConverter.ToSingle(pair.Data, 0), BitConverter.ToSingle(pair.Data, 4), BitConverter.ToSingle(pair.Data, 8), BitConverter.ToSingle(pair.Data, 12));
	}

	public TinyColor GetTinyColor(string name)
	{
		DataPair pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return TinyColor.white;
		}
		if (pair.Type != typeof(TinyColor).Name)
		{
			throw new ArgumentException("Type is not typeof(TinyColor)");
		}
		return new TinyColor(pair.Data[0], pair.Data[1], pair.Data[2], pair.Data[3]);
	}

	public static bool Read(ByteBuffer buffer, out Serializer data)
	{
		data = new Serializer();
		int count;
		if (!buffer.ReadInt(out count))
		{
			return false;
		}
		for (int i = 0; i < count; ++i)
		{
			string key;
			DataPair pair;
			if (!buffer.ReadString(out key) ||
				!DataPair.Read(out pair, buffer))
			{
				return false;
			}
			data.Add(key, pair);
		}
		return true;
	}

	public void Write(ByteBuffer buffer)
	{
		buffer.WriteInt(this.data.Count);
		if (this.data.Count < 1)
		{
			return;
		}
		//pBuffer.WriteString("-------------NEW OBJECT-------------\r\n");
		foreach (KeyValuePair<string, DataPair> pair in this.data)
		{
			//pBuffer.WriteString("[VariableName:" + pair.Key + "]");
			buffer.WriteString(pair.Key);
			pair.Value.Write(buffer);
		}
	}
}