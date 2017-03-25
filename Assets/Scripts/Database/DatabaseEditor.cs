using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class DatabaseEditor<T> : EditorWindow where T : DatabaseItem
{
	private const int DATABASE_CONTROL_ID = int.MaxValue - 2;
	private const int ITEM_CONTROL_ID = int.MaxValue - 1;

	private Database newDatabase;
	private Database currentDatabase;

	private Vector2 scrollPosition;
	private DatabaseItem newItem;
	private DatabaseItem selectedItem;

	void Update()
	{
		if (this.newItem != null)
		{
			this.currentDatabase.Add(this.newItem);
			this.selectedItem = this.newItem;
			this.newItem = null;
			EditorUtility.SetDirty(this.currentDatabase);
		}
		else if (this.newDatabase != null)
		{
			this.currentDatabase = this.newDatabase;
			this.newDatabase = null;
			this.selectedItem = null;
		}
	}

	void OnGUI()
	{
		EditorGUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.MinWidth(640.0f), GUILayout.MaxWidth(1080.0f));
		if (GUILayout.Button("New Database"))
		{
			string path = EditorUtility.SaveFilePanelInProject("New Database", "NewDatabase", "asset", "");
			if (path != null && path.Length > 0)
			{
				Database currentDatabase = ScriptableObject.CreateInstance<Database>();
				path = AssetDatabase.GenerateUniqueAssetPath(path);
				AssetDatabase.CreateAsset(currentDatabase, path);
				AssetDatabase.SaveAssets();
				this.newDatabase = currentDatabase;
			}
		}
		if (GUILayout.Button("Load Database"))
		{
			EditorGUIUtility.ShowObjectPicker<Database>(null, false, "", DATABASE_CONTROL_ID);
		}
		EditorGUILayout.EndHorizontal();

		if (this.currentDatabase != null)
		{
			GUILayout.Space(12);
			EditorGUILayout.BeginHorizontal(GUILayout.MinWidth(640.0f), GUILayout.MaxWidth(1080.0f));
			GUILayout.FlexibleSpace();
			string newDatabaseName = GUILayout.TextField(this.currentDatabase.name, GUILayout.Width(360.0f));
			if (newDatabaseName != this.currentDatabase.name)
			{
				string path = AssetDatabase.GetAssetPath(this.currentDatabase);
				AssetDatabase.RenameAsset(path, newDatabaseName);
			}
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
			GUILayout.Space(12);

			EditorGUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.MinWidth(640.0f), GUILayout.MaxWidth(1080.0f));
				this.scrollPosition = EditorGUILayout.BeginScrollView(this.scrollPosition, EditorStyles.helpBox, GUILayout.Width(200.0f));
				foreach (KeyValuePair<string, DatabaseItem> pair in this.currentDatabase.Items)
				{
					Color defaultColor = GUI.color;
					if (pair.Value == this.selectedItem)
					{
						GUI.color = Color.green;
					}
					if (GUILayout.Button(pair.Value.name))
					{
						this.selectedItem = pair.Value;
					}
					GUI.color = defaultColor;
				}
				EditorGUILayout.EndScrollView();

				if (this.selectedItem != null)
				{
					this.selectedItem.OnGUILayout();
				}
			GUILayout.EndHorizontal();

			GUILayout.BeginHorizontal(EditorStyles.helpBox, GUILayout.MinWidth(640.0f), GUILayout.MaxWidth(1080.0f));
				if (GUILayout.Button("New Item"))
				{
					string path = EditorUtility.SaveFilePanelInProject("New Item", "NewItem", "asset", "");
					if (path != null && path.Length > 0)
					{
						T template = ScriptableObject.CreateInstance<T>();
						path = AssetDatabase.GenerateUniqueAssetPath(path);
						AssetDatabase.CreateAsset(template, path);
						AssetDatabase.SaveAssets();
						this.newItem = template;
					}
				}
				if (GUILayout.Button("Add Existing Item"))
				{
					EditorGUIUtility.ShowObjectPicker<T>(null, false, "", ITEM_CONTROL_ID);
				}
				if (GUILayout.Button("Remove") && this.selectedItem != null)
				{
					this.currentDatabase.Remove(this.selectedItem.Identifier);
					this.selectedItem = null;
				}
			GUILayout.EndHorizontal();
		}

		if (Event.current.commandName == "ObjectSelectorClosed")
		{
			if (EditorGUIUtility.GetObjectPickerControlID() == DATABASE_CONTROL_ID)
			{
				Database currentDatabase = EditorGUIUtility.GetObjectPickerObject() as Database;
				if (currentDatabase != null)
				{
					this.newDatabase = currentDatabase;
				}
			}
			else if (EditorGUIUtility.GetObjectPickerControlID() == ITEM_CONTROL_ID)
			{
				T template = EditorGUIUtility.GetObjectPickerObject() as T;
				if (template != null)
				{
					this.newItem = template;
				}
			}
		}
	}
}