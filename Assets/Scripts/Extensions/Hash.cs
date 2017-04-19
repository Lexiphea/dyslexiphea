public static class Hash
{
	private const int InitialHash = 17;
	private const int Prime = 31;

	public static int GetHashCode<T1>(T1 item1)
	{
		unchecked
		{
			return InitialHash * Prime + ((item1 != null) ? item1.GetHashCode() : 1);
		}
	}

	public static int GetHashCode<T1, T2>(T1 item1, T2 item2)
	{
		unchecked
		{
			int hash = InitialHash;
			hash = hash * Prime + ((item1 != null) ? item1.GetHashCode() : 1);
			hash = hash * Prime + ((item2 != null) ? item2.GetHashCode() : 2);
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
	{
		unchecked
		{
			int hash = InitialHash;
			hash = hash * Prime + ((item1 != null) ? item1.GetHashCode() : 1);
			hash = hash * Prime + ((item2 != null) ? item2.GetHashCode() : 2);
			hash = hash * Prime + ((item3 != null) ? item3.GetHashCode() : 3);
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
	{
		unchecked
		{
			int hash = InitialHash;
			hash = hash * Prime + ((item1 != null) ? item1.GetHashCode() : 1);
			hash = hash * Prime + ((item2 != null) ? item2.GetHashCode() : 2);
			hash = hash * Prime + ((item3 != null) ? item3.GetHashCode() : 3);
			hash = hash * Prime + ((item4 != null) ? item4.GetHashCode() : 4);
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
	{
		unchecked
		{
			int hash = InitialHash;
			hash = hash * Prime + ((item1 != null) ? item1.GetHashCode() : 1);
			hash = hash * Prime + ((item2 != null) ? item2.GetHashCode() : 2);
			hash = hash * Prime + ((item3 != null) ? item3.GetHashCode() : 3);
			hash = hash * Prime + ((item4 != null) ? item4.GetHashCode() : 4);
			hash = hash * Prime + ((item5 != null) ? item5.GetHashCode() : 5);
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
	{
		unchecked
		{
			int hash = InitialHash;
			hash = hash * Prime + ((item1 != null) ? item1.GetHashCode() : 1);
			hash = hash * Prime + ((item2 != null) ? item2.GetHashCode() : 2);
			hash = hash * Prime + ((item3 != null) ? item3.GetHashCode() : 3);
			hash = hash * Prime + ((item4 != null) ? item4.GetHashCode() : 4);
			hash = hash * Prime + ((item5 != null) ? item5.GetHashCode() : 5);
			hash = hash * Prime + ((item6 != null) ? item6.GetHashCode() : 6);
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
	{
		unchecked
		{
			int hash = InitialHash;
			hash = hash * Prime + ((item1 != null) ? item1.GetHashCode() : 1);
			hash = hash * Prime + ((item2 != null) ? item2.GetHashCode() : 2);
			hash = hash * Prime + ((item3 != null) ? item3.GetHashCode() : 3);
			hash = hash * Prime + ((item4 != null) ? item4.GetHashCode() : 4);
			hash = hash * Prime + ((item5 != null) ? item5.GetHashCode() : 5);
			hash = hash * Prime + ((item6 != null) ? item6.GetHashCode() : 6);
			hash = hash * Prime + ((item7 != null) ? item7.GetHashCode() : 7);
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
	{
		unchecked
		{
			int hash = InitialHash;
			hash = hash * Prime + ((item1 != null) ? item1.GetHashCode() : 1);
			hash = hash * Prime + ((item2 != null) ? item2.GetHashCode() : 2);
			hash = hash * Prime + ((item3 != null) ? item3.GetHashCode() : 3);
			hash = hash * Prime + ((item4 != null) ? item4.GetHashCode() : 4);
			hash = hash * Prime + ((item5 != null) ? item5.GetHashCode() : 5);
			hash = hash * Prime + ((item6 != null) ? item6.GetHashCode() : 6);
			hash = hash * Prime + ((item7 != null) ? item7.GetHashCode() : 7);
			hash = hash * Prime + ((item8 != null) ? item8.GetHashCode() : 8);
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9)
	{
		unchecked
		{
			int hash = InitialHash;
			hash = hash * Prime + ((item1 != null) ? item1.GetHashCode() : 1);
			hash = hash * Prime + ((item2 != null) ? item2.GetHashCode() : 2);
			hash = hash * Prime + ((item3 != null) ? item3.GetHashCode() : 3);
			hash = hash * Prime + ((item4 != null) ? item4.GetHashCode() : 4);
			hash = hash * Prime + ((item5 != null) ? item5.GetHashCode() : 5);
			hash = hash * Prime + ((item6 != null) ? item6.GetHashCode() : 6);
			hash = hash * Prime + ((item7 != null) ? item7.GetHashCode() : 7);
			hash = hash * Prime + ((item8 != null) ? item8.GetHashCode() : 8);
			hash = hash * Prime + ((item9 != null) ? item9.GetHashCode() : 9);
			return hash;
		}
	}

	public static int GetHashCode<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8, T9 item9, T10 item10)
	{
		unchecked
		{
			int hash = InitialHash;
			hash = hash * Prime + ((item1 != null) ? item1.GetHashCode() : 1);
			hash = hash * Prime + ((item2 != null) ? item2.GetHashCode() : 2);
			hash = hash * Prime + ((item3 != null) ? item3.GetHashCode() : 3);
			hash = hash * Prime + ((item4 != null) ? item4.GetHashCode() : 4);
			hash = hash * Prime + ((item5 != null) ? item5.GetHashCode() : 5);
			hash = hash * Prime + ((item6 != null) ? item6.GetHashCode() : 6);
			hash = hash * Prime + ((item7 != null) ? item7.GetHashCode() : 7);
			hash = hash * Prime + ((item8 != null) ? item8.GetHashCode() : 8);
			hash = hash * Prime + ((item9 != null) ? item9.GetHashCode() : 9);
			hash = hash * Prime + ((item10 != null) ? item10.GetHashCode() : 10);
			return hash;
		}
	}
}