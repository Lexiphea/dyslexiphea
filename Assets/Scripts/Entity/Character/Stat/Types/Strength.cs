using System;

public class Strength : CharacterStat
{
	public override Type[] ParentTypes { get { return new Type[] { }; } }
	public override Type[] ChildTypes
	{
		get
		{
			return new Type[]
			{
				typeof(PercentStrengthModifier),
				typeof(FlatStrengthModifier),
			};
		}
	}

	public override float InitialValue { get { return 1.0f; } }
	public override float DefaultMinValue { get { return 0.0f; } }
	public override float DefaultMaxValue { get { return 1000.0f; } }

	public override void OnBeforeAwake()
	{
		AddFormula(typeof(PercentStrengthModifier), CalculatePercentStrengthModifierBonus);
		AddFormula(typeof(FlatStrengthModifier), CalculateFlatStrengthModifierBonus);
	}

	private float CalculatePercentStrengthModifierBonus(ICharacterStat child)
	{
		return this.BaseValue * (0.01f * child.TotalValue);
	}

	private float CalculateFlatStrengthModifierBonus(ICharacterStat child)
	{
		return child.TotalValue;
	}
}