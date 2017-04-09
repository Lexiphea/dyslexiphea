using UnityEngine;

public class StatusEffect : MonoBehaviour
{
	private StatusEffectTemplate template;
	private float endTime;
	private IAccumulator accumulator;
	private int stacks;

	void Update()
	{
		if (Time.time >= endTime)
		{
			this.Remove();
		}
		else if (this.accumulator != null)
		{
			this.accumulator.TryUpdate(() =>
			{
				this.template.OnTick(this.gameObject, this.stacks);
			});
		}
	}

	public static void Apply(GameObject target, StatusEffectTemplate template)
	{
		if (target == null || template == null)
		{
			return;
		}
		StatusEffect newEffect = target.AddComponent<StatusEffect>();
		newEffect.template = template;
		newEffect.endTime = Time.time + template.Duration;
		if (template.TickRate > 0.0f)
		{
			newEffect.accumulator = new Accumulator(Time.time, template.TickRate);
		}
		template.OnApply(target);
	}

	private void Remove()
	{
		this.template.OnRemove(this.gameObject);

		this.enabled = false;
		Destroy(this);
	}

	private void OnReset()
	{
		this.endTime = Time.time + this.template.Duration;
		if (this.accumulator != null)
		{
			this.accumulator.SetTime(Time.time);
		}
	}
}