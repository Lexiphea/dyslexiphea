using System;
using System.Text;

public interface ISerializer
{
	void Add(string name, bool value);
	void Add(string name, byte value);
	void Add(string name, sbyte value);
	void Add(string name, char value);
	void Add(string name, ushort value);
	void Add(string name, short value);
	void Add(string name, uint value);
	void Add(string name, int value);
	void Add(string name, ulong value);
	void Add(string name, long value);
	void Add(string name, float value);
	void Add(string name, double value);
	void Add(string name, decimal value);
	void Add(string name, string value);
	void Add(string name, string value, Encoding encoding);
	void Add(string name, Guid value);
	void Add(string name, DateTime value);

	bool GetBool(string name);
	byte GetByte(string name);
	sbyte GetSByte(string name);
	char GetChar(string name);
	ushort GetUShort(string name);
	short GetShort(string name);
	uint GetUInt(string name);
	int GetInt(string name);
	ulong GetULong(string name);
	long GetLong(string name);
	float GetFloat(string name);
	double GetDouble(string name);
	decimal GetDecimal(string name);
	string GetString(string name);
	string GetString(string name, Encoding encoding);
	Guid GetGuid(string name);
	DateTime GetDateTime(string name);
}