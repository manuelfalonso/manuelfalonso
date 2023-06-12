using UnityEngine;
using System.Collections;

namespace SombraStudios.Gameplay.Spawners
{

	/// <summary>
	/// 3D Area Object Spawner with interval
	/// WARINING!: This script doesnt work with rotation of the spawn area
	/// </summary>
	[RequireComponent(typeof(BoxCollider))]
	public class ObjectCreatorArea3D : MonoBehaviour
	{
		[Header("Object creation")]

		[Tooltip("The object to spawn " +
			"WARNING: take if from the Project panel, NOT the Scene/Hierarchy!")]
		public GameObject prefabToSpawn;

		[Header("Other options")]

		[Tooltip("Configure the spawning pattern")]
		public float spawnInterval = 1f;
		public bool spawnOnStart = true;

		private BoxCollider _boxCollider;

		void Start()
		{
			_boxCollider = GetComponent<BoxCollider>();
			// Make the collider not affect physics
			_boxCollider.isTrigger = true;

			if (spawnOnStart)
				StartSpawningObjects();
		}

		/// <summary>
		/// This will spawn an object, and then wait some time, then spawn another...
		/// </summary>
		private IEnumerator SpawnObject()
		{
			while (true)
			{
				// Create some random numbers
				float randomX = Random.Range(
					-_boxCollider.size.x, _boxCollider.size.x) * .5f;
				float randomY = Random.Range(
					-_boxCollider.size.y, _boxCollider.size.y) * .5f;
				float randomZ = Random.Range(
					-_boxCollider.size.z, _boxCollider.size.z) * .5f;

				// Generate the new object
				GameObject newObject = Instantiate(prefabToSpawn);
				newObject.transform.position = new Vector3(
					randomX + transform.position.x,
					randomY + transform.position.y,
					randomZ + transform.position.z);

				// Wait for some time before spawning another object
				yield return new WaitForSeconds(spawnInterval);
			}
		}

		/// <summary>
		/// Start spawning objects with the time interval set
		/// </summary>
		public void StartSpawningObjects()
		{
			StartCoroutine(SpawnObject());
		}

		/// <summary>
		/// Stop spawning objects
		/// </summary>
		public void StopSpawningObjects()
		{
			StopAllCoroutines();
		}
	}
}
