using UnityEngine;
using UnityEditor;

public class AbilityTemplate : DatabaseTemplate
{
	private string description = "NO_DESCRIPTION";
	private bool interruptable = true;
	private float hitChance = 100.0f;
	private float cooldown = 0.0f;
	private float attackForce = 0.0f;
	private AbilityTarget target = AbilityTarget.Enemy;
	private GameObject abilityAnimationPrefab = null;
	private StatusEffectTemplate[] statusEffects = null;

	public string Description { get { return this.description; } }
	public bool IsInterruptable { get { return this.interruptable; } }
	public float HitChance { get { return this.hitChance; } }
	public float Cooldown { get { return this.cooldown; } }
	public float AttackForce { get { return this.attackForce; } }
	public AbilityTarget Target { get { return this.target; } }
	public StatusEffectTemplate[] StatusEffects { get { return this.statusEffects; } }

	[MenuItem("Templates/Ability/New Template")]
	public static AbilityTemplate Create()
	{
		AbilityTemplate template = ScriptableObject.CreateInstance<AbilityTemplate>();
		string path = "Assets/NewAbilityTemplate.asset";
		path = AssetDatabase.GenerateUniqueAssetPath(path);
		AssetDatabase.CreateAsset(template, path);
		AssetDatabase.SaveAssets();
		return template;
	}

	public GameObject Execute(BaseAbilityController abilityController)
	{
		if (this.abilityAnimationPrefab != null)
		{
			GameObject abilityAnimationObj = Instantiate(this.abilityAnimationPrefab, abilityController.transform.position, abilityController.transform.rotation, abilityController.transform);
			return abilityAnimationObj;
		}
		return null;
	}
}