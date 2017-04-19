using System;

public static class Hash
{
	private const int Prime = 31;

	public static int GetHashCode<T1>(T1 item1)
	{
		if (item1 == null)
		{
			return 0;
		}
		unchecked
		{
			return Prime + item1.GetHashCode();
		}
	}

	public static int GetHashCode<T1, T2>(T1 item1, T2 item2)
	{
		unchecked
		{
			int hash = 0;
			if (item1 != null)
			{
				hash = hash * Prime + item1.GetHashCode();
			}
			if (item2 != null)
			{
				hash = hash * Prime + item2.GetHashCode();
			}
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
	{
		unchecked
		{
			int hash = 0;
			if (item1 != null)
			{
				hash = hash * Prime + item1.GetHashCode();
			}
			if (item2 != null)
			{
				hash = hash * Prime + item2.GetHashCode();
			}
			if (item3 != null)
			{
				hash = hash * Prime + item3.GetHashCode();
			}
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
	{
		unchecked
		{
			int hash = 0;
			if (item1 != null)
			{
				hash = hash * Prime + item1.GetHashCode();
			}
			if (item2 != null)
			{
				hash = hash * Prime + item2.GetHashCode();
			}
			if (item3 != null)
			{
				hash = hash * Prime + item3.GetHashCode();
			}
			if (item4 != null)
			{
				hash = hash * Prime + item4.GetHashCode();
			}
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
	{
		unchecked
		{
			int hash = 0;
			if (item1 != null)
			{
				hash = hash * Prime + item1.GetHashCode();
			}
			if (item2 != null)
			{
				hash = hash * Prime + item2.GetHashCode();
			}
			if (item3 != null)
			{
				hash = hash * Prime + item3.GetHashCode();
			}
			if (item4 != null)
			{
				hash = hash * Prime + item4.GetHashCode();
			}
			if (item5 != null)
			{
				hash = hash * Prime + item5.GetHashCode();
			}
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
	{
		unchecked
		{
			int hash = 0;
			if (item1 != null)
			{
				hash = hash * Prime + item1.GetHashCode();
			}
			if (item2 != null)
			{
				hash = hash * Prime + item2.GetHashCode();
			}
			if (item3 != null)
			{
				hash = hash * Prime + item3.GetHashCode();
			}
			if (item4 != null)
			{
				hash = hash * Prime + item4.GetHashCode();
			}
			if (item5 != null)
			{
				hash = hash * Prime + item5.GetHashCode();
			}
			if (item6 != null)
			{
				hash = hash * Prime + item6.GetHashCode();
			}
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
	{
		unchecked
		{
			int hash = 0;
			if (item1 != null)
			{
				hash = hash * Prime + item1.GetHashCode();
			}
			if (item2 != null)
			{
				hash = hash * Prime + item2.GetHashCode();
			}
			if (item3 != null)
			{
				hash = hash * Prime + item3.GetHashCode();
			}
			if (item4 != null)
			{
				hash = hash * Prime + item4.GetHashCode();
			}
			if (item5 != null)
			{
				hash = hash * Prime + item5.GetHashCode();
			}
			if (item6 != null)
			{
				hash = hash * Prime + item6.GetHashCode();
			}
			if (item7 != null)
			{
				hash = hash * Prime + item7.GetHashCode();
			}
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
	{
		unchecked
		{
			int hash = 0;
			if (item1 != null)
			{
				hash = hash * Prime + item1.GetHashCode();
			}
			if (item2 != null)
			{
				hash = hash * Prime + item2.GetHashCode();
			}
			if (item3 != null)
			{
				hash = hash * Prime + item3.GetHashCode();
			}
			if (item4 != null)
			{
				hash = hash * Prime + item4.GetHashCode();
			}
			if (item5 != null)
			{
				hash = hash * Prime + item5.GetHashCode();
			}
			if (item6 != null)
			{
				hash = hash * Prime + item6.GetHashCode();
			}
			if (item7 != null)
			{
				hash = hash * Prime + item7.GetHashCode();
			}
			if (item8 != null)
			{
				hash = hash * Prime + item8.GetHashCode();
			}
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
	{
		unchecked
		{
			int hash = 0;
			if (item1 != null)
			{
				hash = hash * Prime + item1.GetHashCode();
			}
			if (item2 != null)
			{
				hash = hash * Prime + item2.GetHashCode();
			}
			if (item3 != null)
			{
				hash = hash * Prime + item3.GetHashCode();
			}
			if (item4 != null)
			{
				hash = hash * Prime + item4.GetHashCode();
			}
			if (item5 != null)
			{
				hash = hash * Prime + item5.GetHashCode();
			}
			if (item6 != null)
			{
				hash = hash * Prime + item6.GetHashCode();
			}
			if (item7 != null)
			{
				hash = hash * Prime + item7.GetHashCode();
			}
			if (item8 != null)
			{
				hash = hash * Prime + item8.GetHashCode();
			}
			if (item9 != null)
			{
				hash = hash * Prime + item9.GetHashCode();
			}
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10)
	{
		unchecked
		{
			int hash = 0;
			if (item1 != null)
			{
				hash = hash * Prime + item1.GetHashCode();
			}
			if (item2 != null)
			{
				hash = hash * Prime + item2.GetHashCode();
			}
			if (item3 != null)
			{
				hash = hash * Prime + item3.GetHashCode();
			}
			if (item4 != null)
			{
				hash = hash * Prime + item4.GetHashCode();
			}
			if (item5 != null)
			{
				hash = hash * Prime + item5.GetHashCode();
			}
			if (item6 != null)
			{
				hash = hash * Prime + item6.GetHashCode();
			}
			if (item7 != null)
			{
				hash = hash * Prime + item7.GetHashCode();
			}
			if (item8 != null)
			{
				hash = hash * Prime + item8.GetHashCode();
			}
			if (item9 != null)
			{
				hash = hash * Prime + item9.GetHashCode();
			}
			if (item10 != null)
			{
				hash = hash * Prime + item10.GetHashCode();
			}
			return hash;
		}
	}
}