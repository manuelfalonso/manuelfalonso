using UnityEngine;

namespace SombraStudios.Shared.Systems.Teleport
{
    /// <summary>
    /// Transform gameObject used as Teleporter destination.
    /// </summary>
    public class SpawnPoint : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private string _spawnName;

        /// <summary>
        /// Gets or sets the name of the spawn point.
        /// </summary>
        public string SpawnName
        {
            get => _spawnName;
            set => _spawnName = value;
        }

        /// <summary>
        /// Registers the spawn point with the TeleportManager when the object is initialized.
        /// </summary>
        private void Start()
        {
            TeleportManager.Instance.RegisterSpawnPoint(this);
        }

        /// <summary>
        /// Unregisters the spawn point from the TeleportManager when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            TeleportManager.Instance.UnregisterSpawnPoint(this);
        }
    }
}
