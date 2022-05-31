using UnityEngine;

/// <summary>
/// Constant movement the direction selected.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class AutoMove : Physics2DObject
{
	[Tooltip("These are the forces that will push the object every frame " +
		"don't forget they can be negative too!")]
	public Vector2 direction = new Vector2(1f, 0f);

	[Tooltip("Is the push relative or absolute to the world ?")]
	public bool relativeToRotation = true;

	// FixedUpdate is called once per frame
	void FixedUpdate()
	{
		if (relativeToRotation)
		{
			rigidbody2D.AddRelativeForce(direction * 2f);
		}
		else
		{
			rigidbody2D.AddForce(direction * 2f);
		}
	}
}
