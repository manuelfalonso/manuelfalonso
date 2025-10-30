using SombraStudios.Shared.Attributes;
using UnityEngine;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Handles 3D collision events when at least one involved object has a non-kinematic Rigidbody
    /// and both have Colliders with IsTrigger disabled.
    /// The required Rigidbody may be on the GameObject or any of its parents.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class CollisionEventHandler : CollisionEventHandlerBase<Collision>
    {
        [Header("3D Settings")]
        /// <summary>
        /// Type of threshold to use for triggering events. Select None to avoid checking thresholds.
        /// </summary>
        [Tooltip("Type of threshold to use for triggering events. Select None to avoid checking thresholds.")]
        [SerializeField] private CollisionThresholdType _thresholdType = CollisionThresholdType.None;
        /// <summary>
        /// Impulse force threshold to breach, to fire the ThresholdMet event
        /// </summary>
        [Tooltip("Impulse force threshold to breach to fire the ThresholdMet event")]
        [SerializeField] private float _impulseThreshold = 0f;
        
        [Header("3D Debug")]        
        /// <summary>
        /// Debug - Last recorded impulse during collision.
        /// </summary>
        [Tooltip("Debug - Last recorded impulse during collision.")]
        [SerializeField, ReadOnly] private float _lastImpulse;
        
        /// <summary>
        /// Debug - Maximum recorded impulse during collisions.
        /// </summary>
        [Tooltip("Debug - Maximum recorded impulse during collisions.")]
        [SerializeField, ReadOnly] private float _maxImpulse;
        

        #region Unity Messages
        private void OnCollisionEnter(Collision collision)
        {
            CollisionEnter(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            CollisionExit(collision);
        }
        #endregion

        #region Protected Methods
        protected override bool IsStayInteractionValid(Collision component)
        {
            if (component == null) { return false; }
            if (!IsInteractionValid(component.gameObject)) { return false; }
            return true;
        }

        protected bool CollisionEnter(Collision collision)
        {
            if (!IsInteractionValid(collision.gameObject)) { return false; }
            if (!CalculateCollision(collision)) { return false; }

            HandleEnterInteractions(collision);
            if (!_eventType.HasFlag(PhysicInteractionEventType.Enter)) { return false; }

            InteractionOnEnter?.Invoke(collision);
            return true;
        }

        protected bool CollisionExit(Collision collision)
        {
            if (!IsInteractionValid(collision.gameObject)) { return false; }
            if (!CalculateCollision(collision)) { return false; }

            HandleExitInteractions(collision);
            if (!_eventType.HasFlag(PhysicInteractionEventType.Exit)) { return false; }

            InteractionOnExit?.Invoke(collision);
            return true;
        }

        protected override bool CalculateCollision(Collision other)
        {
            // No thresholds to check
            if (_thresholdType == CollisionThresholdType.None) { return true; }

            _lastImpulse = other.impulse.magnitude;
            _lastVelocity = other.relativeVelocity.magnitude;

            _maxImpulse = Mathf.Max(_maxImpulse, _lastImpulse);
            _maxVelocity = Mathf.Max(_maxVelocity, _lastVelocity);

            var forceMet = _lastImpulse > _impulseThreshold;
            var velocityMet = _lastVelocity > _velocityThreshold;

            if (IsImpulseThreshold() && forceMet ||
                IsVelocityThreshold() && velocityMet ||
                IsImpulseAndVelocityThreshold() && (forceMet || velocityMet))
            {
                _lastInteraction = other.transform;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Checks if the impulse threshold is set.
        /// </summary>
        /// <returns>True if the impulse threshold is set, false otherwise.</returns>
        private bool IsImpulseThreshold()
        {
            return (_thresholdType & CollisionThresholdType.Impulse) != 0;
        }

        /// <summary>
        /// Checks if the velocity threshold is set.
        /// </summary>
        /// <returns>True if the velocity threshold is set, false otherwise.</returns>
        private bool IsVelocityThreshold()
        {
            return (_thresholdType & CollisionThresholdType.Velocity) != 0;
        }

        /// <summary>
        /// Checks if both the impulse and velocity thresholds are set.
        /// </summary>
        /// <returns>True if both thresholds are set, false otherwise.</returns>
        private bool IsImpulseAndVelocityThreshold()
        {
            return (_thresholdType & CollisionThresholdType.Impulse & CollisionThresholdType.Velocity) != 0;
        }
        #endregion
    }
}
