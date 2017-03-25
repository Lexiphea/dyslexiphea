using System;
using UnityEngine;

public interface ISerializer
{
	void Add(string name, Guid value);
	void Add(string name, DateTime value);
	void Add(string name, string value);
	void Add(string name, char value);
	void Add(string name, byte value);
	void Add(string name, sbyte value);
	void Add(string name, short value);
	void Add(string name, ushort value);
	void Add(string name, int value);
	void Add(string name, uint value);
	void Add(string name, long value);
	void Add(string name, ulong value);
	void Add(string name, bool value);
	void Add(string name, float value);
	void Add(string name, double value);
	void Add(string name, Vector2 value);
	void Add(string name, Vector3 value);
	void Add(string name, Quaternion value);
	void Add(string name, Color value);
	void Add(string name, TinyColor value);

	Guid GetGuid(string name);
	DateTime GetDateTime(string name);
	string GetString(string name);
	char GetChar(string name);
	byte GetByte(string name);
	sbyte GetSByte(string name);
	short GetShort(string name);
	ushort GetUShort(string name);
	int GetInt(string name);
	uint GetUInt(string name);
	long GetLong(string name);
	ulong GetULong(string name);
	bool GetBool(string name);
	float GetFloat(string name);
	double GetDouble(string name);
	Vector2 GetVector2(string name);
	Vector3 GetVector3(string name);
	Quaternion GetQuaternion(string name);
	Color GetColor(string name);
	TinyColor GetTinyColor(string name);
}