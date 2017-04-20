using System;
using System.Text;
using UnityEngine;

public class ByteUtilityTest : MonoBehaviour
{
	void Awake()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append(BoolTest());
		sb.Append(ByteTest());
		sb.Append(SByteTest());
		sb.Append(CharTest());
		sb.Append(UShortTest());
		sb.Append(ShortTest());
		sb.Append(UIntTest());
		sb.Append(IntTest());
		sb.Append(ULongTest());
		sb.Append(LongTest());
		sb.Append(FloatTest());
		sb.Append(DoubleTest());
		sb.Append(DecimalTest());
		sb.Append(StringTest());
		sb.Append(GuidTest());
		sb.Append(DateTimeTest());
		Debug.Log(sb.ToString());

		Debug.Log("IsLittleEndian: " + BitConverter.IsLittleEndian);
	}

	public string BoolTest()
	{
		bool test = true;
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteBool(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteBool(test, ref BEbuffer);

		bool test2;
		ByteReader.LittleEndian.ReadBool(buffer, out test2);

		return "Bool Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string ByteTest()
	{
		byte test = byte.MaxValue;
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteByte(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteByte(test, ref BEbuffer);

		byte test2;
		ByteReader.LittleEndian.ReadByte(buffer, out test2);

		return "Byte Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string SByteTest()
	{
		sbyte test = sbyte.MaxValue;
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteSByte(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteSByte(test, ref BEbuffer);

		sbyte test2;
		ByteReader.LittleEndian.ReadSByte(buffer, out test2);

		return "SByte Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string CharTest()
	{
		char test = '!';
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteChar(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteChar(test, ref BEbuffer);

		char test2;
		ByteReader.LittleEndian.ReadChar(buffer, out test2);

		return "Char Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string UShortTest()
	{
		ushort test = 42415;
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteUShort(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteUShort(test, ref BEbuffer);

		ushort test2;
		ByteReader.LittleEndian.ReadUShort(buffer, out test2);

		return "UShort Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string ShortTest()
	{
		short test = 12552;
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteShort(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteShort(test, ref BEbuffer);

		short test2;
		ByteReader.LittleEndian.ReadShort(buffer, out test2);

		return "Short Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string UIntTest()
	{
		uint test = 3678678677;
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteUInt(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteUInt(test, ref BEbuffer);

		uint test2;
		ByteReader.LittleEndian.ReadUInt(buffer, out test2);

		return "UInt Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string IntTest()
	{
		int test = 1231242345;
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteInt(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteInt(test, ref BEbuffer);

		int test2;
		ByteReader.LittleEndian.ReadInt(buffer, out test2);

		return "Int Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string ULongTest()
	{
		ulong test = 14254535353445486777;
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteULong(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteULong(test, ref BEbuffer);

		ulong test2;
		ByteReader.LittleEndian.ReadULong(buffer, out test2);

		return "ULong Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string LongTest()
	{
		long test = 2342341758562542432;
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteLong(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteLong(test, ref BEbuffer);

		long test2;
		ByteReader.LittleEndian.ReadLong(buffer, out test2);

		return "Long Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string FloatTest()
	{
		float test = 123.456789f;
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteFloat(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteFloat(test, ref BEbuffer);

		float test2;
		ByteReader.LittleEndian.ReadFloat(buffer, out test2);

		return "Float Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string DoubleTest()
	{
		double test = 456.7890123f;
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteDouble(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteDouble(test, ref BEbuffer);

		double test2;
		ByteReader.LittleEndian.ReadDouble(buffer, out test2);

		return "Double Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string DecimalTest()
	{
		decimal test = 7890.1234567890m;
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteDecimal(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteDecimal(test, ref BEbuffer);

		decimal test2;
		ByteReader.LittleEndian.ReadDecimal(buffer, out test2);

		return "Decimal Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string StringTest()
	{
		string test = "SKDJFLSDJ";
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteString(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteString(test, ref BEbuffer);

		string test2;
		ByteReader.LittleEndian.ReadString(buffer, out test2);

		return "String Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string GuidTest()
	{
		Guid test = Guid.NewGuid();
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteGuid(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteGuid(test, ref BEbuffer);

		Guid test2;
		ByteReader.LittleEndian.ReadGuid(buffer, out test2);

		return "Guid Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string DateTimeTest()
	{
		DateTime test = DateTime.Now;
		byte[] buffer = null;
		ByteWriter.LittleEndian.WriteDateTime(test, ref buffer);
		byte[] BEbuffer = null;
		ByteWriter.BigEndian.WriteDateTime(test, ref BEbuffer);

		DateTime test2;
		ByteReader.LittleEndian.ReadDateTime(buffer, out test2);

		return "DateTime Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}
}