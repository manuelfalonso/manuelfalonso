using UnityEngine;
using System.Collections;

namespace SombraStudios.Shared.Gameplay.Spawners
{
    /// <summary>  
    /// SpawnerArea2D is responsible for spawning objects within a 2D rectangular area.  
    /// It inherits from SpawnerAreaBase and implements the spawning logic for 2D spaces.  
    /// WARNING: This script doesn't work with rotation of the spawn area.  
    /// </summary>  
    public class SpawnerArea2D : SpawnerAreaBase
    {
        private Vector2 _halfSize; // Cache half size to avoid recalculating  

        /// <summary>  
        /// Called when the script instance is being loaded.  
        /// Initializes the cached half size of the spawn area.  
        /// </summary>  
        protected override void Awake()
        {
            base.Awake();
            _halfSize = transform.localScale * 0.5f; // Precompute half size of the area  
        }

        /// <summary>  
        /// Coroutine that spawns objects at random positions within the defined bounds at regular intervals.  
        /// </summary>  
        /// <returns>An IEnumerator for coroutine execution.</returns>  
        protected override IEnumerator SpawnObject()
        {
            while (true)
            {
                Vector2 randomPosition = GetRandomPositionWithinBounds();
                Instantiate(_prefabToSpawn, randomPosition, Quaternion.identity);
                yield return _waitForSpawnInterval;
            }
        }

        /// <summary>  
        /// Calculates a random position within the bounds of the 2D area.  
        /// </summary>  
        /// <returns>A Vector2 representing a random position within the spawn area.</returns>  
        private Vector2 GetRandomPositionWithinBounds()
        {
            Vector2 randomOffset = new(
                Random.Range(-_halfSize.x, _halfSize.x),
                Random.Range(-_halfSize.y, _halfSize.y)
            );
            return (Vector2)transform.position + randomOffset;
        }
    }
}
