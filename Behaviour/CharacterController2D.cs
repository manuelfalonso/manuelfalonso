using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Supports:
/// Move, Jump, Crouch, OnLanding, OnCrouching, Flip.
/// Edited: ManuelFAlonso
/// Source: Brackeys & CodeMonkey
/// </summary>
public class CharacterController2D : MonoBehaviour
{
	[Tooltip("Amount of force added when the player jumps")]
	[SerializeField] private float m_JumpForce = 400f;         
	[Tooltip("Amount of maxSpeed applied to crouching movement. 1 = 100%")]
	[Range(0, 1)] 
	[SerializeField] private float m_CrouchSpeed = .36f;
	[Tooltip("How much to smooth out the movement")]
	[Range(0, .3f)] 
	[SerializeField] private float m_MovementSmoothing = .05f;
	[Tooltip("Whether or not a player can steer while jumping;")]
	[SerializeField] private bool m_AirControl = false;

	[Tooltip("A mask determining what is ground to the character")]
	[SerializeField] private LayerMask m_WhatIsGround;
	[Tooltip("A position marking where to check for ceilings")]
	[SerializeField] private Transform m_CeilingCheck;
	[Tooltip("A collider that will be disabled when crouching")]
	[SerializeField] private Collider2D m_CrouchDisableCollider;
	[Tooltip("A collider that will NOT be disabled when crouching")]
	[SerializeField] private Collider2D m_MainCollider;

	// Height of the Box cast to determine if grounded
	const float k_GroundedExtraHeight = .1f;
	// Radius of the overlap circle to determine if the player can stand up
	const float k_CeilingRadius = .2f;

	// Whether or not the player is grounded.
	private bool m_Grounded = false;
	// For determining which way the player is currently facing.
	private bool m_FacingRight = true;
	private bool m_WasCrouching = false;

	private Vector3 m_Velocity = Vector3.zero;
	private Rigidbody2D m_Rigidbody2D;

    [Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;

	[Header("Debug")]
	[Space]

	[SerializeField] private bool m_DebugGroundCheck = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		GroundedCheck();
	}

    private void GroundedCheck()
    {
		bool wasGrounded = m_Grounded;

		RaycastHit2D raycastHit = Physics2D.BoxCast(
			m_MainCollider.bounds.center, 
			m_MainCollider.bounds.size, 
			0f, 
			Vector2.down, 
			k_GroundedExtraHeight, 
			m_WhatIsGround);

        if (raycastHit.collider != null)
        {
			m_Grounded = true;
			if (!wasGrounded)
				OnLandEvent.Invoke();
        }
        else
        {
			m_Grounded = false;
		}

		// Draw a box with Debug.Ray
        if (m_DebugGroundCheck)
        {
			Color rayColor;
			if (raycastHit.collider != null)
			{
				rayColor = Color.green;
			}
			else
			{
				rayColor = Color.red;
			}

			Debug.DrawRay(
				m_MainCollider.bounds.center + new Vector3(m_MainCollider.bounds.extents.x, 0f, 0f),
				Vector2.down * (m_MainCollider.bounds.extents.y + k_GroundedExtraHeight),
				rayColor);
			Debug.DrawRay(
				m_MainCollider.bounds.center - new Vector3(m_MainCollider.bounds.extents.x, 0f, 0f),
				Vector2.down * (m_MainCollider.bounds.extents.y + k_GroundedExtraHeight),
				rayColor);
			Debug.DrawRay(
				m_MainCollider.bounds.center - new Vector3(
					m_MainCollider.bounds.extents.x,
					m_MainCollider.bounds.extents.y + k_GroundedExtraHeight,
					0f),
				Vector2.right * (m_MainCollider.bounds.extents.y * 2),
				rayColor);
		}
	}

    public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_WasCrouching)
				{
					m_WasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_WasCrouching)
				{
					m_WasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(
				m_Rigidbody2D.velocity, 
				targetVelocity, 
				ref m_Velocity, 
				m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}