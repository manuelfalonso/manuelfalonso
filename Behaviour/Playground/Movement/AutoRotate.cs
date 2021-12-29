using UnityEngine;

/// <summary>
/// Constant rotate in the speed selected.
/// </summary>
[AddComponentMenu("Playground/Movement/Auto Rotate")]
[RequireComponent(typeof(Rigidbody2D))]
public class AutoRotate : Physics2DObject
{
	[Tooltip("This is the force that rotate the object every frame")]
	public float rotationSpeed = 5;

	private float currentRotation;

	// FixedUpdate is called once per frame
	void FixedUpdate()
	{
		// Find the right rotation, according to speed
		currentRotation += .02f * rotationSpeed * 10f;

		// Apply the rotation to the Rigidbody2d
		rigidbody2D.MoveRotation(-currentRotation);
	}
}
