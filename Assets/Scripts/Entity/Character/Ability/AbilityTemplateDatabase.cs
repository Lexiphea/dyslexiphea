using UnityEditor;

public class AbilityTemplateDatabaseEditor : DatabaseEditor<AbilityTemplate>
{
	[MenuItem("Templates/Ability/Template Database Editor")]
	static void Init()
	{
		EditorWindow.GetWindow<AbilityTemplateDatabaseEditor>();
	}
}