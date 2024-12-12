using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Spawners
{
    [CreateAssetMenu(menuName = "Wave Config")]
    public class WaveConfigSO : ScriptableObject
    {
        [SerializeField] GameObject prefab;

        [SerializeField] float timeBetweenSpawns = 0.5f;
        [SerializeField] float spawnRandomFactor = 0.3f;
        [SerializeField] int quantiy = 5;
        [SerializeField] float moveSpeed = 2f;

        public GameObject GetPrefab() => prefab;

        public float GetTimeBetweenSpawns() => timeBetweenSpawns;

        public float GetSpawnRandomFactor() => spawnRandomFactor;

        public int GetQuantity() => quantiy;

        public float GetMoveSpeed() => moveSpeed;
    }
}
