using UnityEngine;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Handles collision events based on specified thresholds for 2D.
    /// The Rigidody is required to send the Collision Events. The Collider can be on child objects.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class CollisionEventHandler2D : CollisionEventHandlerBase<Collision2D>
    {
        #region Unity Messages
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!IsCollisionEventType(PhysicInteractionEventType.Enter)) { return; }
            if (!CheckCollision(collision)) { return; }
            InteractionOnEnter?.Invoke(collision);
        }

        private void OnCollisionStay2D(Collision2D collision)
        {
            if (!IsCollisionEventType(PhysicInteractionEventType.Stay)) { return; }
            if (!CheckCollision(collision)) { return; }
            InteractionOnStay?.Invoke(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
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
        private bool CheckCollision(Collision2D collision)
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
        private bool IsLayerMaskValid(Collision2D collision)
        {
            return (_layerMask == (_layerMask | (1 << collision.gameObject.layer)));
        }

        /// <summary>
        /// Checks if the tag of the collision is valid.
        /// </summary>
        /// <param name="collision">The collision to check.</param>
        /// <returns>True if the tag is valid, false otherwise.</returns>
        private bool IsTagValid(Collision2D collision)
        {
            return string.IsNullOrEmpty(_requiredTag) || collision.gameObject.CompareTag(_requiredTag);
        }

        /// <summary>
        /// Calculates and processes collision data.
        /// </summary>
        /// <param name="collision">The collision data.</param>
        /// <returns>True if the collision meets the thresholds, false otherwise.</returns>
        private bool CalculateCollision(Collision2D collision)
        {
            _lastVelocity = collision.relativeVelocity.magnitude;

            _maxVelocity = Mathf.Max(_maxVelocity, _lastVelocity);

            var velocityMet = _lastVelocity > _velocityThreshold;

            if (velocityMet)
            {
                _lastInteraction = collision.transform;
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
