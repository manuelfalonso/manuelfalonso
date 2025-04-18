using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SombraStudios.Shared.Gameplay.Spawners
{
    /// <summary>  
    /// Handles spawning objects at predefined positions with specific settings.  
    /// Supports multiple spawn configurations and spawn points.  
    /// </summary>  
    public class SpawnerPosition : MonoBehaviour
    {
        [Header("Settings")]
        [Tooltip("List of spawn configurations. Each configuration can have its own settings and spawn points.")]
        [SerializeField] private List<ActiveSpawn> _activeSpawns = new();

        [Header("Debug")]
        [Tooltip("Enables debug logs for spawn operations.")]
        [SerializeField] private bool _showLogs;

        /// <summary>  
        /// Called on the first frame. Starts the spawning process for all active spawns.  
        /// </summary>  
        private void Start()
        {
            StartSpawning();
        }

        /// <summary>  
        /// Iterates through all active spawns and starts their spawning coroutine if enabled.  
        /// </summary>  
        private void StartSpawning()
        {
            foreach (var spawn in _activeSpawns)
            {
                if (spawn.SpawnData.SpawnAtStart)
                {
                    StartCoroutine(SpawnCoroutine(spawn));
                }
            }
        }

        /// <summary>  
        /// Coroutine that handles spawning objects for a specific spawn configuration.  
        /// </summary>  
        /// <param name="spawn">The spawn configuration to process.</param>  
        /// <returns>An IEnumerator for coroutine execution.</returns>  
        private IEnumerator SpawnCoroutine(ActiveSpawn spawn)
        {
            var spawnData = spawn.SpawnData;
            yield return new WaitForSeconds(spawnData.TimeBetweenSpawns.RandomValue);

            while (true)
            {
                Spawn(spawn);
                yield return new WaitForSeconds(spawnData.TimeBetweenSpawns.RandomValue);
            }
        }

        /// <summary>  
        /// Decreases the current spawn count for a specific spawn configuration.  
        /// </summary>  
        /// <param name="spawnToDecrease">The spawn configuration to decrease.</param>  
        public void DecreaseSpawn(SpawnData spawnToDecrease)
        {
            var spawn = _activeSpawns.Find(x => x.SpawnData == spawnToDecrease);

            if (spawn == null)
            {
                Debug.LogWarning("Spawn not found in active spawns list.", this);
                return;
            }

            spawn.CurrentSpawns--;
        }

        /// <summary>  
        /// Spawns an object based on the provided spawn configuration.  
        /// </summary>  
        /// <param name="spawn">The spawn configuration to use.</param>  
        public void Spawn(ActiveSpawn spawn)
        {
            if (spawn.IsActive == false || spawn.CurrentSpawns >= spawn.SpawnData.MaxSpawns) return;

            var objectToSpawn = spawn.SpawnData.ObjectToSpawn[Random.Range(0, spawn.SpawnData.ObjectToSpawn.Length)];
            var spawnPoint = GetRandomSpawnPoint(spawn);

            Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
            spawn.CurrentSpawns++;
        }

        /// <summary>  
        /// Selects a random spawn point from the list of spawn points in the configuration.  
        /// Ensures no consecutive repeats if the option is disabled.  
        /// </summary>  
        /// <param name="spawn">The spawn configuration to use.</param>  
        /// <returns>A Transform representing the selected spawn point.</returns>  
        private Transform GetRandomSpawnPoint(ActiveSpawn spawn)
        {
            if (spawn.SpawnData.AllowConsecutiveRepeatSpawns)
            {
                return spawn.SpawnPoints[Random.Range(0, spawn.SpawnPoints.Count)];
            }

            Transform spawnPoint;
            do
            {
                spawnPoint = spawn.SpawnPoints[Random.Range(0, spawn.SpawnPoints.Count)];
            } while (spawnPoint == spawn.PreviousSpawnPoint);

            spawn.PreviousSpawnPoint = spawnPoint;
            return spawnPoint;
        }

        /// <summary>  
        /// Represents an active spawn configuration, including settings and spawn points.  
        /// </summary>  
        [System.Serializable]
        public class ActiveSpawn
        {
            [Header("Spawn Settings")]
            [Tooltip("Whether this spawn configuration is active.")]
            public bool IsActive = true;
            [Tooltip("The spawn data defining the behavior of this configuration.")]
            public SpawnData SpawnData;
            [Tooltip("List of spawn points for this configuration.")]
            public List<Transform> SpawnPoints = new(); 

            [Header("Debug")]
            [Tooltip("The last spawn point used for this configuration.")]
            public Transform PreviousSpawnPoint;
            [Tooltip("The current number of spawned objects for this configuration.")]
            public int CurrentSpawns;
        }
    }
}
