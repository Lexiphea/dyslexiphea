using System;
using UnityEngine;
using UnityEditor;

[Serializable]
public class ItemTemplate : DatabaseItem
{
	[NonSerialized]
	private Editor meshEditor;
	[NonSerialized]
	private Editor materialEditor;

	public GameObject MeshPrefab;
	public Material Material;

	[MenuItem("Assets/Create/Templates/Item/New Template")]
	public static ItemTemplate Create()
	{
		ItemTemplate template = ScriptableObject.CreateInstance<ItemTemplate>();
		string path = "Assets/NewItemTemplate.asset";
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

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Mesh Prefab:");
		GameObject newMeshPrefab = EditorGUILayout.ObjectField(this.MeshPrefab, typeof(GameObject), false) as GameObject;
		if (newMeshPrefab != this.MeshPrefab)
		{
			this.MeshPrefab = newMeshPrefab;
			DestroyImmediate(this.meshEditor);
			this.meshEditor = Editor.CreateEditor(this.MeshPrefab);
		}
		if (this.meshEditor != null)
		{
			this.meshEditor.OnPreviewGUI(GUILayoutUtility.GetRect(100, 100), EditorStyles.foldout);
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Material:");
		Material newMaterial = EditorGUILayout.ObjectField(this.Material, typeof(Material), false) as Material;
		if (newMaterial != this.Material)
		{
			this.Material = newMaterial;
			DestroyImmediate(this.materialEditor);
			this.materialEditor = Editor.CreateEditor(this.Material);
		}
		if (this.materialEditor != null)
		{
			this.materialEditor.OnPreviewGUI(GUILayoutUtility.GetRect(100, 100), EditorStyles.foldout);
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();
	}
}