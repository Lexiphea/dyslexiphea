using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMovementController : MonoBehaviour 
{

	private Rigidbody cachedRigidbody = null;
	[SerializeField]
	private Vector3 direction = Vector3.zero;
	[SerializeField]
	private float movementSpeed = 4;
	[SerializeField]
	private float gravity = -0.987f;
	[SerializeField]
	private float jumpSpeed = 6.0f;
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
	protected bool grounded = true;
	private float distanceToGround = 0.0f;

	public Vector3 Velocity = Vector3.zero;

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
		this.Velocity = this.cachedRigidbody.velocity;
		this.direction = this.CalculateDirection();

		bool prevGrounded = this.grounded;
		this.grounded = this.IsGrounded();
		if (this.grounded && prevGrounded != this.grounded) 
		{
			this.remainingJumps = this.maxJumps;
		}

		if (this.TryJump() && this.remainingJumps > 0 && Time.time >= this.nextJumpTime && (Time.time - this.lastLandingTime) >= this.landingDelay) 
		{
			this.cachedRigidbody.AddForce(Vector3.up * this.jumpSpeed, ForceMode.Impulse);
			--this.remainingJumps;
			this.nextJumpTime = Time.time + this.jumpDelay;	
		}
	}

	void FixedUpdate()
	{
		this.direction *= this.movementSpeed;
		this.direction.y += this.gravity;
		this.cachedRigidbody.AddForce(this.direction);
	}

	private bool IsGrounded()
	{
		return Physics.Raycast(this.transform.position, Vector3.down, this.distanceToGround + 0.1f);
	}

	public abstract Vector3 CalculateDirection();
	public abstract bool TryJump();
}