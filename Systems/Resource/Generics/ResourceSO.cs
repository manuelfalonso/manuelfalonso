using UnityEngine;

namespace SombraStudios.Shared.Systems.Resource
{
    /// <summary>
    /// ScriptableObject class to hold resource data.
    /// </summary>
    /// <typeparam name="T">The type of the resource amount.</typeparam>
    public class ResourceSO<T> : ScriptableObject where T : struct
    {
        /// <summary>
        /// The name of the resource.
        /// </summary>
        public string Name;

        /// <summary>
        /// The description of the resource.
        /// </summary>
        [Multiline]
        public string Description;

        /// <summary>
        /// The minimum amount of the resource.
        /// </summary>
        public T MinAmount;

        /// <summary>
        /// The maximum amount of the resource.
        /// </summary>
        public T MaxAmount;

        /// <summary>
        /// The initial amount of the resource.
        /// </summary>
        public T InitialAmount;
    }
}
