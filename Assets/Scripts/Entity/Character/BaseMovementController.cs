using UnityEngine;

public abstract class BaseMovementController : MonoBehaviour 
{
	private Rigidbody cachedRigidbody = null;
	[SerializeField]
	private Vector3 direction = Vector3.zero;
	[SerializeField]
	private float movementSpeed = 4.0f;
	[SerializeField]
	private float gravity = -9.81f;
	private bool fastFalling = false;
	[SerializeField]
	private float fastFallSpeed = -8.0f;
	[SerializeField]
	private float jumpSpeed = 12.0f;
	[SerializeField]
	private int remainingJumps = 2;
	[SerializeField]
	private int maxJumps = 2;
	private float nextJumpTime = 0.0f;
	[SerializeField]
	private float jumpDelay = 0.66f;
	private float lastLandingTime = 0.0f;
	[SerializeField]
	private float landingDelay = 0.066f;
	[SerializeField]
	private bool grounded = false;
	private float distanceToGround = 0.0f;

	void Awake()
	{
		this.cachedRigidbody = this.gameObject.GetComponent<Rigidbody>();
		Collider c = this.gameObject.GetComponent<Collider>();
		if (c != null) 
		{
			this.distanceToGround = c.bounds.extents.y;
		}
	}

	void Update()
	{
		this.direction = this.CalculateDirection();

		bool prevGrounded = this.grounded;
		this.grounded = this.IsGrounded();
		//Check if we just landed on ground
		if (this.grounded && prevGrounded != this.grounded) 
		{
			this.nextJumpTime = 0.0f;
			this.remainingJumps = this.maxJumps;
		}

		//Jump
		if (this.TryJump()) 
		{
			this.cachedRigidbody.AddForce(Vector3.up * this.jumpSpeed, ForceMode.Impulse);
			--this.remainingJumps;
			this.nextJumpTime = Time.time + this.jumpDelay;
		}

		//FastFall
		this.fastFalling = this.TryFastFall();
	}

	void FixedUpdate()
	{
		this.direction *= this.movementSpeed;
		this.direction.y += this.gravity;
		if (this.fastFalling)
		{
			this.direction.y += this.fastFallSpeed;
		}
		this.cachedRigidbody.AddForce(this.direction, ForceMode.Force);
	}

	private bool IsGrounded()
	{
		return Physics.Raycast(this.transform.position, Vector3.down, this.distanceToGround + 0.1f);
	}

	public abstract Vector3 CalculateDirection();
	public virtual bool TryJump()
	{
		return this.remainingJumps > 0 && Time.time >= this.nextJumpTime && (Time.time - this.lastLandingTime) >= this.landingDelay;
	}
	public virtual bool TryFastFall()
	{
		return !this.grounded;
	}
}