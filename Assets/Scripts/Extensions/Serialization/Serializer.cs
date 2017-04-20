using System;
using System.Collections.Generic;
using System.Text;

public class Serializer : ISerializer
{
	private Dictionary<string, Tuple<byte[], string>> data = new Dictionary<string, Tuple<byte[], string>>();

	private void Add(string key, Tuple<byte[], string> pair)
	{
		if (this.data.ContainsKey(key))
		{
			throw new ArgumentException("SerializedData does not support duplicate keys");
		}
		this.data.Add(key, pair);
	}

	public int Count
	{
		get
		{
			return this.data.Count;
		}
	}

	public Serializer()
	{
	}

	public Serializer(ISerializable serializable)
	{
		serializable.Serialize(this);
	}

	public void Serialize(ISerializable serializable)
	{
		serializable.Serialize(this);
	}

	public void Deserialize(ISerializable serializable)
	{
		serializable.Deserialize(this);
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
		this.data.Add(name, new Tuple<byte[], string>(value, type.Name));
	}

	public void Add(string name, bool value)
	{
		Add(name, new byte[] { (byte)(value ? 1 : 0) }, typeof(bool));
	}

	public void Add(string name, byte value)
	{
		Add(name, new byte[] { value }, typeof(byte));
	}

	public void Add(string name, sbyte value)
	{
		byte[] bytes = new byte[2];
		LittleEndianWriter.WriteSByte(value, ref bytes);
		Add(name, bytes, typeof(sbyte));
	}

	public void Add(string name, char value)
	{
		byte[] bytes = new byte[2];
		LittleEndianWriter.WriteChar(value, ref bytes);
		Add(name, bytes, typeof(char));
	}

	public void Add(string name, ushort value)
	{
		byte[] bytes = new byte[2];
		LittleEndianWriter.WriteUShort(value, ref bytes);
		Add(name, bytes, typeof(ushort));
	}

	public void Add(string name, short value)
	{
		byte[] bytes = new byte[2];
		LittleEndianWriter.WriteShort(value, ref bytes);
		Add(name, bytes, typeof(short));
	}

	public void Add(string name, uint value)
	{
		byte[] bytes = new byte[4];
		LittleEndianWriter.WriteUInt(value, ref bytes);
		Add(name, bytes, typeof(uint));
	}

	public void Add(string name, int value)
	{
		byte[] bytes = new byte[4];
		LittleEndianWriter.WriteInt(value, ref bytes);
		Add(name, bytes, typeof(int));
	}

	public void Add(string name, ulong value)
	{
		byte[] bytes = new byte[8];
		LittleEndianWriter.WriteULong(value, ref bytes);
		Add(name, bytes, typeof(ulong));
	}

	public void Add(string name, long value)
	{
		byte[] bytes = new byte[8];
		LittleEndianWriter.WriteLong(value, ref bytes);
		Add(name, bytes, typeof(long));
	}

	public void Add(string name, float value)
	{
		byte[] bytes = new byte[4];
		LittleEndianWriter.WriteFloat(value, ref bytes);
		Add(name, bytes, typeof(float));
	}

	public void Add(string name, double value)
	{
		byte[] bytes = new byte[8];
		LittleEndianWriter.WriteDouble(value, ref bytes);
		Add(name, bytes, typeof(double));
	}

	public void Add(string name, decimal value)
	{
		byte[] bytes = new byte[16];
		LittleEndianWriter.WriteDecimal(value, ref bytes);
		Add(name, bytes, typeof(decimal));
	}

	public void Add(string name, string value)
	{
		Add(name, value, Encoding.Unicode);
	}

	public void Add(string name, string value, Encoding encoding)
	{
		if (value == null)
		{
			value = default(string);
		}
		byte[] rawString = encoding.GetBytes(value);
		byte[] bytes = new byte[4 + rawString.Length];
		LittleEndianWriter.WriteBytes(rawString, ref bytes);
		Add(name, bytes, typeof(string));
	}

