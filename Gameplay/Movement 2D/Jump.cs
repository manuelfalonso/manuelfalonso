using UnityEngine;

namespace SombraStudios.Gameplay.Movement2D
{

	/// <summary>
	/// Jump script with ground check
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D))]
	public class Jump : MonoBehaviour
	{
		[Header("Jump setup")]
		[Tooltip("The key used to activate the push")]
		[SerializeField] private KeyCode _key = KeyCode.Space;

		[Tooltip("Strength of the push")]
		[SerializeField] private float _jumpStrength = 10f;

		[Header("Ground setup")]
		[Tooltip("If the object collides with another object tagged as this, it can jump again")]
		[SerializeField] private string _groundTag = "Ground";

		[Tooltip("This determines if the script has to check for when the player " +
			"touches the ground to enable him to jump again if not, the player can " +
			"jump even while in the air")]
		[SerializeField] private bool _checkGround = true;

		private bool _canJump = true;

		private Rigidbody2D _rigidbody2D;

		void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		// Read the input from the player
		void Update()
		{
			CheckJump();
		}

		private void CheckJump()
		{
			if (_canJump
				&& Input.GetKeyDown(_key))
			{
				// Apply an instantaneous upwards force
				_rigidbody2D.AddForce(Vector2.up * _jumpStrength, ForceMode2D.Impulse);
				_canJump = !_checkGround;
			}
		}

		private void OnCollisionEnter2D(Collision2D collisionData)
		{
			if (_checkGround
				&& collisionData.gameObject.CompareTag(_groundTag))
			{
				_canJump = true;
			}
		}
	}
}