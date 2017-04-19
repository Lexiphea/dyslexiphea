using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
	[SerializeField]
	private MovementController movementController;
	[SerializeField]
	private InteractionController interactionController;

	void Awake()
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder();
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
		Debug.Log(sb.ToString());
	}

	public string BoolTest()
	{
		bool test = true;
		byte[] buffer = new byte[1];
		ByteUtility.WriteBool(test, ref buffer);

		bool test2;
		ByteUtility.ReadBool(buffer, out test2);

		return "Bool Test: Initial<" + test + "> Result<" + test2 + ">\r\n";
	}

	public string ByteTest()
	{
		byte test = byte.MaxValue;
		byte[] buffer = new byte[1];
		ByteUtility.WriteByte(test, ref buffer);

		byte test2;
		ByteUtility.ReadByte(buffer, out test2);

		return "Byte Test: Initial<" + test + "> Result<" + test2 + ">\r\n";
	}

	public string SByteTest()
	{
		sbyte test = sbyte.MaxValue;
		byte[] buffer = new byte[1];
		ByteUtility.WriteSByte(test, ref buffer);

		sbyte test2;
		ByteUtility.ReadSByte(buffer, out test2);

		return "SByte Test: Initial<" + test + "> Result<" + test2 + ">\r\n";
	}

	public string CharTest()
	{
		char test = char.MaxValue;
		byte[] buffer = new byte[2];
		ByteUtility.WriteChar(test, ref buffer);

		char test2;
		ByteUtility.ReadChar(buffer, out test2);

		return "Char Test: Initial<" + test + "> Result<" + test2 + ">\r\n";
	}

	public string UShortTest()
	{
		ushort test = ushort.MaxValue;
		byte[] buffer = new byte[2];
		ByteUtility.WriteUShort(test, ref buffer);

		ushort test2;
		ByteUtility.ReadUShort(buffer, out test2);

		return "UShort Test: Initial<" + test + "> Result<" + test2 + ">\r\n";
	}

	public string ShortTest()
	{
		short test = short.MaxValue;
		byte[] buffer = new byte[2];
		ByteUtility.WriteShort(test, ref buffer);

		short test2;
		ByteUtility.ReadShort(buffer, out test2);

		return "Short Test: Initial<" + test + "> Result<" + test2 + ">\r\n";
	}

	public string UIntTest()
	{
		uint test = uint.MaxValue;
		byte[] buffer = new byte[4];
		ByteUtility.WriteUInt(test, ref buffer);

		uint test2;
		ByteUtility.ReadUInt(buffer, out test2);

		return "UInt Test: Initial<" + test + "> Result<" + test2 + ">\r\n";
	}

	public string IntTest()
	{
		int test = int.MaxValue;
		byte[] buffer = new byte[4];
		ByteUtility.WriteInt(test, ref buffer);

		int test2;
		ByteUtility.ReadInt(buffer, out test2);

		return "Int Test: Initial<" + test + "> Result<" + test2 + ">\r\n";
	}

	public string ULongTest()
	{
		ulong test = ulong.MaxValue;
		byte[] buffer = new byte[8];
		ByteUtility.WriteULong(test, ref buffer);

		ulong test2;
		ByteUtility.ReadULong(buffer, out test2);

		return "ULong Test: Initial<" + test + "> Result<" + test2 + ">\r\n";
	}

	public string LongTest()
	{
		long test = long.MaxValue;
		byte[] buffer = new byte[8];
		ByteUtility.WriteLong(test, ref buffer);

		long test2;
		ByteUtility.ReadLong(buffer, out test2);

		return "Long Test: Initial<" + test + "> Result<" + test2 + ">\r\n";
	}

	public string FloatTest()
	{
		float test = float.MaxValue;
		byte[] buffer = new byte[4];
		ByteUtility.WriteFloat(test, ref buffer);

		float test2;
		ByteUtility.ReadFloat(buffer, out test2);

		return "Float Test: Initial<" + test + "> Result<" + test2 + ">\r\n";
	}

	public string DoubleTest()
	{
		double test = double.MaxValue;
		byte[] buffer = new byte[8];
		ByteUtility.WriteDouble(test, ref buffer);

		double test2;
		ByteUtility.ReadDouble(buffer, out test2);

		return "Double Test: Initial<" + test + "> Result<" + test2 + ">\r\n";
	}

	public string DecimalTest()
	{
		decimal test = decimal.MaxValue;
		byte[] buffer = new byte[16];
		ByteUtility.WriteDecimal(test, ref buffer);

		decimal test2;
		ByteUtility.ReadDecimal(buffer, out test2);

		return "Decimal Test: Initial<" + test + "> Result<" + test2 + ">\r\n";
	}

	void Update()
	{
		if (this.movementController != null)
		{
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				this.movementController.Jump();
			}
			if (Input.GetAxis("Vertical") < 0.0f)
			{
				this.movementController.Fastfall();
			}
			this.movementController.MoveXZ(Input.GetAxis("Horizontal"), 0.0f);
		}
		if (this.interactionController != null)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				this.interactionController.Interact();
			}
		}
	}
}