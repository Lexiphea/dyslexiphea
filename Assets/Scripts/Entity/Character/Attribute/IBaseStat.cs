public interface IBaseStat
{
	float BaseValue { get; set; }
	float MinValue { get; set; }
	float MaxValue { get; set; }

	float InitialValue { get; }
	float DefaultMinValue { get; }
	float DefaultMaxValue { get; }
}