using UnityEngine;

public class MovementController : MonoBehaviour 
{
	public class RaycastOrigins
	{
		private Vector3 bottomLeft;
		private Vector3 bottomRight;
		private Vector3 topLeft;
		private Vector3 topRight;

		public Vector3 BottomLeft { get { return this.bottomLeft; } }
		public Vector3 BottomRight { get { return this.bottomRight; } }
		public Vector3 TopLeft { get { return this.topLeft; } }
		public Vector3 TopRight { get { return this.topRight; } }

		public RaycastOrigins(Vector3 bottomLeft, Vector3 bottomRight, Vector3 topLeft, Vector3 topRight)
		{
			this.bottomLeft = bottomLeft;
			this.bottomRight = bottomRight;
			this.topLeft = topLeft;
			this.topRight = topRight;
		}
	}

	private const float RaycastOriginExpansionWidth = -0.01f;
	private const float MinimumDistanceFromGround = 0.01f;

	private Rigidbody cachedRigidbody = null;
	private Collider cachedCollider = null;

	[SerializeField]
	private LayerMask rayLayermask;
	private RaycastOrigins raycastOrigins = null;
	[SerializeField]
	[Tooltip("RayCount will be automatically calculated based on the objects local scale if set to true.")]
	private bool calculateRayCount = true;
	[SerializeField]
	private int rayCountX = 2;
	[SerializeField]
	private int rayCountY = 2;
	[SerializeField]
	private int rayCountZ = 2;
	[SerializeField]
	private Vector3 raycastSpacing = Vector3.zero;

	[SerializeField]
	private float groundedMoveSpeed = 8.0f;
	[SerializeField]
	private float airMoveSpeed = 8.0f;
	[SerializeField]
	private bool useGravity = true;
	[SerializeField]
	private float gravity;
	[SerializeField]
	private bool grounded = false;
	[SerializeField]
	private bool fastFalling = false;
	[SerializeField]
	private bool canFastFall = true;
	[SerializeField]
	[Tooltip("The bonus gravity modifier applied in the air after pressing down. (Ex: 1.0f = 100% more gravity)")]
	private float fastFallModifier = 0.75f;
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

	public Vector3 rbVelocity = Vector3.zero;
	public Vector3 direction = Vector3.zero;

	void Awake()
	{
		this.cachedRigidbody = this.gameObject.GetComponent<Rigidbody>();
		this.cachedCollider = this.gameObject.GetComponent<Collider>();
		this.raycastOrigins = this.CalculateRaycastOrigins();
		this.raycastSpacing = this.CalculateRaySpacing();

		this.gravity = -(2.0f * this.jumpHeight) / Mathf.Pow(this.timeToJumpApex, 2.0f);
		this.jumpVelocity = this.gravity.Absolute() * this.timeToJumpApex;
	}

	void Update()
	{
		this.raycastOrigins = this.CalculateRaycastOrigins();
		rbVelocity = this.cachedRigidbody.velocity;
		
		this.grounded = this.IsGrounded();
	}

	void FixedUpdate()
	{
		this.direction *= this.grounded ? this.groundedMoveSpeed : this.airMoveSpeed;
		if (!this.grounded)
		{
			if (this.useGravity)
			{
				this.direction.y += this.gravity;
			}
			if (this.fastFalling)
			{
				this.direction.y += this.gravity * this.fastFallModifier;
			}
		}
		///this.cachedRigidbody.velocity
		this.cachedRigidbody.AddForce(this.direction);
	}

	private void DebugRaycasts()
	{
		//horizontal
		for (int y = 0; y < this.rayCountY; ++y)
		{
			//left
			Debug.DrawRay(this.raycastOrigins.BottomLeft + Vector3.up * this.raycastSpacing.y * y, Vector3.left, Color.red);
			//right
			Debug.DrawRay(this.raycastOrigins.BottomRight + Vector3.up * this.raycastSpacing.y * y, Vector3.right, Color.red);
		}
		//vertical
		for (int x = 0; x < this.rayCountX; ++x)
		{
			//up
			Debug.DrawRay(this.raycastOrigins.TopLeft + Vector3.right * this.raycastSpacing.x * x, Vector3.up, Color.red);
			//down
			Debug.DrawRay(this.raycastOrigins.BottomLeft + Vector3.right * this.raycastSpacing.x * x, Vector3.down, Color.red);
		}
	}

