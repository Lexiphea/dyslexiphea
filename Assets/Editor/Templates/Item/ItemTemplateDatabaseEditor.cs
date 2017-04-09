using UnityEditor;

public class ItemTemplateDatabaseEditor : DatabaseEditor<ItemTemplate>
{
	[MenuItem("Templates/Item/Database Editor")]
	static void Init()
	{
		EditorWindow.GetWindow<ItemTemplateDatabaseEditor>();
	}

	void Awake()
	{
		this.newDatabaseTitle = "New Item Database";
		this.newDatabaseDefaultName = "NewItemDatabase";
		this.newItemTitle = "New Item";
		this.newItemDefaultName = "NewItem";
	}
}