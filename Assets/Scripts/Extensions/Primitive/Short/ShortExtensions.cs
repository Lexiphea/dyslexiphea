﻿using System;

public static class ShortExtensions
{
	/// <summary>
	/// Returns the absolute value of the number.
	/// </summary>
	public static short Absolute(this short number)
	{
		return (number < 0) ? (short)(-number) : number;
	}

	/// <summary>
	/// Returns the sign of the number;
	/// </summary>
	public static short Sign(this short number)
	{
		return (short)((number < 0) ? -1 : 1);
	}

	/// <summary>
	/// Returns the number clamped to the specified minimum and maximum value.
	/// </summary>
	public static short Clamp(this short number, short minimum, short maximum)
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
	public static short Min(this short number, short minimum)
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
	public static short Max(this short number, short maximum)
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
	public static int DigitCount(this short number)
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
	public static short GetDigit(this short number, int digit)
	{
		const byte MIN_DIGITS = 0;
		const byte BASE_TEN = 10;

		number = number.Absolute();
		digit = digit.Clamp(MIN_DIGITS, number.DigitCount());
		for (int i = MIN_DIGITS; i < digit; ++i)
		{
			number /= BASE_TEN;
		}
		return (short)(number % BASE_TEN);
	}
}