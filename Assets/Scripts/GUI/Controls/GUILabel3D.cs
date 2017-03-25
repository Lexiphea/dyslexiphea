using UnityEngine;

public class GUILabel3D : GUIObject3D
{
	public string Text = "";

	void OnGUI()
	{
		if (!IsVisible || Camera.main == null)
		{
			return;
		}
		Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
		UnityEngine.GUI.Label(new Rect(screenPos.x - (PixelOffset.x + Center.x), Screen.height - screenPos.y - (PixelOffset.y + Center.y), Size.x, Size.y), Text, Style);
	}

	public static GUILabel3D Create(string text, int fontSize, Transform transform)
	{
		return Create(text, "", fontSize, Color.white, false, transform, Vector2.zero);
	}
	public static GUILabel3D Create(string text, int fontSize, Transform transform, Vector2 pixelOffset)
	{
		return Create(text, "", fontSize, Color.white, false, transform, pixelOffset);
	}
	public static GUILabel3D Create(string text, int fontSize, Color color, Transform transform)
	{
		return Create(text, "", fontSize, color, false, transform, Vector2.zero);
	}
	public static GUILabel3D Create(string text, int fontSize, Color color, Transform transform, Vector2 pixelOffset)
	{
		return Create(text, "", fontSize, color, false, transform, pixelOffset);
	}
	public static GUILabel3D Create(string text, int fontSize, Color color, bool isFade, Transform transform)
	{
		return Create(text, "", fontSize, color, isFade, transform, Vector2.zero);
	}
	public static GUILabel3D Create(string text, int fontSize, Color color, bool isFade, Transform transform, Vector2 pixelOffset)
	{
		return Create(text, "", fontSize, color, isFade, transform, pixelOffset);
	}
	public static GUILabel3D Create(string text, string extraText, int fontSize, Color color, bool isFade, Transform transform, Vector2 pixelOffset)
	{
		GameObject newObject = new GameObject("GUILabel3D: " + text + extraText);
		newObject.transform.position = transform.position;
		newObject.transform.SetParent(transform);

		GUILabel3D label = (GUILabel3D)newObject.AddComponent<GUILabel3D>();
		label.Setup(text, extraText, FontStyle.Bold, null, fontSize, pixelOffset, color, isFade);
		return label;
	}

	public void Setup(string text, string extraText, FontStyle fontStyle, Font font, int fontSize, Vector2 pixelOffset, Color color, bool isFade)
	{
		Text = text;
		Style.wordWrap = false;
		Style.normal.textColor = color;
		Style.fontStyle = fontStyle;
		Style.fontSize = fontSize;
		Style.alignment = TextAnchor.MiddleCenter;
		if (font != null)
		{
			Style.font = font;
		}
		Size = Style.CalcSize(new GUIContent(Text));
		base.Setup(pixelOffset, isFade);
		text += extraText;
	}

	public void ChangeFont(int fontSize)
	{
		Style.fontSize = fontSize;
		Size = Style.CalcSize(new GUIContent(Text));
		Center = new Vector2(Size.x * 0.5f, Size.y * 0.5f);
	}

	public void SetText(string text)
	{
		SetText(text, "");
	}
	public void SetText(string text, string extraText)
	{
		Text = text;
		Size = Style.CalcSize(new GUIContent(Text));
		Center = new Vector2(Size.x * 0.5f, Size.y * 0.5f);
		Text += extraText;
	}

	public void SetColor(Color color)
	{
		Style.normal.textColor = color;
	}
}