	/// <summary>
	/// Calculates and returns the Raycast Origin positions of the cached collider.
	/// </summary>
	private RaycastOrigins CalculateRaycastOrigins()
	{
		if (this.cachedCollider != null)
		{
			Bounds bounds = this.cachedCollider.bounds;
			bounds.Expand(MovementController.RaycastOriginExpansionWidth * 2.0f);

			return new RaycastOrigins(new Vector3(bounds.min.x, bounds.min.y, 0.0f),
									  new Vector3(bounds.max.x, bounds.min.y, 0.0f),
									  new Vector3(bounds.min.x, bounds.max.y, 0.0f),
									  new Vector3(bounds.max.x, bounds.max.y, 0.0f));
		}
		return null;
	}

	/// <summary>
	/// Calculates and returns the spacing between each ray for the cached collider. RayCount will be calculated based on localScale if calculateRayCount is true.
	/// </summary>
	private Vector3 CalculateRaySpacing()
	{
		if (this.cachedCollider != null)
		{
			Bounds bounds = this.cachedCollider.bounds;
			bounds.Expand(MovementController.RaycastOriginExpansionWidth * 2.0f);

			if (this.calculateRayCount)
			{
				this.rayCountX = Mathf.RoundToInt(this.transform.localScale.x) + 1;
				this.rayCountY = Mathf.RoundToInt(this.transform.localScale.y) + 1;
				this.rayCountZ = Mathf.RoundToInt(this.transform.localScale.z) + 1;
			}

			//clamp the count to a minimum of 2
			this.rayCountX = this.rayCountX.Clamp(2, int.MaxValue);
			this.rayCountY = this.rayCountY.Clamp(2, int.MaxValue);
			this.rayCountZ = this.rayCountZ.Clamp(2, int.MaxValue);

			return new Vector3(bounds.size.x / (this.rayCountX - 1),
							   bounds.size.y / (this.rayCountY - 1),
							   bounds.size.z / (this.rayCountZ - 1));
		}
		return Vector3.zero;
	}

	/// <summary>
	/// Moves the object on the X and Z axis.
	/// </summary>
	public void MoveXZ(float x, float z)
	{
		this.direction = new Vector3(x, 0.0f, z).normalized;
	}

	/// <summary>
	/// Attempts to make the object jump.
	/// </summary>
	public void Jump()
	{
		if (this.remainingJumps > 0 && this.jumpRateLimiter.IsReady && this.landingLagLimiter.IsReady)
		{
			this.cachedRigidbody.velocity = new Vector3(this.cachedRigidbody.velocity.x, this.jumpVelocity, this.cachedRigidbody.velocity.z);
			--this.remainingJumps;
			this.jumpRateLimiter.Reset();
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
	/// Checks if the object is grounded. If the object just landed, OnLanded is called.
	/// </summary>
	private bool IsGrounded()
	{
		bool prevGrounded = this.grounded;
		bool grounded = false;

		for (int x = 0; x < this.rayCountX; ++x)
		{
			Vector3 rayOrigin = this.raycastOrigins.BottomLeft + Vector3.right * this.raycastSpacing.x * x;
			RaycastHit hit;
			if (Physics.Raycast(rayOrigin, Vector3.down, out hit, this.cachedRigidbody.velocity.y + 1.0f, rayLayermask))
			{
				//Debug.DrawRay(rayOrigin, Vector3.down * hit.distance, Color.red);
				if (hit.distance < MovementController.MinimumDistanceFromGround)
				{
					grounded = true;
					break;
				}
			}
		}
		//check if the object just landed
		if (grounded && prevGrounded != grounded)
		{
			OnLanded();
		}
		return grounded;
	}

	private void IsCollidingWithWall()
	{
		
	}

	/// <summary>
	/// Called automatically when the object lands.
	/// </summary>
	protected virtual void OnLanded()
	{
		this.fastFalling = false;
		this.landingLagLimiter.Reset();
		this.jumpRateLimiter.SetNextTick(0.0f);
		this.remainingJumps = this.maxJumps;
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