using System.Collections.Generic;

namespace SombraStudios.Systems.Teleport
{
    // This Manager must persist between scenes
    public class TeleportManager : Patterns.Creational.Singleton.Singleton<TeleportManager>
    {
        private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();


        public void RegisterSpawnPoint(SpawnPoint spawnPoint)
        {
            if (spawnPoint == null) { return; }

            if (!_spawnPoints.Contains(spawnPoint))
            {
                _spawnPoints.Add(spawnPoint);
            }
        }

        public SpawnPoint FindSpawnPoint(string spawnName)
        {
            return _spawnPoints.Find((spawnPoint) => string.Equals(spawnPoint.SpawnName, spawnName));
        }

        public void UnregisterSpawnPoint(SpawnPoint spawnPoint)
        {
            if (spawnPoint == null) { return; }

            if (_spawnPoints.Contains(spawnPoint))
            {
                _spawnPoints.Remove(spawnPoint);
            }
        }
    }
}
