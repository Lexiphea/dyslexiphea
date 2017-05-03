using UnityEngine;

public class Teleporter : BaseInteractable
{
	[SerializeField]
	private Transform destinationTransform = null;
	[SerializeField]
	private Vector3 destination = Vector3.zero;
	[SerializeField]
	private float teleportDelay = 0.0f;

	public Transform DestinationTransform
	{
		get
		{
			return this.destinationTransform;
		}
		set
		{
			if (value != null)
			{
				this.destinationTransform = value;
				this.destination = destinationTransform.position;
			}
			else
			{
				this.destinationTransform = null;
				this.destination = Vector3.zero;
			}
		}
	}

	void Awake()
	{
		DestinationTransform = this.destinationTransform;
	}

	public override void OnInteractStart(InteractionController controller)
	{
		/*
		play teleport activation animation
		play teleport activation audio
		freeze input
		delay
		*/
	}

	public override void OnInteractEnd(InteractionController controller)
	{
		/*
		play teleport activated animation
		play teleport activated audio
		resume input
		*/
		MovementController movementController = controller.gameObject.GetComponent<MovementController>();
		if (movementController != null)
		{
			controller.gameObject.transform.position = this.destination - new Vector3(0.0f, movementController.Extents.y, 0.0f);
		}
		else
		{
			controller.gameObject.transform.position = this.destination;
		}
	}
}