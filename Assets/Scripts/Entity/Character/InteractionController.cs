using UnityEngine;

public class InteractionController : MonoBehaviour
{
	[SerializeField]
	private BaseInteractable currentInteractable;
	[SerializeField]
	private bool isInteracting;

	public BaseInteractable CurrentInteractable { get { return this.currentInteractable; } set { this.currentInteractable = value; } }
	public bool IsInteracting { get { return this.isInteracting; } }

	public void Interact()
	{
		if (!this.isInteracting && this.currentInteractable != null)
		{
			this.isInteracting = true;
			this.currentInteractable.Interact(this);
			this.isInteracting = false;
		}
	}
}