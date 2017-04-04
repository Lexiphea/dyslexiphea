using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMovementController : MonoBehaviour 
{
	private Rigidbody cachedRigidbody = null;
	[SerializeField]
	private float movementSpeed = 8.0f;
	[SerializeField]
	private bool useGravity = true;
	[SerializeField]
	private float gravity = -32.0f;
	private bool fastFalling = false;
	[SerializeField]
	private float fastFallSpeed = -24.0f;
	[SerializeField]
	private float jumpSpeed = 18.0f;
	[SerializeField]
	private int remainingJumps = 2;
	[SerializeField]
	private int maxJumps = 2;
	private IRateLimiter jumpLimiter = new RateLimiter(0.33f);
	private float landingLag = 0.0f;
	private IRateLimiter landingLagLimiter = new RateLimiter(0.066f);
	[SerializeField]
	private bool grounded = false;
	private float distanceToGround = 0.0f;

	[SerializeField]
	private List<Transform> groundedRayCastOrigins = new List<Transform>();

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
		Vector3 direction = this.CalculateDirection();
		if (direction.sqrMagnitude > 1.0f)
		{
			direction.Normalize();
		}
		direction *= this.movementSpeed * Time.deltaTime;

		bool prevGrounded = this.grounded;
		this.grounded = this.CheckGrounded();
		if (this.grounded)
		{
			//Check if we just landed on ground
			if (prevGrounded != this.grounded)
			{
				this.landingLagLimiter.Reset();
				this.jumpLimiter.SetNextTick(0.0f);
				this.remainingJumps = this.maxJumps;
			}
		}

		//Jump
		if (this.TryJump() && this.jumpLimiter.IsReady && this.landingLagLimiter.IsReady)
		{
			if (!this.grounded)
			{
				this.cachedRigidbody.velocity = new Vector3(this.cachedRigidbody.velocity.x, 0.0f, this.cachedRigidbody.velocity.z);
			}
			this.cachedRigidbody.AddForce(Vector3.up * this.jumpSpeed, ForceMode.Impulse);
			--this.remainingJumps;
			this.jumpLimiter.Reset();
		}

		//FastFall
		this.fastFalling = this.TryFastFall();

		//Translate
		this.cachedRigidbody.MovePosition(this.transform.position + direction);
	}

	void FixedUpdate()
	{
		if (!this.grounded)
		{
			Vector3 downForce = Vector3.zero;
			if (this.useGravity)
			{
				downForce.y += this.gravity;
			}
			if (this.fastFalling)
			{
				downForce.y += this.fastFallSpeed;
			}
			this.cachedRigidbody.AddForce(downForce);
		}
	}

	private bool CheckGrounded()
	{
		foreach (Transform origin in this.groundedRayCastOrigins)
		{
			if (Physics.Raycast(origin.position, Vector3.down, this.distanceToGround + 0.1f))
			{
				return true;
			}
		}
		return false;
	}

	public abstract Vector3 CalculateDirection();
	public virtual bool TryJump()
	{
		return this.remainingJumps > 0;
	}
	public virtual bool TryFastFall()
	{
		return !this.grounded;
	}
}