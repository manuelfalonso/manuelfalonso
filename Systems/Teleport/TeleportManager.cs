using System.Collections.Generic;

namespace SombraStudios.Shared.Systems.Teleport
{
    /// <summary>
    /// Manages the registration and unregistration of spawn points.
    /// </summary>
    public class TeleportManager : Patterns.Creational.Singleton.PersistentSingleton<TeleportManager>
    {
        private readonly List<SpawnPoint> _spawnPoints = new();

        /// <summary>
        /// Registers a spawn point.
        /// </summary>
        /// <param name="spawnPoint">The spawn point to register.</param>
        public void RegisterSpawnPoint(SpawnPoint spawnPoint)
        {
            if (spawnPoint != null && !_spawnPoints.Contains(spawnPoint))
            {
                _spawnPoints.Add(spawnPoint);
            }
        }

        /// <summary>
        /// Finds a spawn point by name.
        /// </summary>
        /// <param name="spawnName">The name of the spawn point to find.</param>
        /// <returns>The spawn point with the specified name, or null if not found.</returns>
        public SpawnPoint FindSpawnPoint(string spawnName)
        {
            return _spawnPoints.Find(spawnPoint => spawnPoint.SpawnName == spawnName);
        }

        /// <summary>
        /// Unregisters a spawn point.
        /// </summary>
        /// <param name="spawnPoint">The spawn point to unregister.</param>
        public void UnregisterSpawnPoint(SpawnPoint spawnPoint)
        {
            if (spawnPoint != null)
            {
                _spawnPoints.Remove(spawnPoint);
            }
        }
    }
}
