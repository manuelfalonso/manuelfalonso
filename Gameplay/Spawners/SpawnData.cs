using UnityEngine;
using SombraStudios.Shared.Structs;

namespace SombraStudios.Shared.Gameplay.Spawners
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "Sombra Studios/Gameplay/Spawners/Spawn Data")]
    public class SpawnData : ScriptableObject
    {
        [Header("Spawn Settings")]
        public GameObject[] ObjectToSpawn;
        public RangedFloat TimeBetweenSpawns;
        public bool AllowConsecutiveRepeatSpawns = false;
        public int MaxSpawns = 3;
        public bool SpawnAtStart = true;
    }
}
