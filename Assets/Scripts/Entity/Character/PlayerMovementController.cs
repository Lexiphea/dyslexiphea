using UnityEngine;

public class PlayerMovementController : BaseMovementController
{
	public override Vector3 CalculateDirection()
	{
		return new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f);
	}

	public override bool TryJump()
	{
		return base.TryJump() && Input.GetAxis("Vertical") > 0.0f;
	}

	public override bool TryFastFall()
	{
		return base.TryFastFall() && Input.GetAxis("Vertical") < 0.0f;
	}
}