using UnityEngine;
using System.Collections;

namespace SombraStudios.Gameplay.Movement2D
{

	/// <summary>
	/// Wander script using forces
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D))]
	public class Wander : MonoBehaviour
	{
		[Header("Movement")]

		public float speed = 1f;
		public float directionChangeInterval = 3f;
		public bool keepNearStartingPoint = true;

		[Header("Orientation")]

		public bool orientToDirection = false;
		[Tooltip("The direction that the GameObject will be oriented to")]
		public Enums.Directions lookAxis = Enums.Directions.Up;

		private Vector2 direction;
		private Vector3 startingPoint;

		private new Rigidbody2D rigidbody2D;

		void Awake()
		{
			rigidbody2D = GetComponent<Rigidbody2D>();
		}
	
		// Start is called at the beginning of the game
		private void Start()
		{
			// We don't want directionChangeInterval to be 0, so we force it to a
			// minimum value ;)
			if (directionChangeInterval < 0.1f)
			{
				directionChangeInterval = 0.1f;
			}

			// We note down the initial position of the GameObject in case it has
			// to hover around that (see keepNearStartingPoint)
			startingPoint = transform.position;

			StartCoroutine(ChangeDirection());
		}

		// Calculates a new direction
		private IEnumerator ChangeDirection()
		{
			while (true)
			{
				//change the direction the player is going
				direction = Random.insideUnitCircle; 

				// if we need to keep near the starting point...
				if (keepNearStartingPoint)
				{
					// we measure the distance from it...
					float distanceFromStart = Vector2.Distance(startingPoint, transform.position);
					if (distanceFromStart > 1f + (speed * 0.1f)) // and if it's too much...
					{
						//... we get a direction that points back to the starting point
						direction = (startingPoint - transform.position).normalized;
					}
				}

				//if the object has to look in the direction of movement
				if (orientToDirection)
				{
					Utils.SetAxisTowards(lookAxis, transform, direction);
				}

				// this will make Unity wait for some time before continuing 
				// the execution of the code
				yield return new WaitForSeconds(directionChangeInterval);
			}
		}

		// FixedUpdate is called every frame when the physics are calculated
		private void FixedUpdate()
		{
			rigidbody2D.AddForce(direction * speed);
		}
	}
}
