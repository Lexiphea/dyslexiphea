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

	private void CheckEndianness(byte[] array)
	{
		if (!BitConverter.IsLittleEndian)
		{
			Array.Reverse(array);
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
		CheckEndianness(value);
		this.data.Add(name, new DataPair(value, type));
	}

	public void Add(string name, Guid value)
	{
		Add(name, value.ToByteArray(), typeof(Guid));
	}

	public void Add(string name, DateTime value)
	{
		Add(name, BitConverter.GetBytes(value.ToBinary()), typeof(DateTime));
	}

	public void Add(string name, string value)
	{
		if (value == null)
		{
			value = "";
		}
		byte[] bytes = Encoding.ASCII.GetBytes(value);
		Add(name, bytes, typeof(string));
	}

	public void Add(string name, char value)
	{
		Add(name, BitConverter.GetBytes(value), typeof(char));
	}

	public void Add(string name, byte value)
	{
		Add(name, BitConverter.GetBytes(value), typeof(byte));
	}

	public void Add(string name, sbyte value)
	{
		Add(name, BitConverter.GetBytes(value), typeof(sbyte));
	}

	public void Add(string name, short value)
	{
		Add(name, BitConverter.GetBytes(value), typeof(short));
	}

	public void Add(string name, ushort value)
	{
		Add(name, BitConverter.GetBytes(value), typeof(ushort));
	}

	public void Add(string name, int value)
	{
		Add(name, BitConverter.GetBytes(value), typeof(int));
	}

	public void Add(string name, uint value)
	{
		Add(name, BitConverter.GetBytes(value), typeof(uint));
	}

	public void Add(string name, long value)
	{
		Add(name, BitConverter.GetBytes(value), typeof(long));
	}

	public void Add(string name, ulong value)
	{
		Add(name, BitConverter.GetBytes(value), typeof(ulong));
	}

	public void Add(string name, bool value)
	{
		Add(name, BitConverter.GetBytes(value), typeof(bool));
	}

	public void Add(string name, float value)
	{
		Add(name, BitConverter.GetBytes(value), typeof(float));
	}

	public void Add(string name, double value)
	{
		Add(name, BitConverter.GetBytes(value), typeof(double));
	}

	public void Add(string name, Vector2 value)
	{
		List<byte> bytes = new List<byte>();
		bytes.AddRange(BitConverter.GetBytes(value.x));
		bytes.AddRange(BitConverter.GetBytes(value.y));
		Add(name, bytes.ToArray(), typeof(Vector2));
	}

	public void Add(string name, Vector3 value)
	{
		List<byte> bytes = new List<byte>();
		bytes.AddRange(BitConverter.GetBytes(value.x));
		bytes.AddRange(BitConverter.GetBytes(value.y));
		bytes.AddRange(BitConverter.GetBytes(value.z));
		Add(name, bytes.ToArray(), typeof(Vector3));
	}

	public void Add(string name, Quaternion value)
	{
		List<byte> bytes = new List<byte>();
		bytes.AddRange(BitConverter.GetBytes(value.x));
		bytes.AddRange(BitConverter.GetBytes(value.y));
		bytes.AddRange(BitConverter.GetBytes(value.z));
		bytes.AddRange(BitConverter.GetBytes(value.w));
		Add(name, bytes.ToArray(), typeof(Quaternion));
	}

	public void Add(string name, Color value)
	{
		List<byte> bytes = new List<byte>();
		bytes.AddRange(BitConverter.GetBytes(value.r));
		bytes.AddRange(BitConverter.GetBytes(value.g));
		bytes.AddRange(BitConverter.GetBytes(value.b));
		bytes.AddRange(BitConverter.GetBytes(value.a));
		Add(name, bytes.ToArray(), typeof(Color));
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
		CheckEndianness(pair.Data);
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
		CheckEndianness(pair.Data);
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
		CheckEndianness(pair.Data);
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
		CheckEndianness(pair.Data);
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
		CheckEndianness(pair.Data);
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
		CheckEndianness(pair.Data);
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
		CheckEndianness(pair.Data);
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
		CheckEndianness(pair.Data);
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
		CheckEndianness(pair.Data);
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
		CheckEndianness(pair.Data);
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
		CheckEndianness(pair.Data);
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
		CheckEndianness(pair.Data);
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
		CheckEndianness(pair.Data);
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
		CheckEndianness(pair.Data);
		return new TinyColor(pair.Data[0], pair.Data[1], pair.Data[2], pair.Data[3]);
	}

	public static bool Read(DataBuffer buffer, out Serializer data)
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

	public void Write(DataBuffer buffer)
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