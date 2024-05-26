using UnityEngine;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Base class for handling physics trigger events.
    /// </summary>
    /// <typeparam name="T">The type of the component that will trigger the events.</typeparam>
    public class TriggerEventHandlerBase<T> : PhysicsEventHandlerBase<T> where T : Component
    {
        #region Protected Methods
        /// <summary>
        /// Handles the trigger enter event.
        /// </summary>
        /// <param name="collider">The collider that entered the trigger.</param>
        protected void TriggerEnter(T collider)
        {
            if (!IsCollisionEventType(PhysicInteractionEventType.Enter)) { return; }
            if (!CheckTrigger(collider)) { return; }
            InteractionOnEnter?.Invoke(collider);
        }

        /// <summary>
        /// Handles the trigger stay event.
        /// </summary>
        /// <param name="collider">The collider that is staying in the trigger.</param>
        protected void TriggerStay(T collider)
        {
            if (!IsCollisionEventType(PhysicInteractionEventType.Stay)) { return; }
            if (!CheckTrigger(collider)) { return; }
            InteractionOnStay?.Invoke(collider);
        }

        /// <summary>
        /// Handles the trigger exit event.
        /// </summary>
        /// <param name="collider">The collider that exited the trigger.</param>
        protected void TriggerExit(T collider)
        {
            if (!IsCollisionEventType(PhysicInteractionEventType.Exit)) { return; }
            if (!CheckTrigger(collider)) { return; }
            InteractionOnExit?.Invoke(collider);
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Checks if the trigger should be processed based on various conditions.
        /// </summary>
        /// <param name="collider">The collider to check.</param>
        /// <returns>True if the trigger should be processed, false otherwise.</returns>
        private bool CheckTrigger(T collider)
        {
            if (!_isActive) { return false; }
            if (!IsLayerMaskValid(collider)) { return false; }
            if (!IsTagValid(collider)) { return false; }
            _lastInteraction = collider.transform;
            return true;
        }

        /// <summary>
        /// Checks if the layer of the collider is valid.
        /// </summary>
        /// <param name="collider">The collider to check.</param>
        /// <returns>True if the layer is valid, false otherwise.</returns>
        private bool IsLayerMaskValid(T collider)
        {
            return (_layerMask == (_layerMask | (1 << collider.gameObject.layer)));
        }

        /// <summary>
        /// Checks if the tag of the collider is valid.
        /// </summary>
        /// <param name="collider">The collider to check.</param>
        /// <returns>True if the tag is valid, false otherwise.</returns>
        private bool IsTagValid(T collider)
        {
            return string.IsNullOrEmpty(_requiredTag) || collider.gameObject.CompareTag(_requiredTag);
        }
        #endregion
    }
}
