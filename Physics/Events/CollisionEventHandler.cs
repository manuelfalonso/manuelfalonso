using SombraStudios.Shared.Attributes;
using UnityEngine;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Handles collision events based on specified thresholds for 3D.
    /// The Rigidody is required to send the Collision Events. The Collider can be on child objects.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class CollisionEventHandler : CollisionEventHandlerBase<Collision>
    {
        [Header("3D Settings")]
        /// <summary>
        /// Type of threshold to use for triggering events. Select None to avoid checking thresholds.
        /// </summary>
        [Tooltip("Type of threshold to use for triggering events. Select None to avoid checking thresholds.")]
        [SerializeField] private CollisionThresholdType _thresholdType = CollisionThresholdType.Impulse;
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
            if (!IsCollisionEventType(PhysicInteractionEventType.Enter)) { return; }
            if (!CheckCollision(collision)) { return; }
            InteractionOnEnter?.Invoke(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            if (!IsCollisionEventType(PhysicInteractionEventType.Stay)) { return; }
            if (!CheckCollision(collision)) { return; }
            InteractionOnStay?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!IsCollisionEventType(PhysicInteractionEventType.Exit)) { return; }
            if (!CheckCollision(collision)) { return; }
            InteractionOnExit?.Invoke(collision);
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Checks if the collision should be processed based on various conditions.
        /// </summary>
        /// <param name="collision">The collision to check.</param>
        /// <returns>True if the collision should be processed, false otherwise.</returns>
        private bool CheckCollision(Collision collision)
        {
            if (!_isActive) { return false; }
            if (!IsLayerMaskValid(collision)) { return false; }
            if (!IsTagValid(collision)) { return false; }
            if (!CalculateCollision(collision)) { return false; }
            return true;
        }

        /// <summary>
        /// Checks if the layer of the collision is valid.
        /// </summary>
        /// <param name="collision">The collision to check.</param>
        /// <returns>True if the layer is valid, false otherwise.</returns>
        private bool IsLayerMaskValid(Collision collision)
        {
            return (_layerMask == (_layerMask | (1 << collision.gameObject.layer)));
        }

        /// <summary>
        /// Checks if the tag of the collision is valid.
        /// </summary>
        /// <param name="collision">The collision to check.</param>
        /// <returns>True if the tag is valid, false otherwise.</returns>
        private bool IsTagValid(Collision collision)
        {
            return string.IsNullOrEmpty(_requiredTag) || collision.gameObject.CompareTag(_requiredTag);
        }

        /// <summary>
        /// Calculates and processes collision data.
        /// </summary>
        /// <param name="other">The collision data.</param>
        /// /// <returns>True if the collision meets the thresholds, false otherwise.</returns>
        private bool CalculateCollision(Collision other)
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
