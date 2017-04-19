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
	}

	public string BoolTest()
	{
		bool test = true;
		byte[] buffer = null;
		ByteUtility.WriteBool(test, ref buffer);

		bool test2;
		ByteUtility.ReadBool(buffer, out test2);

		return "Bool Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string ByteTest()
	{
		byte test = byte.MaxValue;
		byte[] buffer = null;
		ByteUtility.WriteByte(test, ref buffer);

		byte test2;
		ByteUtility.ReadByte(buffer, out test2);

		return "Byte Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string SByteTest()
	{
		sbyte test = sbyte.MaxValue;
		byte[] buffer = null;
		ByteUtility.WriteSByte(test, ref buffer);

		sbyte test2;
		ByteUtility.ReadSByte(buffer, out test2);

		return "SByte Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string CharTest()
	{
		char test = '!';
		byte[] buffer = null;
		ByteUtility.WriteChar(test, ref buffer);

		char test2;
		ByteUtility.ReadChar(buffer, out test2);

		return "Char Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string UShortTest()
	{
		ushort test = ushort.MaxValue;
		byte[] buffer = null;
		ByteUtility.WriteUShort(test, ref buffer);

		ushort test2;
		ByteUtility.ReadUShort(buffer, out test2);

		return "UShort Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string ShortTest()
	{
		short test = short.MaxValue;
		byte[] buffer = null;
		ByteUtility.WriteShort(test, ref buffer);

		short test2;
		ByteUtility.ReadShort(buffer, out test2);

		return "Short Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string UIntTest()
	{
		uint test = uint.MaxValue;
		byte[] buffer = null;
		ByteUtility.WriteUInt(test, ref buffer);

		uint test2;
		ByteUtility.ReadUInt(buffer, out test2);

		return "UInt Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string IntTest()
	{
		int test = int.MaxValue;
		byte[] buffer = null;
		ByteUtility.WriteInt(test, ref buffer);

		int test2;
		ByteUtility.ReadInt(buffer, out test2);

		return "Int Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string ULongTest()
	{
		ulong test = ulong.MaxValue;
		byte[] buffer = null;
		ByteUtility.WriteULong(test, ref buffer);

		ulong test2;
		ByteUtility.ReadULong(buffer, out test2);

		return "ULong Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string LongTest()
	{
		long test = long.MaxValue;
		byte[] buffer = null;
		ByteUtility.WriteLong(test, ref buffer);

		long test2;
		ByteUtility.ReadLong(buffer, out test2);

		return "Long Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string FloatTest()
	{
		float test = float.MaxValue;
		byte[] buffer = null;
		ByteUtility.WriteFloat(test, ref buffer);

		float test2;
		ByteUtility.ReadFloat(buffer, out test2);

		return "Float Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string DoubleTest()
	{
		double test = double.MaxValue;
		byte[] buffer = null;
		ByteUtility.WriteDouble(test, ref buffer);

		double test2;
		ByteUtility.ReadDouble(buffer, out test2);

		return "Double Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string DecimalTest()
	{
		decimal test = decimal.MaxValue;
		byte[] buffer = null;
		ByteUtility.WriteDecimal(test, ref buffer);

		decimal test2;
		ByteUtility.ReadDecimal(buffer, out test2);

		return "Decimal Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string StringTest()
	{
		string test = "SKDJFLSDJKFKLSDFJ&#$*(&8a7sd7a9sd!)(";
		byte[] buffer = null;
		ByteUtility.WriteString(test, ref buffer);

		string test2;
		ByteUtility.ReadString(buffer, out test2);

		return "String Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string GuidTest()
	{
		Guid test = Guid.NewGuid();
		byte[] buffer = null;
		ByteUtility.WriteGuid(test, ref buffer);

		Guid test2;
		ByteUtility.ReadGuid(buffer, out test2);

		return "Guid Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}

	public string DateTimeTest()
	{
		DateTime test = DateTime.Now;
		byte[] buffer = null;
		ByteUtility.WriteDateTime(test, ref buffer);

		DateTime test2;
		ByteUtility.ReadDateTime(buffer, out test2);

		return "DateTime Test:\r\n\tInitial  <" + test + ">\r\n\tResult <" + test2 + ">\r\n";
	}
}