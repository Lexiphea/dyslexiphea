using UnityEngine;

public class GUIObject3D : MonoBehaviour
{
	public bool IsVisible = false;
	public bool IsFade = false;

	public Vector2 Size = Vector2.zero;
	public GUIStyle Style = new GUIStyle();
	public Vector2 Center = Vector2.zero;
	public Vector2 PixelOffset = Vector2.zero;

	//fade
	public float FadeStartTime = 0.0f;
	public float FadeTime = 2.0f;
	public float OldY = 0.0f;
	public float IncreaseY = 200.0f;
	public Color OldColor = Color.clear;
	public Color ClearColor = Color.clear;

	//bounce
	public float Bounce = 0.0f;
	public float BounceDecay = 0.1f;

	void Update()
	{
		if (IsFade)
		{
			float t = Mathf.Clamp((Time.time - FadeStartTime) / FadeTime, 0.0f, 1.0f);
			if (t >= 1.0f)
			{
				Destroy(this.gameObject);
			}
			Color c = Style.normal.textColor;
			c.a = Mathf.Lerp(OldColor.a, ClearColor.a, t);
			Style.normal.textColor = c;
			PixelOffset = new Vector2(PixelOffset.x, Mathf.Lerp(OldY, IncreaseY, t));
		}
	}

	public void Setup(Vector2 pixelOffset)
	{
		Setup(pixelOffset, IsFade);
	}

	public void Setup(Vector2 pixelOffset, bool isFade)
	{
		Center = new Vector2(Size.x * 0.5f, Size.y * 0.5f);
		PixelOffset = pixelOffset;
		IsFade = isFade;
		IsVisible = true;
		if (!isFade)
		{
			return;
		}
		OldY = PixelOffset.y;
		IncreaseY = OldY + IncreaseY;
		FadeStartTime = Time.time;
		OldColor = Style.normal.textColor;
	}
}