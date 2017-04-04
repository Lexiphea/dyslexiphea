using UnityEditor;

public class BuffTemplateDatabaseEditor : DatabaseEditor<BuffTemplate>
{
	[MenuItem("Templates/Buff/Template Database Editor")]
	static void Init()
	{
		EditorWindow.GetWindow<BuffTemplateDatabaseEditor>();
	}
}