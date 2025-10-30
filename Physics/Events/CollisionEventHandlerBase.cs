using SombraStudios.Shared.Attributes;
using UnityEngine;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Base class for handling physics collision events.
    /// </summary>
    public abstract class CollisionEventHandlerBase<T> : PhysicsEventHandlerBase<T>
    {
        [Header("Collision Settings")]
        /// <summary>
        /// Collision velocity threshold to breach, to fire the interaction event
        /// </summary>
        [Tooltip("Collision velocity threshold to breach to fire the interaction event")]
        [SerializeField] protected float _velocityThreshold = 0f;

        [Header("Collision Debug")]        
        /// <summary>
        /// Debug - Last recorded relative velocity during collision.
        /// </summary>
        [Tooltip("Debug - Last recorded relative velocity during collision.")]
        [SerializeField, ReadOnly] protected float _lastVelocity;                
        /// <summary>
        /// Debug - Maximum recorded relative velocity during collisions.
        /// </summary>
        [Tooltip("Debug - Maximum recorded relative velocity during collisions.")]
        [SerializeField, ReadOnly] protected float _maxVelocity;

        /// <summary>
        /// Calculates and processes collision data.
        /// </summary>
        /// <param name="other">The collision data.</param>
        /// /// <returns>True if the collision meets the thresholds, false otherwise.</returns>
        protected abstract bool CalculateCollision(T collision);
    }
}
