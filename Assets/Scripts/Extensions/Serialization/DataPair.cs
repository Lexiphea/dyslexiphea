using System;

internal class DataPair
{
	public byte[] Data;
	public string Type;

	public DataPair() { }
	public DataPair(byte[] data, Type type)
	{
		Data = data;
		Type = type.Name;
	}

	public static bool Read(byte[] buffer, ref int readOffset, out DataPair pair)
	{
		pair = new DataPair();
		if (!ByteUtility.ReadBytes(buffer, ref readOffset, out pair.Data) ||
			!ByteUtility.ReadString(buffer, ref readOffset, out pair.Type))
		{
			return false;
		}
		return true;
	}

	public void Write(ref byte[] buffer, ref int writeOffset)
	{
		//string test = Encoding.ASCII.GetString(Data);
		//ByteUtility.WriteString("[Value:" + test + "]", ref buffer, ref writeOffset);
		//ByteUtility.WriteString("[DataType:" + Type + "]", ref buffer, ref writeOffset);
		//ByteUtility.WriteString("\r\n", ref buffer, ref writeOffset);
		ByteUtility.WriteBytes(Data, ref buffer, ref writeOffset);
		ByteUtility.WriteString(Type, ref buffer, ref writeOffset);
	}
}