	public void Add(string name, Guid value)
	{
		byte[] bytes = new byte[16];
		LittleEndianWriter.WriteGuid(value, ref bytes);
		Add(name, bytes, typeof(Guid));
	}

	public void Add(string name, DateTime value)
	{
		byte[] bytes = new byte[8];
		LittleEndianWriter.WriteDateTime(value, ref bytes);
		Add(name, bytes, typeof(DateTime));
	}

	public bool GetBool(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(bool);
		}
		if (pair.Item2 != typeof(bool).Name)
		{
			throw new ArgumentException("Type is not typeof(bool)");
		}
		bool value;
		if (!LittleEndianReader.ReadBool(pair.Item1, out value))
		{
			value = default(bool);
		}
		return value;
	}

	public byte GetByte(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(byte);
		}
		if (pair.Item2 != typeof(byte).Name)
		{
			throw new ArgumentException("Type is not typeof(byte)");
		}
		byte value;
		if (!LittleEndianReader.ReadByte(pair.Item1, out value))
		{
			value = default(byte);
		}
		return value;
	}

	public sbyte GetSByte(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(sbyte);
		}
		if (pair.Item2 != typeof(sbyte).Name)
		{
			throw new ArgumentException("Type is not typeof(sbyte)");
		}
		sbyte value;
		if (!LittleEndianReader.ReadSByte(pair.Item1, out value))
		{
			value = default(sbyte);
		}
		return value;
	}

	public char GetChar(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(char);
		}
		if (pair.Item2 != typeof(char).Name)
		{
			throw new ArgumentException("Type is not typeof(char)");
		}
		char value;
		if (!LittleEndianReader.ReadChar(pair.Item1, out value))
		{
			value = default(char);
		}
		return value;
	}

	public ushort GetUShort(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(ushort);
		}
		if (pair.Item2 != typeof(ushort).Name)
		{
			throw new ArgumentException("Type is not typeof(ushort)");
		}
		ushort value;
		if (!LittleEndianReader.ReadUShort(pair.Item1, out value))
		{
			value = default(ushort);
		}
		return value;
	}

	public short GetShort(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(short);
		}
		if (pair.Item2 != typeof(short).Name)
		{
			throw new ArgumentException("Type is not typeof(short)");
		}
		short value;
		if (!LittleEndianReader.ReadShort(pair.Item1, out value))
		{
			value = default(short);
		}
		return value;
	}

	public uint GetUInt(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(uint);
		}
		if (pair.Item2 != typeof(uint).Name)
		{
			throw new ArgumentException("Type is not typeof(uint)");
		}
		uint value;
		if (!LittleEndianReader.ReadUInt(pair.Item1, out value))
		{
			value = default(uint);
		}
		return value;
	}

	public int GetInt(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(int);
		}
		if (pair.Item2 != typeof(int).Name)
		{
			throw new ArgumentException("Type is not typeof(int)");
		}
		int value;
		if (!LittleEndianReader.ReadInt(pair.Item1, out value))
		{
			value = default(int);
		}
		return value;
	}

	public ulong GetULong(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(ulong);
		}
		if (pair.Item2 != typeof(ulong).Name)
		{
			throw new ArgumentException("Type is not typeof(ulong)");
		}
		ulong value;
		if (!LittleEndianReader.ReadULong(pair.Item1, out value))
		{
			value = default(ulong);
		}
		return value;
	}

	public long GetLong(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(long);
		}
		if (pair.Item2 != typeof(long).Name)
		{
			throw new ArgumentException("Type is not typeof(long)");
		}
		long value;
		if (!LittleEndianReader.ReadLong(pair.Item1, out value))
		{
			value = default(long);
		}
		return value;
	}

	public float GetFloat(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(float);
		}
		if (pair.Item2 != typeof(float).Name)
		{
			throw new ArgumentException("Type is not typeof(float)");
		}
		float value;
		if (!LittleEndianReader.ReadFloat(pair.Item1, out value))
		{
			value = default(float);
		}
		return value;
	}

	public double GetDouble(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(double);
		}
		if (pair.Item2 != typeof(double).Name)
		{
			throw new ArgumentException("Type is not typeof(double)");
		}
		double value;
		if (!LittleEndianReader.ReadDouble(pair.Item1, out value))
		{
			value = default(double);
		}
		return value;
	}

	public decimal GetDecimal(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(decimal);
		}
		if (pair.Item2 != typeof(decimal).Name)
		{
			throw new ArgumentException("Type is not typeof(decimal)");
		}
		decimal value;
		if (!LittleEndianReader.ReadDecimal(pair.Item1, out value))
		{
			value = default(decimal);
		}
		return value;
	}

	public string GetString(string name)
	{
		return GetString(name, Encoding.Unicode);
	}

	public string GetString(string name, Encoding encoding)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return default(string);
		}
		if (pair.Item2 != typeof(string).Name)
		{
			throw new ArgumentException("Type is not typeof(string)");
		}
		if (pair.Item1 == null || pair.Item1.Length < 1)
		{
			return default(string);
		}
		string value;
		if (!LittleEndianReader.ReadString(pair.Item1, encoding, out value))
		{
			value = default(string);
		}
		return value;
	}

	public Guid GetGuid(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return Guid.Empty;
		}
		if (pair.Item2 != typeof(Guid).Name)
		{
			throw new ArgumentException("Type is not typeof(Guid)");
		}
		Guid value;
		if (!LittleEndianReader.ReadGuid(pair.Item1, out value))
		{
			value = default(Guid);
		}
		return value;
	}

	public DateTime GetDateTime(string name)
	{
		Tuple<byte[], string> pair;
		if (!this.data.TryGetValue(name, out pair))
		{
			return DateTime.Now;
		}
		if (pair.Item2 != typeof(DateTime).Name)
		{
			throw new ArgumentException("Type is not typeof(DateTime)");
		}
		DateTime value;
		if (!LittleEndianReader.ReadDateTime(pair.Item1, out value))
		{
			value = default(DateTime);
		}
		return value;
	}

	public static bool Read(byte[] buffer, ref int readOffset, out Serializer data)
	{
		data = new Serializer();
		int count;
		if (!LittleEndianReader.ReadInt(buffer, ref readOffset, out count))
		{
			return false;
		}
		for (int i = 0; i < count; ++i)
		{
			string key;
			byte[] bytes;
			string type;
			if (!LittleEndianReader.ReadString(buffer, ref readOffset, out key) ||
				!LittleEndianReader.ReadBytes(buffer, ref readOffset, out bytes) ||
				!LittleEndianReader.ReadString(buffer, ref readOffset, out type))
			{
				return false;
			}
			data.Add(key, new Tuple<byte[], string>(bytes, type));
		}
		return true;
	}

	public void Write(ref byte[] buffer, ref int writeOffset)
	{
		LittleEndianWriter.WriteInt(this.data.Count, ref buffer, ref writeOffset);
		if (this.data.Count < 1)
		{
			return;
		}
		//LittleEndianByteUtility.WriteString("-------------NEW OBJECT-------------\r\n", ref buffer, ref writeOffset);
		foreach (KeyValuePair<string, Tuple<byte[], string>> pair in this.data)
		{
			//LittleEndianByteUtility.WriteString("[VariableName:" + pair.Key + "]", ref buffer, ref writeOffset);
			LittleEndianWriter.WriteString(pair.Key, ref buffer, ref writeOffset);
			LittleEndianWriter.WriteBytes(pair.Value.Item1, ref buffer, ref writeOffset);
			LittleEndianWriter.WriteString(pair.Value.Item2, ref buffer, ref writeOffset);
		}
	}
}