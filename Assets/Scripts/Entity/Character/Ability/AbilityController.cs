using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
	private Dictionary<string, AbilityTemplate> knownAbilities = new Dictionary<string, AbilityTemplate>();
	private List<AbilityTemplate> abilityHotkeys = new List<AbilityTemplate>();

	[SerializeField]
	private AbilityTemplate currentAbilityTemplate = null;
	[SerializeField]
	private GameObject currentAbilityAnimationObject = null;
	[SerializeField]
	private float cooldownEndTime = 0.0f;

	public int HotkeyCount
	{
		get
		{
			return this.abilityHotkeys.Count;
		}
	}

	void Awake()
	{
		IAttackable attackable = this.gameObject.GetComponent<IAttackable>();
		if (attackable != null)
		{
			attackable.OnAttacked += OnAttacked;
		}
	}

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