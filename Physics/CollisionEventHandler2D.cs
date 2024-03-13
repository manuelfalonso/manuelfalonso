using SombraStudios.Shared.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Physics
{
    /// <summary>
    /// Handles collision events based on specified thresholds for 2D.
    /// The Rigidody is required to send the Collision Events. The Collider can be on child objects.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class CollisionEventHandler2D : MonoBehaviour
    {
        [Header("Settings")]
        /// <summary>
        /// Type of collision event to handle (Enter, Stay, Exit).
        /// </summary>
        [Tooltip("Type of collision event to handle (Enter, Stay, Exit).")]
        [SerializeField] private CollisionEventType _eventType = CollisionEventType.Enter;
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
        /// Wheter the component is calculating collisions or not.
        /// </summary>        
        [Tooltip("Wheter the component is calculating collisions or not.")]
        [SerializeField] private bool _isActive = true;
        /// <summary>
        /// Debug - Last recorded relative velocity during collision.
        /// </summary>
        [Tooltip("Debug - Last recorded relative velocity during collision.")]
        [SerializeField, ReadOnly] private float _lastVelocity;

        /// <summary>
        /// Debug - Maximum recorded relative velocity during collisions.
        /// </summary>
        [Tooltip("Debug - Maximum recorded relative velocity during collisions.")]
        [SerializeField, ReadOnly] private float _maxVelocity;

        public bool IsActive { get => _isActive; set => _isActive = value; }


        #region Unity Messages
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_isActive) { return; }
            if (!IsCollisionEventType(CollisionEventType.Enter)) { return; }
            CalculateCollision(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!_isActive) { return; }
            if (!IsCollisionEventType(CollisionEventType.Stay)) { return; }
            CalculateCollision(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (!_isActive) { return; }
            if (!IsCollisionEventType(CollisionEventType.Exit)) { return; }
            CalculateCollision(collision);
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Calculates and processes collision data.
        /// </summary>
        /// <param name="other">The collision data.</param>
        private void CalculateCollision(Collision2D other)
        {
            _lastVelocity = other.relativeVelocity.magnitude;

            _maxVelocity = Mathf.Max(_maxVelocity, _lastVelocity);

            var velocityMet = _lastVelocity > _velocityThreshold;

            if (velocityMet)
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
            return _eventType != CollisionEventType.None && (_eventType & collisionEventType) != 0;
        }
        #endregion
    }
}
