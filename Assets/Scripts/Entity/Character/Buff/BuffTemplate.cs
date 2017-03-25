using UnityEngine;

public class BuffTemplate : DatabaseItem
{
	private string description = "NO_DESCRIPTION";
	private float duration = 0.0f;
	private int maxStacks = 1;

	[MenuItem("Assets/Create/Templates/Buff/BuffTemplate")]
	public static ItemTemplate Create()
	{
		BuffTemplate template = ScriptableObject.CreateInstance<BuffTemplate>();
		string path = "Assets/NewBuffTemplate.asset";
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