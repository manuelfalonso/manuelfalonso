using UnityEngine;

namespace SombraStudios.Shared.Patterns.Creational.FlyweightFactoryPool
{
    /// <summary>
    /// Represents an object that can be pooled using the Flyweight Factory Pool pattern.
    /// This MonoBehaviour contains a reference to the pool settings that define
    /// how the object behaves when managed by the pool.
    /// </summary>
    public class FlyweightPooledObject : MonoBehaviour
    {
        /// <summary>
        /// Pool settings that define the parameters and behaviors
        /// for managing this object in the pool.
        /// </summary>
        [Tooltip("Pool settings that define the parameters and behaviors for managing this object in the pool.")]
        public FlyweightPoolSettings Settings;
    }
}
