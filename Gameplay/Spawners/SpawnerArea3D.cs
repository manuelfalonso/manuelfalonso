using UnityEngine;
using System.Collections;

namespace SombraStudios.Shared.Gameplay.Spawners
{
    /// <summary>  
    /// 3D Area prefab Spawner with interval.  
    /// Spawns objects at random positions within a 3D rectangular area at regular intervals.  
    /// WARNING: This script doesn't work with rotation of the spawn area.  
    /// </summary>  
    public class SpawnerArea3D : SpawnerAreaBase
    {
        /// <summary>  
        /// Cached half size of the spawn area to optimize calculations.  
        /// </summary>  
        private Vector3 _halfSize;

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
        /// Coroutine that spawns objects at random positions within the defined 3D bounds at regular intervals.  
        /// </summary>  
        /// <returns>An IEnumerator for coroutine execution.</returns>  
        protected override IEnumerator SpawnObject()
        {
            while (true)
            {
                Vector3 randomPosition = GetRandomPositionWithinBounds();
                Instantiate(_prefabToSpawn, randomPosition, Quaternion.identity);
                yield return _waitForSpawnInterval;
            }
        }

        /// <summary>  
        /// Calculates a random position within the bounds of the 3D spawn area.  
        /// </summary>  
        /// <returns>A Vector3 representing a random position within the spawn area.</returns>  
        private Vector3 GetRandomPositionWithinBounds()
        {
            Vector3 randomOffset = new(
                Random.Range(-_halfSize.x, _halfSize.x),
                Random.Range(-_halfSize.y, _halfSize.y),
                Random.Range(-_halfSize.z, _halfSize.z)
            );
            return transform.position + randomOffset;
        }
    }
}
