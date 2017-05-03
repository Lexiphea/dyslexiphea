using UnityEngine;

public class BouncePad : MonoBehaviour
{
	[SerializeField]
	private float bounceForce = 24.0f;

	void OnTriggerEnter(Collider other)
	{
		MovementController movementController = other.gameObject.GetComponent<MovementController>();
		if (movementController != null)
		{
			movementController.ResetRemainingJumps(-1);
			//rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0.0f, rigidbody.velocity.y) + (this.transform.up * this.bounceForce);
		}
	}
}