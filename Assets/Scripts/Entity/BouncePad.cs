using UnityEngine;

public class BouncePad : MonoBehaviour
{
	[SerializeField]
	private float bounceForce = 24.0f;

	void OnTriggerEnter(Collider other)
	{
		Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
		if (rb != null)
		{
			BaseMovementController movement = other.gameObject.GetComponent<BaseMovementController>();
			if (movement != null)
			{
				movement.ResetRemainingJumps(-1);
			}
			rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.y);
			rb.AddForce(this.transform.up * bounceForce, ForceMode.Impulse);
		}
	}
}