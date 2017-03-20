using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MovementController
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