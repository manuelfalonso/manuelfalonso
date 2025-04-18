using UnityEngine;
using System.Collections;
using SombraStudios.Shared.Attributes;

namespace SombraStudios.Shared.Gameplay.Spawners
{
    /// <summary>  
    /// Base class for spawning objects within a defined area.  
    /// Handles spawning logic and provides an abstract method for custom spawn behavior.  
    /// </summary>  
    public abstract class SpawnerAreaBase : MonoBehaviour
    {
        [Header("Object creation")]
        [SerializeField, AssetOnly]
        protected GameObject _prefabToSpawn;

        [Header("Settings")]
        [SerializeField, Min(0.1f)]
        private float _spawnInterval = 1f;

        [SerializeField]
        private bool _spawnOnStart = true;

        [Header("Debug")]
        [SerializeField]
        [Tooltip("Color used to visualize the spawn area in the editor.")]
        private Color _areaColor = Color.yellow;

        protected WaitForSeconds _waitForSpawnInterval; // Cached WaitForSeconds instance to reduce allocations.  
        private Coroutine _spawnCoroutine; // Reference to the active spawn coroutine.  

        /// <summary>  
        /// Draws a wireframe cube in the editor to visualize the spawn area.  
        /// </summary>  
        private void OnDrawGizmos()
        {
            Gizmos.color = _areaColor;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }

        /// <summary>  
        /// Initializes cached variables.  
        /// </summary>  
        protected virtual void Awake()
        {
            _waitForSpawnInterval = new WaitForSeconds(_spawnInterval);
        }

        /// <summary>  
        /// Starts spawning objects if the spawn-on-start option is enabled.  
        /// </summary>  
        private void Start()
        {
            if (_spawnOnStart)
                StartSpawningObjects();
        }

        /// <summary>  
        /// Starts the spawning coroutine if it is not already running.  
        /// </summary>  
        public void StartSpawningObjects()
        {
            if (_spawnCoroutine != null) return; // Prevent duplicate coroutines.  
            _spawnCoroutine = StartCoroutine(SpawnObject());
        }

        /// <summary>  
        /// Stops the currently running spawning coroutine.  
        /// </summary>  
        public void StopSpawningObjects()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
        }

        /// <summary>  
        /// Abstract method to define custom spawn behavior.  
        /// Implement this method in derived classes to handle object spawning.  
        /// </summary>  
        /// <returns>An IEnumerator for coroutine execution.</returns>  
        protected abstract IEnumerator SpawnObject();
    }
}
