using UnityEngine;

public class BouncePad : MonoBehaviour
{
	[SerializeField]
	private float bounceForce = 24.0f;

	void OnTriggerEnter(Collider other)
	{
		Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
		if (rigidbody != null)
		{
			MovementController movementController = other.gameObject.GetComponent<MovementController>();
			if (movementController != null)
			{
				movementController.ResetRemainingJumps(-1);
			}
			rigidbody.velocity = new Vector3(rigidbody.velocity.x, this.bounceForce, rigidbody.velocity.y);
		}
	}
}