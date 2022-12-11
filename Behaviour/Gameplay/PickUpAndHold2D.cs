using UnityEngine;

/// <summary>
/// Pick up and hold objects script using parenting and de parenting.
/// Required:
/// The PickUp object must have a "Pickup" tag
/// </summary>
public class PickUpAndHold2D : MonoBehaviour
{
	[Tooltip("Pickup key and drop key could be the same")]
	public KeyCode pickupKey = KeyCode.B;
	public KeyCode dropKey = KeyCode.B;

	[Tooltip("An object need to be closer than that distance to be picked up.")]
	public float pickUpDistance = 2f;

	private Transform carriedObject = null;

	private void Update()
	{
		bool justPickedUpSomething = false;

		if (Input.GetKeyDown(pickupKey)
			&& carriedObject == null)
		{
			// Nothing in hand, we check if something is around and pick it up.
			justPickedUpSomething = PickUp();
		}

		if (Input.GetKeyDown(dropKey)
			&& carriedObject != null
			&& !justPickedUpSomething)
		{
			// We're holding something already, we drop
			Drop();
		}
	}

	public void Drop()
	{
		Rigidbody2D rb2d = carriedObject.GetComponent<Rigidbody2D>();
		if (rb2d != null)
		{
			rb2d.bodyType = RigidbodyType2D.Dynamic;
			rb2d.velocity = Vector2.zero;
		}
		// Unparenting
		carriedObject.parent = null;
		// Hands are free again
		carriedObject = null;
	}

	public bool PickUp()
	{
		//Collect every Pickup around
		GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

		// Find the closest
		float dist = pickUpDistance;
		for (int i = 0; i < pickups.Length; i++)
		{
			float newDist = (transform.position - pickups[i].transform.position).sqrMagnitude;
			if (newDist < dist)
			{
				carriedObject = pickups[i].transform;
				dist = newDist;
			}
		}

		// Check if we found something
		if (carriedObject != null)
		{
			// Check if another player had it, in this case, steal it
			Transform pickupParent = carriedObject.parent;
			if (pickupParent != null)
			{
				PickUpAndHold2D pickupScript = pickupParent.GetComponent<PickUpAndHold2D>();
				if (pickupScript != null)
				{
					pickupScript.Drop();
				}
			}

			carriedObject.parent = gameObject.transform;
			// Set to Kinematic so it will move with the Player
			Rigidbody2D rb2d = carriedObject.GetComponent<Rigidbody2D>();
			if (rb2d != null)
			{
				rb2d.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
			}
			return true;
		}
		else
		{
			return false;
		}
	}
}