using UnityEngine;

public abstract class BaseInteractionController : MonoBehaviour
{
	[SerializeField]
	private BaseInteractable currentInteractable;
	[SerializeField]
	private bool isInteracting;

	public BaseInteractable CurrentInteractable { get { return this.currentInteractable; } set { this.currentInteractable = value; } }
	public bool IsInteracting { get { return this.isInteracting; } }

	void Update()
	{
		if (!this.isInteracting && this.currentInteractable != null && this.TryInteract())
		{
			this.isInteracting = true;
			this.currentInteractable.Interact(this);
			this.isInteracting = false;
		}
	}

	public abstract bool TryInteract();
}