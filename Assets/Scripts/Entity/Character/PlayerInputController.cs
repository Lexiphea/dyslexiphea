using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
	[SerializeField]
	private MovementController movementController;
	[SerializeField]
	private InteractionController interactionController;
	[SerializeField]
	private AbilityController abilityController;
	[SerializeField]
	private KeyCode[] abilityHotkeys = new KeyCode[]
	{
		KeyCode.Alpha1,
		KeyCode.Alpha2,
		KeyCode.Alpha3,
		KeyCode.Alpha4,
		KeyCode.Alpha5,
		KeyCode.Alpha6,
		KeyCode.Alpha7,
		KeyCode.Alpha8,
		KeyCode.Alpha9,
		KeyCode.Alpha0,
	};

	void Update()
	{
		if (this.movementController != null)
		{
			if (Input.GetKeyDown(KeyCode.W))
			{
				this.movementController.Jump();
			}
			if (Input.GetAxis("Vertical") < 0.0f)
			{
				this.movementController.Fastfall();
			}
			this.movementController.Move(Input.GetAxis("Horizontal"), 0.0f);
		}
		if (this.interactionController != null)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				this.interactionController.Interact();
			}
		}
		if (this.abilityController != null)
		{
			for (int i = 0; i < this.abilityController.HotkeyCount && i < this.abilityHotkeys.Length; ++i)
			{
				if (Input.GetKeyDown(this.abilityHotkeys[i]))
				{
					this.abilityController.UseAbility(i);
				}
			}
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				this.abilityController.Interrupt();
			}
		}
	}
}