using UnityEngine;

public class MovementController : MonoBehaviour 
{
	public struct CollisionHitInfo
	{
		public readonly float Distance;
		public readonly Vector3 Normal;

		public CollisionHitInfo(float distance, Vector3 normal)
		{
			Distance = distance;
			Normal = normal;
		}
	}

	private const float MinimumMoveMagnitude = 0.001f;
	private const float ColliderPadding = 0.01f;

	private Rigidbody cachedRigidbody = null;
	private Collider cachedCollider = null;
	private Vector3 extents = Vector3.zero;
	[SerializeField]
	private LayerMask rayLayermask;
	[SerializeField]
	private float groundedMoveSpeed = 32.0f;
	[SerializeField]
	private float airMoveSpeed = 32.0f;
	[SerializeField]
	private float maxFallVelocity = -32.0f;
	[SerializeField]
	private bool useGravity = true;
	[SerializeField]
	private float gravity = -32.0f;
	[SerializeField]
	private bool grounded = false;
	[SerializeField]
	private float minGroundedSlope = 0.5f;
	[SerializeField]
	private Vector3 groundNormal = Vector3.zero;
	[SerializeField]
	private bool fastFalling = false;
	[SerializeField]
	private bool canFastFall = true;
	[SerializeField]
	[Tooltip("The bonus gravity modifier applied in the air after pressing down. (Ex: 1.0f = 100% more gravity)")]
	private float fastFallModifier = 0.75f;
	[SerializeField]
	private bool isJumping = false;
	[SerializeField]
	private float jumpHeight = 4.0f;
	[SerializeField]
	private float timeToJumpApex = 0.5f;
	[SerializeField]
	private float jumpVelocity;
	[SerializeField]
	private int remainingJumps = 2;
	[SerializeField]
	private int maxJumps = 2;
	private IRateLimiter jumpRateLimiter = new RateLimiter(0.33f);
	private IRateLimiter landingLagLimiter = new RateLimiter(0.066f);

	public Vector3 velocity = Vector3.zero;
	public Vector3 targetDirection = Vector3.zero;
	public Vector3 deltaMove = Vector3.zero;
	public Vector3 move = Vector3.zero;

	/// <summary>
	/// Gets the collider bounding box extents with ColliderPadding subtracted from each axis.
	/// </summary>
	public Vector3 Extents
	{
		get
		{
			return this.extents;
		}
	}

	public float Gravity
	{
		get
		{
			return this.gravity;
		}
		set
		{
			this.gravity = value;
			this.jumpVelocity = this.CalculateJumpVelocity();
		}
	}

	public float JumpHeight
	{
		get
		{
			return this.jumpHeight;
		}
		set
		{
			this.jumpHeight = value;
			this.gravity = this.CalculateGravity();
			this.jumpVelocity = this.CalculateJumpVelocity();
		}
	}

	public float TimeToJumpApex
	{
		get
		{
			return this.timeToJumpApex;
		}
		set
		{
			this.timeToJumpApex = value;
			this.gravity = this.CalculateGravity();
			this.jumpVelocity = this.CalculateJumpVelocity();
		}
	}

	void Awake()
	{
		this.cachedRigidbody = this.gameObject.GetComponent<Rigidbody>();
		this.cachedCollider = this.gameObject.GetComponent<Collider>();
		if (this.cachedCollider != null)
		{
			this.extents = new Vector3(this.cachedCollider.bounds.extents.x - ColliderPadding, this.cachedCollider.bounds.extents.y - ColliderPadding, this.cachedCollider.bounds.extents.z - ColliderPadding);
		}
		this.gravity = this.CalculateGravity();
		this.jumpVelocity = this.CalculateJumpVelocity();
	}

