using UnityEngine;

public static class Vector2Extensions
{
	public static Vector2 Perpendicular(this Vector2 vector)
	{
		return new Vector2(vector.y, -vector.x);
	}
}