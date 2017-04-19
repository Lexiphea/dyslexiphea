using System;
using System.Text;

[Serializable]
public class Tuple<T1, T2> : IEquatable<Tuple<T1, T2>>
{
	private readonly T1 item1;
	private readonly T2 item2;

	public T1 Item1 { get { return this.item1; } }
	public T2 Item2 { get { return this.item2; } }

	public Tuple(T1 item1, T2 item2)
	{
		this.item1 = item1;
		this.item2 = item2;
	}

	public override bool Equals(Object other)
	{
		return this.Equals(other as Tuple<T1, T2>);
	}

	public bool Equals(Tuple<T1, T2> other)
	{
		return other != null && this.item1.Equals(other.item1) && this.item2.Equals(other.item2);
	}

	public static bool operator ==(Tuple<T1, T2> a, Tuple<T1, T2> b)
	{
		return a.Equals(b);
	}

	public static bool operator !=(Tuple<T1, T2> a, Tuple<T1, T2> b)
	{
		return !(a.Equals(b));
	}

	public override int GetHashCode()
	{
		return Hash.GetHashCode(this.item1, this.item2);
	}

	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("(");
		sb.Append(this.item1.ToString());
		sb.Append(", ");
		sb.Append(this.item2.ToString());
		sb.Append(")");
		return sb.ToString();
	}
}

[Serializable]
public class Tuple<T1, T2, T3> : IEquatable<Tuple<T1, T2, T3>>
{
	private readonly T1 item1;
	private readonly T2 item2;
	private readonly T3 item3;

	public T1 Item1 { get { return this.item1; } }
	public T2 Item2 { get { return this.item2; } }
	public T3 Item3 { get { return this.item3; } }

	public Tuple(T1 item1, T2 item2, T3 item3)
	{
		this.item1 = item1;
		this.item2 = item2;
		this.item3 = item3;
	}

	public override bool Equals(Object other)
	{
		return this.Equals(other as Tuple<T1, T2, T3>);
	}

	public bool Equals(Tuple<T1, T2, T3> other)
	{
		return other != null && this.item1.Equals(other.item1) && this.item2.Equals(other.item2) && this.item3.Equals(other.item3);
	}

	public static bool operator ==(Tuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
	{
		return a.Equals(b);
	}

	public static bool operator !=(Tuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
	{
		return !(a.Equals(b));
	}

	public override int GetHashCode()
	{
		return Hash.GetHashCode(this.item1, this.item2, this.item3);
	}

	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("(");
		sb.Append(this.item1.ToString());
		sb.Append(", ");
		sb.Append(this.item2.ToString());
		sb.Append(", ");
		sb.Append(this.item3.ToString());
		sb.Append(")");
		return sb.ToString();
	}
}

[Serializable]
public class Tuple<T1, T2, T3, T4> : IEquatable<Tuple<T1, T2, T3, T4>>
{
	private readonly T1 item1;
	private readonly T2 item2;
	private readonly T3 item3;
	private readonly T4 item4;

	public T1 Item1 { get { return this.item1; } }
	public T2 Item2 { get { return this.item2; } }
	public T3 Item3 { get { return this.item3; } }
	public T4 Item4 { get { return this.item4; } }

	public Tuple(T1 item1, T2 item2, T3 item3, T4 item4)
	{
		this.item1 = item1;
		this.item2 = item2;
		this.item3 = item3;
		this.item4 = item4;
	}

	public override bool Equals(Object other)
	{
		return this.Equals(other as Tuple<T1, T2, T3, T4>);
	}

	public bool Equals(Tuple<T1, T2, T3, T4> other)
	{
		return other != null && this.item1.Equals(other.item1) && this.item2.Equals(other.item2) && this.item3.Equals(other.item3) && this.item4.Equals(other.item4);
	}

	public static bool operator ==(Tuple<T1, T2, T3, T4> a, Tuple<T1, T2, T3, T4> b)
	{
		return a.Equals(b);
	}

	public static bool operator !=(Tuple<T1, T2, T3, T4> a, Tuple<T1, T2, T3, T4> b)
	{
		return !(a.Equals(b));
	}

	public override int GetHashCode()
	{
		return Hash.GetHashCode(this.item1, this.item2, this.item3, this.item4);
	}

	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("(");
		sb.Append(this.item1.ToString());
		sb.Append(", ");
		sb.Append(this.item2.ToString());
		sb.Append(", ");
		sb.Append(this.item3.ToString());
		sb.Append(", ");
		sb.Append(this.item4.ToString());
		sb.Append(")");
		return sb.ToString();
	}
}

[Serializable]
public class Tuple<T1, T2, T3, T4, T5> : IEquatable<Tuple<T1, T2, T3, T4, T5>>
{
	private readonly T1 item1;
	private readonly T2 item2;
	private readonly T3 item3;
	private readonly T4 item4;
	private readonly T5 item5;

	public T1 Item1 { get { return this.item1; } }
	public T2 Item2 { get { return this.item2; } }
	public T3 Item3 { get { return this.item3; } }
	public T4 Item4 { get { return this.item4; } }
	public T5 Item5 { get { return this.item5; } }

	public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
	{
		this.item1 = item1;
		this.item2 = item2;
		this.item3 = item3;
		this.item4 = item4;
		this.item5 = item5;
	}

	public override bool Equals(Object other)
	{
		return this.Equals(other as Tuple<T1, T2, T3, T4, T5>);
	}

	public bool Equals(Tuple<T1, T2, T3, T4, T5> other)
	{
		return other != null && this.item1.Equals(other.item1) && this.item2.Equals(other.item2) && this.item3.Equals(other.item3) && this.item4.Equals(other.item4) && this.item5.Equals(other.item5);
	}

	public static bool operator ==(Tuple<T1, T2, T3, T4, T5> a, Tuple<T1, T2, T3, T4, T5> b)
	{
		return a.Equals(b);
	}

	public static bool operator !=(Tuple<T1, T2, T3, T4, T5> a, Tuple<T1, T2, T3, T4, T5> b)
	{
		return !(a.Equals(b));
	}

	public override int GetHashCode()
	{
		return Hash.GetHashCode(this.item1, this.item2, this.item3, this.item4, this.item5);
	}

	public override string ToString()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("(");
		sb.Append(this.item1.ToString());
		sb.Append(", ");
		sb.Append(this.item2.ToString());
		sb.Append(", ");
		sb.Append(this.item3.ToString());
		sb.Append(", ");
		sb.Append(this.item4.ToString());
		sb.Append(", ");
		sb.Append(this.item5.ToString());
		sb.Append(")");
		return sb.ToString();
	}
}