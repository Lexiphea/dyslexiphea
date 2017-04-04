﻿using UnityEngine;
using UnityEditor;

public class BuffTemplate : DatabaseItem
{
	public string Description = "NO_DESCRIPTION";
	public float Duration = 0.0f;
	public float TickRate = 0.0f;
	[Tooltip("Resets the duration of the buff when applied again if true.")]
	public bool ResetDuration = true;
	public int MaxStacks = 1;

	[MenuItem("Templates/Buff/New Template")]
	public static BuffTemplate Create()
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

	public void OnApply(GameObject target)
	{

	}

	public void OnApplyTick(GameObject target, int numStacks)
	{

	}

	public void OnRemove(GameObject target)
	{

	}
}