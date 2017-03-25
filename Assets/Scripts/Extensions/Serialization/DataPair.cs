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

	public static bool Read(out DataPair pair, DataBuffer buffer)
	{
		pair = new DataPair();
		if (!buffer.ReadBytes(out pair.Data) ||
			!buffer.ReadString(out pair.Type))
		{
			return false;
		}
		return true;
	}

	public void Write(DataBuffer buffer)
	{
		//string test = Encoding.ASCII.GetString(Data);
		//pBuffer.WriteString("[Value:" + test + "]");
		//pBuffer.WriteString("[DataType:" + Type + "]");
		//pBuffer.WriteString("\r\n");
		buffer.WriteBytes(Data);
		buffer.WriteString(Type);
	}
}