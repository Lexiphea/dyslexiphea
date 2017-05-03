using System;

public static class SByteExtensions
{
	/// <summary>
	/// Returns the absolute value of the number.
	/// </summary>
	public static sbyte Absolute(this sbyte number)
	{
		return (number < 0) ? (sbyte)(-number) : number;
	}

	/// <summary>
	/// Returns the sign of the number;
	/// </summary>
	public static sbyte Sign(this sbyte number)
	{
		return (sbyte)((number < 0) ? -1 : 1);
	}

	/// <summary>
	/// Returns the number clamped to the specified minimum and maximum value.
	/// </summary>
	public static sbyte Clamp(this sbyte number, sbyte minimum, sbyte maximum)
	{
		if (number < minimum)
		{
			return minimum;
		}
		if (number > maximum)
		{
			return maximum;
		}
		return number;
	}

	/// <summary>
	/// Returns the number clamped to the specified minimum value.
	/// </summary>
	public static sbyte Min(this sbyte number, sbyte minimum)
	{
		if (number < minimum)
		{
			return minimum;
		}
		return number;
	}

	/// <summary>
	/// Returns the number clamped to the specified maximum value.
	/// </summary>
	public static sbyte Max(this sbyte number, sbyte maximum)
	{
		if (number > maximum)
		{
			return maximum;
		}
		return number;
	}

	/// <summary>
	/// Returns the number of digits of the current value.
	/// </summary>
	public static int DigitCount(this sbyte number)
	{
		if (number != 0)
		{
			return ((int)Math.Log10(number.Absolute())) + 1;
		}
		return 1;
	}

	/// <summary>
	/// Returns the specified digit of the number. Where zero is the least significant digit.
	/// </summary>
	public static sbyte GetDigit(this sbyte number, int digit)
	{
		const byte MIN_DIGITS = 0;
		const sbyte BASE_TEN = 10;

		number = number.Absolute();
		digit = digit.Clamp(MIN_DIGITS, number.DigitCount());
		for (int i = MIN_DIGITS; i < digit; ++i)
		{
			number /= BASE_TEN;
		}
		return (sbyte)(number % BASE_TEN);
	}
}