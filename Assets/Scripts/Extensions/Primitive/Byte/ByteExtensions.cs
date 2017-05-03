﻿using System;

public static class ByteExtensions
{
	/// <summary>
	/// Returns the number clamped to the specified minimum and maximum value.
	/// </summary>
	public static byte Clamp(this byte number, byte minimum, byte maximum)
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
	public static byte Min(this byte number, byte minimum)
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
	public static byte Max(this byte number, byte maximum)
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
	public static int DigitCount(this byte number)
	{
		if (number != 0)
		{
			return ((int)Math.Log10(number)) + 1;
		}
		return 1;
	}

	/// <summary>
	/// Returns the specified digit of the number. Where zero is the least significant digit.
	/// </summary>
	public static byte GetDigit(this byte number, int digit)
	{
		const byte MIN_DIGITS = 0;
		const byte BASE_TEN = 10;

		digit = digit.Clamp(MIN_DIGITS, number.DigitCount());
		for (int i = MIN_DIGITS; i < digit; ++i)
		{
			number /= BASE_TEN;
		}
		return (byte)(number % BASE_TEN);
	}
}