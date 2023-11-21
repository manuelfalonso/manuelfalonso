using System;
using UnityEngine;
using UnityEngine.Events;
using SombraStudios.Shared.Attributes;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// Handles collision events based on specified thresholds for 3D.
    /// The Rigidody is required to send the Collision Events. The Collider can be on child objects.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CollisionEventHandler : MonoBehaviour
    {
        [Header("Settings")]
        /// <summary>
        /// Type of collision event to handle (Enter, Stay, Exit).
        /// </summary>
        [Tooltip("Type of collision event to handle (Enter, Stay, Exit).")]
        [SerializeField] private CollisionEventType _eventType = CollisionEventType.Enter;
        /// <summary>
        /// Type of threshold to use for triggering events (Impulse, Velocity, ImpulseOrVelocity).
        /// </summary>
        [Tooltip("Type of threshold to use for triggering events (Impulse, Velocity, ImpulseOrVelocity).")]
        [SerializeField] private CollisionThresholdType _thresholdType = CollisionThresholdType.Impulse;
        /// <summary>
        /// Force threshold to breach, to fire the ThresholdMet event
        /// </summary>
        [Tooltip("Force threshold to breach to fire the ThresholdMet event")]
        [SerializeField] private float _forceThreshold = 0f;
        /// <summary>
        /// Collision velocity threshold to breach, to fire the ThresholdMetEvent
        /// </summary>
        [Tooltip("Collision velocity threshold to breach to fire the ThresholdMetEvent")]
        [SerializeField] private float _velocityThreshold = 0f;

        /// <summary>
        /// Event triggered when collision thresholds are met.
        /// </summary>
        [Tooltip("Event triggered when collision thresholds are met.")]
        public UnityEvent CollisionThresholdMet = new UnityEvent();

        [Header("Debug")]
        /// <summary>
        /// Debug - Last recorded impulse during collision.
        /// </summary>
        [Tooltip("Debug - Last recorded impulse during collision.")]
        [SerializeField, ReadOnly] private float _lastImpulse;
        /// <summary>
        /// Debug - Last recorded relative velocity during collision.
        /// </summary>
        [Tooltip("Debug - Last recorded relative velocity during collision.")]
        [SerializeField, ReadOnly] private float _lastVelocity;

        /// <summary>
        /// Debug - Maximum recorded impulse during collisions.
        /// </summary>
        [Tooltip("Debug - Maximum recorded impulse during collisions.")]
        [SerializeField, ReadOnly] private float _maxImpulse;
        /// <summary>
        /// Debug - Maximum recorded relative velocity during collisions.
        /// </summary>
        [Tooltip("Debug - Maximum recorded relative velocity during collisions.")]
        [SerializeField, ReadOnly] private float _maxVelocity;


        private void OnCollisionEnter(Collision collision)
        {
            if (!IsCollisionEventType(CollisionEventType.Enter)) { return; }
            CalculateCollision(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!IsCollisionEventType(CollisionEventType.Stay)) { return; }
            CalculateCollision(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!IsCollisionEventType(CollisionEventType.Exit)) { return; }
            CalculateCollision(collision);
        }


        /// <summary>
        /// Calculates and processes collision data.
        /// </summary>
        /// <param name="other">The collision data.</param>
        private void CalculateCollision(Collision other)
        {
            _lastImpulse = other.impulse.magnitude;
            _lastVelocity = other.relativeVelocity.magnitude;

            _maxImpulse = Mathf.Max(_maxImpulse, _lastImpulse);
            _maxVelocity = Mathf.Max(_maxVelocity, _lastVelocity);

            var forceMet = _lastImpulse > _forceThreshold;
            var velocityMet = _lastVelocity > _velocityThreshold;

            if (_thresholdType == CollisionThresholdType.Impulse && forceMet ||
                _thresholdType == CollisionThresholdType.Velocity && velocityMet ||
                _thresholdType == CollisionThresholdType.ImpulseOrVelocity && (forceMet || velocityMet))
            {
                CollisionThresholdMet?.Invoke();
            }
        }

        /// <summary>
        /// Checks if the specified collision event type should be processed.
        /// </summary>
        /// <param name="collisionEventType">The collision event type to check.</param>
        /// <returns>True if the event type should be processed, false otherwise.</returns>
        private bool IsCollisionEventType(CollisionEventType collisionEventType)
        {
            return _eventType == CollisionEventType.None ? false : (_eventType & collisionEventType) != 0;
        }
    }


    /// <summary>
    /// Types of collision events.
    /// </summary>
    [Serializable]
    [Flags]
    public enum CollisionEventType
    {
        None = 0,
        Enter = 1,
        Stay = 2, 
        Exit = 4
    }

    /// <summary>
    /// Types of collision threshold to trigger events.
    /// </summary>
    [Serializable]
    public enum CollisionThresholdType
    {
        Impulse, Velocity, ImpulseOrVelocity
    }
}
