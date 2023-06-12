using UnityEngine;

namespace SombraStudios.Gameplay.Movement2D
{

	/// <summary>
	/// Constant movement the direction selected.
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D))]
	public class AutoMove : MonoBehaviour
	{
		[Tooltip("These are the forces that will push the object every frame")]
		[SerializeField] private Vector2 _direction = new Vector2(1f, 0f);

		[Tooltip("Is the push relative or absolute to the world ?")]
		[SerializeField] private bool _relativeToRotation = true;

		private new Rigidbody2D _rigidbody2D;

		void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}

		// FixedUpdate is called once per frame
		void FixedUpdate()
		{
			if (_relativeToRotation)
			{
				_rigidbody2D.AddRelativeForce(_direction * 2f);
			}
			else
			{
				_rigidbody2D.AddForce(_direction * 2f);
			}
		}
	}
}
