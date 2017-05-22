using System;

public class PercentStrengthModifier : CharacterStat
{
	public override Type[] ParentTypes
	{
		get
		{
			return new Type[]
			{
				typeof(Strength),
			};
		}
	}
	public override Type[] ChildTypes { get { return new Type[] { }; } }

	public override float InitialValue { get { return 0.0f; } }
	public override float DefaultMinValue { get { return 0.0f; } }
	public override float DefaultMaxValue { get { return 50.0f; } }
}