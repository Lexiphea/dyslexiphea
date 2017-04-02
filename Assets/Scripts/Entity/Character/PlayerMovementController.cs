using UnityEngine;

public class PlayerMovementController : BaseMovementController
{
	public override Vector3 CalculateDirection()
	{
		return new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
	}

	public override bool TryJump()
	{
		return Input.GetAxis("Vertical")>0.0f;
	}
}