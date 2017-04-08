using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbilityController : MonoBehaviour
{
	private Dictionary<string, AbilityTemplate> knownAbilities = new Dictionary<string, AbilityTemplate>();
	private List<AbilityTemplate> abilityHotkeys = new List<AbilityTemplate>();

	public abstract void TryUseAbility(int hotkey);
}