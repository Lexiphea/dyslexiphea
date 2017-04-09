using UnityEngine;
using UnityEditor;

public class StatusEffectTemplate : DatabaseTemplate
{
	public string Description = "NO_DESCRIPTION";
	public float Duration = 0.0f;
	public float TickRate = 0.0f;
	[Tooltip("Resets the duration of the buff when applied again if true.")]
	public bool ResetDuration = true;
	public int MaxStacks = 1;

	[MenuItem("Templates/Status Effect/New Template")]
	public static StatusEffectTemplate Create()
	{
		StatusEffectTemplate template = ScriptableObject.CreateInstance<StatusEffectTemplate>();
		string path = "Assets/NewStatusEffectTemplate.asset";
		path = AssetDatabase.GenerateUniqueAssetPath(path);
		AssetDatabase.CreateAsset(template, path);
		AssetDatabase.SaveAssets();
		return template;
	}

	public void OnApply(GameObject target)
	{
	}

	public void OnTick(GameObject target, int numStacks)
	{
	}

	public void OnRemove(GameObject target)
	{
	}
}