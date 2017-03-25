using UnityEngine;
using UnityEngine.EventSystems;

public class GUIDraggable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
	private RectTransform rectTransform;
	[SerializeField]
	private bool clampToScreen;
	private Vector2 startPosition;
	private Vector2 dragOffset = Vector2.zero;
	private bool isDragging;

	void Awake()
	{
		this.rectTransform = this.transform as RectTransform;
		if (this.rectTransform == null)
		{
			throw new UnityException("GUIDraggable must be attached to a Unity3D 4.6+ GUI control.");
		}
		this.startPosition = this.transform.position;
	}

	public void OnPointerDown(PointerEventData data)
	{
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.rectTransform, data.pressPosition, data.pressEventCamera, out this.dragOffset))
		{
			this.isDragging = true;
		}
		else
		{
			this.dragOffset = Vector2.zero;
		}
	}

	public void OnPointerUp(PointerEventData data)
	{
		this.isDragging = false;
	}

	public void OnDrag(PointerEventData data)
	{
		if (this.isDragging)
		{
			float x = data.position.x - this.dragOffset.x;
			float y = data.position.y - this.dragOffset.y;
			if (this.clampToScreen)
			{
				float halfWidth = this.rectTransform.rect.width * 0.5f;
				float halfHeight = this.rectTransform.rect.height * 0.5f;
				x = Mathf.Clamp(x, halfWidth, Screen.width - halfWidth);
				y = Mathf.Clamp(y, halfHeight, Screen.height - halfHeight);
			}
			this.transform.position = new Vector2(x, y);
		}
	}

	public void ResetPosition()
	{
		this.transform.position = startPosition;
		this.dragOffset = Vector2.zero;
		this.isDragging = false;
	}
}