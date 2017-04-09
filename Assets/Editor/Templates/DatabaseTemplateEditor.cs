using UnityEditor;

public abstract class DatabaseTemplateEditor<T> : Editor where T : DatabaseTemplate
{
	public override void OnInspectorGUI()
	{
		T template = (T)target;
		template.OnGUILayout();
	}
}