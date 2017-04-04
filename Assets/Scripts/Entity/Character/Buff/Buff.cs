using UnityEngine;

public class Buff : MonoBehaviour
{
	private BuffTemplate template;
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
				this.template.OnApplyTick(this.gameObject, this.stacks);
			});
		}
	}

	public static void Apply(GameObject target, BuffTemplate template)
	{
		if (target == null || template == null)
		{
			return;
		}
		Buff newBuff = target.AddComponent<Buff>();
		newBuff.template = template;
		newBuff.endTime = Time.time + template.Duration;
		if (template.TickRate > 0.0f)
		{
			newBuff.accumulator = new Accumulator(Time.time, template.TickRate);
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