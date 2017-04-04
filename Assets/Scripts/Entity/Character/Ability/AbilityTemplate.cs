using UnityEngine;
using UnityEditor;

public class AbilityTemplate : DatabaseItem
{
	private string description = "NO_DESCRIPTION";
	private float startupDuration = 0.0f;
	private float activeDuration = 0.0f;
	private float recoveryDuration = 0.0f;
	private float cooldown = 0.0f;
	private float force = 0.0f;
	private float damage = 0.0f;
	private BuffTemplate[] buffs = null;
	private GameObject hitbox = null;

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

	public override void OnGUILayout()
	{
		EditorGUILayout.BeginVertical(EditorStyles.helpBox);
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Name:");
		string newName = GUILayout.TextField(this.name);
		if (newName != this.name)
		{
			string path = AssetDatabase.GetAssetPath(this);
			AssetDatabase.RenameAsset(path, newName);
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Identifier:");
		GUILayout.TextField(this.Identifier.ToString());
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndVertical();
	}

	void OnApply(GameObject obj)
	{

	}

	void OnRemove(GameObject obj)
	{

	}
}