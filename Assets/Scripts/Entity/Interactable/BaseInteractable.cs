using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		InteractionController interactionController = other.gameObject.GetComponent<InteractionController>();
		if (interactionController != null)
		{
			interactionController.CurrentInteractable = this;
		}
	}

	void OnTriggerExit(Collider other)
	{
		InteractionController interactionController = other.gameObject.GetComponent<InteractionController>();
		if (interactionController != null)
		{
			interactionController.CurrentInteractable = null;
		}
	}

	public void Interact(InteractionController controller)
	{
		this.OnInteractStart(controller);
		this.OnInteractEnd(controller);
	}
	public virtual void OnInteractStart(InteractionController controller) { }
	public virtual void OnInteractEnd(InteractionController controller) { }
}