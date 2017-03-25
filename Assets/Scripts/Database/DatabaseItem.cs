using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

[Serializable]
public abstract class DatabaseItem : ScriptableObject
{
	[SerializeField]
	private string identifier = string.Empty;
	public string Identifier { get { return this.identifier; } }

	public abstract void OnGUILayout();

	void OnEnable()
	{
		if (this.identifier == string.Empty)
		{
			this.identifier = GetNewGuid<DatabaseItem>();
		}
	}

	public static string GetNewGuid<T>() where T : DatabaseItem
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

	public static T[] GetAssetsOfType<T>(string fileExtension) where T : UnityEngine.Object
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