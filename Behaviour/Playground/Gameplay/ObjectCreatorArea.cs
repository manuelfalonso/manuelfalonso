using UnityEngine;
using System.Collections;

/// <summary>
/// Spawner area with interval
/// </summary>
[AddComponentMenu("Playground/Gameplay/Object Creator Area")]
[RequireComponent(typeof(BoxCollider2D))]
public class ObjectCreatorArea : MonoBehaviour
{
	[Header("Object creation")]

	[Tooltip("The object to spawn " +
		"WARNING: take if from the Project panel, NOT the Scene/Hierarchy!")]
	public GameObject prefabToSpawn;

	[Header("Other options")]

	[Tooltip("Configure the spawning pattern")]
	public float spawnInterval = 1f;

	private BoxCollider2D boxCollider2D;

	void Start()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();

		StartCoroutine(SpawnObject());
	}

	/// <summary>
	/// This will spawn an object, and then wait some time, then spawn another...
	/// </summary>
	/// <returns></returns>
	IEnumerator SpawnObject()
	{
		while (true)
		{
			// Create some random numbers
			float randomX = Random.Range(-boxCollider2D.size.x, boxCollider2D.size.x) * .5f;
			float randomY = Random.Range(-boxCollider2D.size.y, boxCollider2D.size.y) * .5f;

			// Generate the new object
			GameObject newObject = Instantiate(prefabToSpawn);
			newObject.transform.position = new Vector2(randomX + this.transform.position.x, randomY + this.transform.position.y);

			// Wait for some time before spawning another object
			yield return new WaitForSeconds(spawnInterval);
		}
	}
}
