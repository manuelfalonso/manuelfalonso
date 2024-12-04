using UnityEngine;

namespace SombraStudios.Shared.Gameplay.Behaviours.Destructible
{
    [CreateAssetMenu(fileName = "Destructible Data", menuName = "Sombra Studios/Behaviours/Destructible Data", order = 0)]
    public class DestructibleBehaviourSO : ScriptableObject
    {
        [Header("Explosion")]
        /// <summary>
        /// Multiplier for the explosion force applied to debris.
        /// </summary>
        [Tooltip("Multiplier for the explosion force applied to debris.")]
        public float ExplosionPowerMultiplier = 1;
        /// <summary>
        /// Lower bound for the random explosion force.
        /// </summary>
        [Tooltip("Lower bound for the random explosion force.")]
        public float ExplosionPowerLower = -1f;
        /// <summary>
        /// Upper bound for the random explosion force.
        /// </summary>
        [Tooltip("Upper bound for the random explosion force.")]
        public float ExplosionPowerUpper = 1f;

        [Header("Debris")]
        /// <summary>
        /// Determines whether to remove debris.
        /// </summary>
        [Tooltip("Determines whether to remove debris.")]
        public bool RemoveDebris = true;
        /// <summary>
        /// Minimum time before removing debris.
        /// </summary>
        [Tooltip("Minimum time before removing debris.")]
        [Min(0.1f)] public float RemoveDebrisTimerLower = 5f;
        /// <summary>
        /// Maximum time before removing debris.
        /// </summary>
        [Tooltip("Maximum time before removing debris.")]
        [Min(0.1f)] public float RemoveDebrisTimerUpper = 10f;


        /// <summary>
        /// Validates the debris removal timers to ensure they are within the correct range.
        /// </summary>
        private void OnValidate()
        {
            ValidateDebrisTimer();
        }


        /// <summary>
        /// Validates and adjusts the debris removal timers if necessary.
        /// </summary>
        private void ValidateDebrisTimer()
        {
            if (RemoveDebrisTimerLower > RemoveDebrisTimerUpper) { RemoveDebrisTimerLower = RemoveDebrisTimerUpper; }
            if (RemoveDebrisTimerUpper < RemoveDebrisTimerLower) { RemoveDebrisTimerUpper = RemoveDebrisTimerLower; }
        }
    }
}