	void FixedUpdate()
	{
		this.targetDirection *= (this.grounded ? this.groundedMoveSpeed : this.airMoveSpeed) * Time.deltaTime;

		if (this.isJumping)
		{
			this.isJumping = false;
			--this.remainingJumps;
			this.jumpRateLimiter.Reset();
			this.velocity = new Vector3(this.targetDirection.x, this.jumpVelocity, this.targetDirection.z);
		}
		else
		{
			this.velocity = new Vector3(this.targetDirection.x, this.CalculateVelocityY(), this.targetDirection.z);
		}
		this.deltaMove = this.velocity * Time.deltaTime;

		if (deltaMove.magnitude > MinimumMoveMagnitude)
		{
			CollisionHitInfo hit;
			if (this.CheckCollision(deltaMove, out hit))
			{
				this.groundNormal = hit.Normal;
				if (this.groundNormal.y >= this.minGroundedSlope)
				{
					bool prevGrounded = this.grounded;
					this.grounded = true;
					if (prevGrounded != this.grounded)
					{
						this.OnLanded();
					}
				}
				float projection = Vector3.Dot(this.velocity, hit.Normal);
				if (projection < 0.0f)
				{
					this.velocity -= projection * this.groundNormal;
				}
				this.deltaMove = this.deltaMove.normalized * hit.Distance;

				Vector3 groundNormalPerpendicularVector = new Vector3(this.groundNormal.y, -this.groundNormal.x, 0.0f);
				Vector3 move = groundNormalPerpendicularVector * this.deltaMove.x;
				move += Vector3.up * deltaMove.y;
				this.cachedRigidbody.position = this.cachedRigidbody.position + move;
			}
			else
			{
				this.grounded = false;
				this.groundNormal = Vector3.zero;
				this.cachedRigidbody.position = this.cachedRigidbody.position + this.deltaMove;
			}
		}
	}

	private bool CheckCollision(Vector3 deltaMove, out CollisionHitInfo hit)
	{
		float distance = deltaMove.magnitude;
		float sign = distance.Sign();
		Vector3 direction = deltaMove.normalized;
		RaycastHit rayHit;
		if (Physics.BoxCast(this.cachedCollider.bounds.center, this.cachedCollider.bounds.extents, direction, out rayHit, this.cachedRigidbody.rotation, distance + ColliderPadding, this.rayLayermask))
		{
			float hitDistance = (rayHit.distance - ColliderPadding) * sign;
			distance = hitDistance < distance ? hitDistance : distance;
			hit = new CollisionHitInfo(distance, rayHit.normal);
			return true;
		}
		hit = default(CollisionHitInfo);
		return false;
	}

	/// <summary>
	/// Called when the object lands.
	/// </summary>
	protected virtual void OnLanded()
	{
		this.fastFalling = false;
		this.landingLagLimiter.Reset();
		this.jumpRateLimiter.SetNextTick(0.0f);
		this.remainingJumps = this.maxJumps;
	}

	/// <summary>
	/// Calculates the total Y velocity.
	/// </summary>
	private float CalculateVelocityY()
	{
		float yVelocity = this.velocity.y;
		yVelocity += this.CalculateGravityModifier() * Time.deltaTime;
		yVelocity += this.targetDirection.y;
		return yVelocity.Min(this.maxFallVelocity);
	}

	/// <summary>
	/// Returns the total gravity modifier to apply to velocity.
	/// </summary>
	private float CalculateGravityModifier()
	{
		if (this.useGravity && !this.grounded)
		{
			float gravity = 0.0f;
			if (this.useGravity)
			{
				gravity += this.gravity;
			}
			if (this.fastFalling)
			{
				gravity += this.gravity * this.fastFallModifier;
			}
			return gravity;
		}
		return 0.0f;
	}

	/// <summary>
	/// Sets the desired movement direction on the x and z axis.
	/// </summary>
	public void Move(float x, float z)
	{
		this.targetDirection = new Vector3(x, 0.0f, z).normalized;
	}

	/// <summary>
	/// Sets the desired movement direction on the x, y, and z axis.
	/// </summary>
	public void Move(Vector3 direction)
	{
		this.targetDirection = direction.normalized;
	}

	/// <summary>
	/// Attempts to make the object jump.
	/// </summary>
	public void Jump()
	{
		if (!this.isJumping && this.remainingJumps > 0 && this.jumpRateLimiter.IsReady && this.landingLagLimiter.IsReady)
		{
			this.isJumping = true;
		}
	}

	/// <summary>
	/// Enables fast falling.
	/// </summary>
	public void Fastfall()
	{
		if (this.canFastFall && !this.grounded)
		{
			this.fastFalling = true;
		}
	}

	/// <summary>
	/// Calculates the gravity applied to the controller based on jump height and time to reach the jump apex.
	/// </summary>
	public float CalculateGravity()
	{
		return -(2.0f * this.jumpHeight) / Mathf.Pow(this.timeToJumpApex, 2.0f);
	}

	/// <summary>
	/// Calculates the jump velocity required to reach the jump apex.
	/// </summary>
	public float CalculateJumpVelocity()
	{
		return this.gravity.Absolute() * this.timeToJumpApex;
	}

	public void ResetRemainingJumps()
	{
		this.ResetRemainingJumps(0);
	}

	public void ResetRemainingJumps(int modifier)
	{
		this.remainingJumps = this.maxJumps + modifier;
	}
}