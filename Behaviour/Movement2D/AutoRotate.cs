using UnityEngine;

/// <summary>
/// Constant rotate in the speed selected.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class AutoRotate : MonoBehaviour
{
	[Tooltip("This is the force that rotate the object every frame")]
	[SerializeField] private float _rotationSpeed = 5;

	private float _currentRotation;

	private Rigidbody2D _rigidbody2D;

	void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// FixedUpdate is called once per frame
	void FixedUpdate()
    {
        Rotate();
    }

    private void Rotate()
    {
        // Find the right rotation, according to speed
        _currentRotation += .02f * _rotationSpeed * 10f;

        // Apply the rotation to the Rigidbody2d
        _rigidbody2D.MoveRotation(-_currentRotation);
    }
}
