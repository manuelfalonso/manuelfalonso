using UnityEngine;

namespace SombraStudios.Shared.Systems.Teleport
{
    /// <summary>
    /// Transform gameObject used as Teleporter destination
    /// </summary>
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private string _spawnName;
        public string SpawnName
        {
            get
            {
                return _spawnName;
            }
            set
            {
                _spawnName = value;
            }
        }


        private void Start()
        {
            TeleportManager.Instance.RegisterSpawnPoint(this);
        }

        private void OnDestroy()
        {
            TeleportManager.Instance.UnregisterSpawnPoint(this);
        }
    }
}
