using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GUIControl : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField]
	private bool startEnabled;
	private bool hasFocus;

	public bool IsVisible { get { return this.gameObject.activeSelf; } }
	public bool HasFocus { get { return this.hasFocus; } }

	protected virtual void Awake()
	{
		GUIManager.Register(this);
		this.gameObject.SetActive(startEnabled);
	}

	protected virtual void OnDestroy()
	{
		GUIManager.Unregister(this);
	}

	public virtual void OnShow()
	{
		this.gameObject.SetActive(true);
	}

	public virtual void OnHide()
	{
		this.gameObject.SetActive(false);
	}

	public virtual void OnPointerEnter(PointerEventData data)
	{
		this.hasFocus = true;
	}

	public virtual void OnPointerExit(PointerEventData data)
	{
		this.hasFocus = false;
	}
}