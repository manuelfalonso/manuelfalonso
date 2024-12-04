using UnityEngine;

namespace SombraStudios.Shared.Systems.Resource
{
    /// <summary>
    /// ScriptableObject class to hold resource system data with cooldown durations.
    /// </summary>
    /// <typeparam name="T">The type of the resource amount.</typeparam>
    public class ResourceSystemDataSO<T> : ResourceSO<T> where T : struct
    {
        /// <summary>
        /// The cooldown duration after increasing the resource.
        /// </summary>
        [Min(0f)]
        public float IncreaseCooldownDuration;

        /// <summary>
        /// The cooldown duration after decreasing the resource.
        /// </summary>
        [Min(0f)]
        public float DecreaseCooldownDuration;
    }
}
