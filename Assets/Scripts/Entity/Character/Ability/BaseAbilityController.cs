using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityController : MonoBehaviour
{
	private Dictionary<string, AbilityTemplate> knownAbilities = new Dictionary<string, AbilityTemplate>();
	private List<AbilityTemplate> abilityHotkeys = new List<AbilityTemplate>();

	private AbilityTemplate currentAbilityTemplate = null;
	private GameObject currentAbilityAnimationObject = null;
	private float cooldownEndTime = 0.0f;

	void Awake()
	{
		IAttackable attackable = this.gameObject.GetComponent<IAttackable>();
		if (attackable != null)
		{
			attackable.OnAttacked += OnAttacked;
		}
	}

	void Update()
	{
		this.UseAbility(this.TryUseAbility());
	}

	/// <summary>
	/// Triggers the usage of an ability by returning the correct ability hotkey number to execute. Return -1 for no ability execution.
	/// </summary>
	public abstract int TryUseAbility();

	/// <summary>
	/// Attempts to execute the requested ability.
	/// </summary>
	/// <param name="hotKey"></param>
	public void UseAbility(int hotKey)
	{
		if (hotKey > -1 && hotKey < this.abilityHotkeys.Count && Time.time >= cooldownEndTime && this.abilityHotkeys[hotKey] != null)
		{
			this.currentAbilityAnimationObject = this.abilityHotkeys[hotKey].Execute(this);
			if (this.currentAbilityAnimationObject != null)
			{
				this.currentAbilityTemplate = this.abilityHotkeys[hotKey];
				this.cooldownEndTime = this.currentAbilityTemplate.Cooldown + Time.time;
			}
		}
	}

	/// <summary>
	/// Attempts to interrupt the ability that is currently being executed.
	/// </summary>
	public void Interrupt()
	{
		if (this.currentAbilityTemplate != null && this.currentAbilityTemplate.IsInterruptable && this.currentAbilityAnimationObject != null)
		{
			this.currentAbilityTemplate = null;
			this.currentAbilityAnimationObject.SetActive(false);
			Destroy(this.currentAbilityAnimationObject);
		}
	}

	/// <summary>
	/// Called when the defender is attacked. Base function interrupts the current ability if hit.
	/// </summary>
	/// <param name="attacker"></param>
	/// <param name="ability"></param>
	public virtual void OnAttacked(AttackInfo attackInfo)
	{
		Interrupt();
	}
}