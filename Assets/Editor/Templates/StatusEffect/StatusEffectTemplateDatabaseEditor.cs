using UnityEditor;

public class StatusEffectTemplateDatabaseEditor : DatabaseEditor<StatusEffectTemplate>
{
	[MenuItem("Templates/Status Effect/Database Editor")]
	static void Init()
	{
		EditorWindow.GetWindow<StatusEffectTemplateDatabaseEditor>();
	}

	void Awake()
	{
		this.newDatabaseTitle = "New Status Effect Database";
		this.newDatabaseDefaultName = "NewStatusEffectDatabase";
		this.newItemTitle = "New Status Effect";
		this.newItemDefaultName = "NewStatusEffect";
	}
}