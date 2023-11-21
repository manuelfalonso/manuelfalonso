using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Gameplay.PlayerController
{
	/// <summary>
	/// Supports: Move, Jump, Crouch, OnLanding, OnCrouching and Flip.
	/// Features: Coyote Time, Input Buffer and Corner Correction.
	/// Edited: Manuel F. Alonso
	/// Source: Brackeys, CodeMonkey & Alva Majo + DEValen
	/// Require: A layer defined for the ground
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D))]
	public class CharacterController2D : MonoBehaviour
	{
		[Tooltip("Amount of force added when the player jumps")]
		[SerializeField] private float m_JumpForce = 600f;         
		[Tooltip("Amount of maxSpeed applied to crouching movement. 1 = 100%")]
		[Range(0, 1)] 
		[SerializeField] private float m_CrouchSpeed = .36f;
		[Tooltip("How much to smooth out the movement")]
		[Range(0, .3f)] 
		[SerializeField] private float m_MovementSmoothing = .05f;
		[Tooltip("Whether or not a player can steer while jumping")]
		[SerializeField] private bool m_AirControl = false;
		[Tooltip("Air time limit for jumping")]
		[SerializeField] private float m_CoyoteTime = 0.1f;
		[Tooltip("Max number of Input stored")]
		[SerializeField] private int m_InputBufferLimit = 1;
		[Tooltip("Corner correction height")]
		[SerializeField] private float m_CornerCorrectionHeight = 0.5f;

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
		private float m_AirTime = 0f;

		private Vector3 m_Velocity = Vector3.zero;
		private Rigidbody2D m_Rigidbody2D;
		// KeyCode instead of bool for combos.
		private Queue<bool> m_InputBuffer = new Queue<bool>();

		[System.Serializable]
		public class BoolEvent : UnityEvent<bool> { }

		[Header("Events")]
		[Space]

		public BoolEvent OnCrouchEvent;
		public BoolEvent OnJumpingEvent;

		[Header("Debug")]
		[Space]

		[SerializeField] private bool m_DebugGroundCheck = false;
		[SerializeField] private bool m_DebugCornerCorrection = false;

		private void Awake()
		{
			m_Rigidbody2D = GetComponent<Rigidbody2D>();

			if (OnCrouchEvent == null)
				OnCrouchEvent = new BoolEvent();

			if (OnJumpingEvent == null)
				OnJumpingEvent = new BoolEvent();
		}

		private void FixedUpdate()
		{
			GroundedCheck();
			CornerCorrection();
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
				// Reset time counter
				m_AirTime = 0f;
				m_Grounded = true;
				if (!wasGrounded)
					OnJumpingEvent.Invoke(false);
			}
			else
			{
				m_Grounded = false;
				// Add time to counter
				m_AirTime += Time.fixedDeltaTime;
			}

			// Draw a box with Debug.Ray
			if (m_DebugGroundCheck)
			{
				DrawDebugGroundCheckRays(raycastHit);
			}
		}

		private void DrawDebugGroundCheckRays(RaycastHit2D rayCastToCheck)
		{
			Color rayColor;
			// Change ray color depending if its colliding
			if (rayCastToCheck.collider != null)
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

		public void Move(float move, bool crouch, bool jump)
		{
			// If crouching, check to see if the character can stand up
			if (m_WasCrouching && !crouch)
			{
				// If the character has a ceiling preventing them from standing up, keep them crouching
				if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
				{
					crouch = true;
				}
			}

			// Only control the player if grounded or airControl is turned on
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

			if (jump)
			{
				if (m_InputBuffer.Count < m_InputBufferLimit)
				{
					m_InputBuffer.Enqueue(jump);
					Invoke("InputDequeue", 0.5f);
				}
			}
		
			// Clamp velocity to avoid accumulate Jump forces
			m_Rigidbody2D.velocity = Vector2.ClampMagnitude(m_Rigidbody2D.velocity, 12f);

			if (m_InputBuffer.Count <= 0)
				return;

			if (m_InputBuffer.Peek() != true)
				return;

			if (m_Grounded)
			{
				// Add a vertical force to the player.
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				m_InputBuffer.Dequeue();
				OnJumpingEvent.Invoke(true);
				return;
			}

			if (m_AirTime < m_CoyoteTime && m_Rigidbody2D.velocity.y <= 0f)
			{
				// Add a vertical force to the player.
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				m_InputBuffer.Dequeue();
				OnJumpingEvent.Invoke(true);
			}
		}

		private void CornerCorrection()
		{
			// Left Raycast
			RaycastHit2D leftRaycast = Physics2D.Raycast(
				m_MainCollider.bounds.center 
					+ new Vector3(0f, m_MainCollider.bounds.extents.y)
					- new Vector3(m_MainCollider.bounds.extents.x, 0f),
				Vector2.up,
				m_CornerCorrectionHeight,
				m_WhatIsGround
				);
			// Right Raycast
			RaycastHit2D rightRaycast = Physics2D.Raycast(
				m_MainCollider.bounds.center
					+ new Vector3(0f, m_MainCollider.bounds.extents.y)
					+ new Vector3(m_MainCollider.bounds.extents.x, 0f),
				Vector2.up,
				m_CornerCorrectionHeight,
				m_WhatIsGround
				);

			// Draw Rays for each Corner
			if (m_DebugCornerCorrection)
				DrawDebugCornerCorrectionRays(leftRaycast, rightRaycast);

			// Modify object position obly if its jumping
			if (m_Rigidbody2D.velocity.y <= 1f)
				return;

			// If left raycast is hitting and not the right one
			if (leftRaycast && !rightRaycast)
			{
				transform.position += new Vector3(0.2f, 0f);
			}
			// If right raycast is hitting and not the left one
			else if (rightRaycast && !leftRaycast)
			{
				transform.position -= new Vector3(0.2f, 0f);
			}
		}

		private void DrawDebugCornerCorrectionRays(RaycastHit2D leftRayCast, RaycastHit2D rightRayCast)
		{
			Color leftRayColor;
			Color rightRayColor;
			// Change ray color depending if its colliding
			if (leftRayCast.collider != null)
			{
				leftRayColor = Color.green;
			}
			else
			{
				leftRayColor = Color.red;
			}
			if (rightRayCast.collider != null)
			{
				rightRayColor = Color.green;
			}
			else
			{
				rightRayColor = Color.red;
			}

			// Must be the same rays as CornerCorrection method
			Debug.DrawRay(
				m_MainCollider.bounds.center
					+ new Vector3(0f, m_MainCollider.bounds.extents.y)
					- new Vector3(m_MainCollider.bounds.extents.x, 0f),
				Vector2.up * m_CornerCorrectionHeight,
				leftRayColor);

			Debug.DrawRay(
			m_MainCollider.bounds.center
				+ new Vector3(0f, m_MainCollider.bounds.extents.y)
				+ new Vector3(m_MainCollider.bounds.extents.x, 0f),
        		Vector2.up* m_CornerCorrectionHeight,
				rightRayColor);
		}

		private void InputDequeue()
		{
			if (m_InputBuffer.Count > 0)
			{
				m_InputBuffer.Dequeue();
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
}