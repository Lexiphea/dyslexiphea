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
		LittleEndianWriter.WriteBool(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteBool(test, ref BEbuffer);

		bool test2;
		LittleEndianReader.ReadBool(buffer, out test2);

		return "Bool Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string ByteTest()
	{
		byte test = byte.MaxValue;
		byte[] buffer = null;
		LittleEndianWriter.WriteByte(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteByte(test, ref BEbuffer);

		byte test2;
		LittleEndianReader.ReadByte(buffer, out test2);

		return "Byte Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string SByteTest()
	{
		sbyte test = sbyte.MaxValue;
		byte[] buffer = null;
		LittleEndianWriter.WriteSByte(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteSByte(test, ref BEbuffer);

		sbyte test2;
		LittleEndianReader.ReadSByte(buffer, out test2);

		return "SByte Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string CharTest()
	{
		char test = '!';
		byte[] buffer = null;
		LittleEndianWriter.WriteChar(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteChar(test, ref BEbuffer);

		char test2;
		LittleEndianReader.ReadChar(buffer, out test2);

		return "Char Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string UShortTest()
	{
		ushort test = 42415;
		byte[] buffer = null;
		LittleEndianWriter.WriteUShort(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteUShort(test, ref BEbuffer);

		ushort test2;
		LittleEndianReader.ReadUShort(buffer, out test2);

		return "UShort Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string ShortTest()
	{
		short test = 12552;
		byte[] buffer = null;
		LittleEndianWriter.WriteShort(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteShort(test, ref BEbuffer);

		short test2;
		LittleEndianReader.ReadShort(buffer, out test2);

		return "Short Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string UIntTest()
	{
		uint test = 3678678677;
		byte[] buffer = null;
		LittleEndianWriter.WriteUInt(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteUInt(test, ref BEbuffer);

		uint test2;
		LittleEndianReader.ReadUInt(buffer, out test2);

		return "UInt Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string IntTest()
	{
		int test = 1231242345;
		byte[] buffer = null;
		LittleEndianWriter.WriteInt(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteInt(test, ref BEbuffer);

		int test2;
		LittleEndianReader.ReadInt(buffer, out test2);

		return "Int Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string ULongTest()
	{
		ulong test = 14254535353445486777;
		byte[] buffer = null;
		LittleEndianWriter.WriteULong(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteULong(test, ref BEbuffer);

		ulong test2;
		LittleEndianReader.ReadULong(buffer, out test2);

		return "ULong Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string LongTest()
	{
		long test = 2342341758562542432;
		byte[] buffer = null;
		LittleEndianWriter.WriteLong(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteLong(test, ref BEbuffer);

		long test2;
		LittleEndianReader.ReadLong(buffer, out test2);

		return "Long Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string FloatTest()
	{
		float test = 123.456789f;
		byte[] buffer = null;
		LittleEndianWriter.WriteFloat(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteFloat(test, ref BEbuffer);

		float test2;
		LittleEndianReader.ReadFloat(buffer, out test2);

		return "Float Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string DoubleTest()
	{
		double test = 456.7890123f;
		byte[] buffer = null;
		LittleEndianWriter.WriteDouble(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteDouble(test, ref BEbuffer);

		double test2;
		LittleEndianReader.ReadDouble(buffer, out test2);

		return "Double Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string DecimalTest()
	{
		decimal test = 7890.1234567890m;
		byte[] buffer = null;
		LittleEndianWriter.WriteDecimal(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteDecimal(test, ref BEbuffer);

		decimal test2;
		LittleEndianReader.ReadDecimal(buffer, out test2);

		return "Decimal Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string StringTest()
	{
		string test = "SKDJFLSDJ";
		byte[] buffer = null;
		LittleEndianWriter.WriteString(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteString(test, ref BEbuffer);

		string test2;
		LittleEndianReader.ReadString(buffer, out test2);

		return "String Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string GuidTest()
	{
		Guid test = Guid.NewGuid();
		byte[] buffer = null;
		LittleEndianWriter.WriteGuid(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteGuid(test, ref BEbuffer);

		Guid test2;
		LittleEndianReader.ReadGuid(buffer, out test2);

		return "Guid Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}

	public string DateTimeTest()
	{
		DateTime test = DateTime.Now;
		byte[] buffer = null;
		LittleEndianWriter.WriteDateTime(test, ref buffer);
		byte[] BEbuffer = null;
		BigEndianWriter.WriteDateTime(test, ref BEbuffer);

		DateTime test2;
		LittleEndianReader.ReadDateTime(buffer, out test2);

		return "DateTime Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n\tLEBytes  <" + Hex.ToString(buffer) + ">\r\n\tBEBytes  <" + Hex.ToString(BEbuffer) + ">\r\n";
	}
}