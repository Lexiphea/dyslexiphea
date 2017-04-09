using UnityEditor;

public class AbilityTemplateTemplateDatabaseEditor : DatabaseEditor<AbilityTemplate>
{
	[MenuItem("Templates/Ability/Database Editor")]
	static void Init()
	{
		EditorWindow.GetWindow<AbilityTemplateTemplateDatabaseEditor>();
	}

	void Awake()
	{
		this.newDatabaseTitle = "New Ability Database";
		this.newDatabaseDefaultName = "NewAbilityDatabase";
		this.newItemTitle = "New Ability";
		this.newItemDefaultName = "NewAbility";
	}
}