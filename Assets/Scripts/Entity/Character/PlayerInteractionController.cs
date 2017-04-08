using UnityEngine;

public class PlayerInteractionController : BaseInteractionController
{
	public override bool TryInteract()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			return true;
		}
		return false;
	}
}