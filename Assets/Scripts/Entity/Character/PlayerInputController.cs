using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
	[SerializeField]
	private MovementController movementController;
	[SerializeField]
	private InteractionController interactionController;

	void Update()
	{
		if (this.movementController != null)
		{
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
			{
				this.movementController.Jump();
			}
			if (Input.GetAxis("Vertical") < 0.0f)
			{
				this.movementController.Fastfall();
			}
			this.movementController.MoveXZ(Input.GetAxis("Horizontal"), 0.0f);
		}
		if (this.interactionController != null)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				this.interactionController.Interact();
			}
		}
	}
}