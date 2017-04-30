using UnityEngine;

public class Teleporter : BaseInteractable
{
	[SerializeField]
	private Transform destinationTransform = null;
	[SerializeField]
	private Vector3 destination = Vector3.zero;

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
		*/
	}

	public override void OnInteractEnd(InteractionController controller)
	{
		/*
		play teleport activated animation
		play teleport activated audio
		resume input
		*/
		controller.gameObject.transform.position = this.destination;
	}
}