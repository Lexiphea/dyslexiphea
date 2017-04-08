using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		BaseInteractionController interactionController = other.gameObject.GetComponent<BaseInteractionController>();
		if (interactionController != null)
		{
			interactionController.CurrentInteractable = this;
		}
	}

	void OnTriggerExit(Collider other)
	{
		BaseInteractionController interactionController = other.gameObject.GetComponent<BaseInteractionController>();
		if (interactionController != null)
		{
			interactionController.CurrentInteractable = null;
		}
	}

	public void Interact(BaseInteractionController controller)
	{
		this.OnInteractStart(controller);
		this.OnInteractEnd(controller);
	}
	public virtual void OnInteractStart(BaseInteractionController controller) { }
	public virtual void OnInteractEnd(BaseInteractionController controller) { }
}