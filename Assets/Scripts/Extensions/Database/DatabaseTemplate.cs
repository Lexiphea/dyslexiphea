using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[Serializable]
public abstract class DatabaseTemplate : ScriptableObject
{
	[SerializeField]
	private string identifier = string.Empty;
	public string Identifier { get { return this.identifier; } }

	public void OnGUILayout()
	{
		EditorGUILayout.BeginVertical(EditorStyles.helpBox);
		OnGUILayoutBegin();
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Name:");
		string newName = EditorGUILayout.TextField(this.name);
		if (newName != this.name)
		{
			string path = AssetDatabase.GetAssetPath(this);
			AssetDatabase.RenameAsset(path, newName);
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Identifier:");
		EditorGUILayout.TextField(this.Identifier.ToString());
		EditorGUILayout.EndHorizontal();
		OnGUILayoutEnd();
		EditorGUILayout.EndVertical();
	}

	/// <summary>
	/// Executed at the very beginning of OnGUILayout.
	/// </summary>
	protected virtual void OnGUILayoutBegin() { }

	/// <summary>
	/// Executed at the very end of OnGUILayout.
	/// </summary>
	protected virtual void OnGUILayoutEnd() { }

#if UNITY_EDITOR
	void OnEnable()
	{
		if (this.identifier == string.Empty)
		{
			this.identifier = GetNewGuid<DatabaseTemplate>();
		}
	}
#endif

	/// <summary>
	/// Retrieves a list of all DatabaseTemplates of type T and ensures a unique identifier is returned.
	/// </summary>
	private string GetNewGuid<T>() where T : DatabaseTemplate
	{
		HashSet<string> usedIdentifiers = new HashSet<string>();

		//We can't use the .meta identifiers because they can change if the file is moved outside of the Unity editor...
		T[] assets = GetAssetsOfType<T>(".asset");
		for (int i = 0; i < assets.Length; ++i)
		{
			usedIdentifiers.Add(assets[i].Identifier);
		}

		string newIdentifier = Guid.NewGuid().ToString();
		while (usedIdentifiers.Contains(newIdentifier))
		{
			newIdentifier = Guid.NewGuid().ToString();
		}
		return newIdentifier;
	}

	private T[] GetAssetsOfType<T>(string fileExtension) where T : UnityEngine.Object
	{
		List<T> assets = new List<T>();
		DirectoryInfo directory = new DirectoryInfo(Application.dataPath);
		FileInfo[] files = directory.GetFiles("*" + fileExtension, SearchOption.AllDirectories);

		for (int i = 0; i < files.Length; ++i)
		{
			string path = files[i].FullName.Replace("\\", "/").Replace(Application.dataPath, "Assets");
			T asset = AssetDatabase.LoadAssetAtPath<T>(path);
			if (asset != null)
			{
				assets.Add(asset);
			}
		}

		return assets.ToArray();
	}
}