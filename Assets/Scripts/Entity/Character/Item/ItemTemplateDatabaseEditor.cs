using UnityEditor;

public class ItemTemplateDatabaseEditor : DatabaseEditor<ItemTemplate>
{
	[MenuItem("Templates/Item Template Database Editor")]
	static void Init()
	{
		EditorWindow.GetWindow<ItemTemplateDatabaseEditor>();
	}
}