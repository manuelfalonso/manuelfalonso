using UnityEngine;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Base class for handling physics trigger events.
    /// </summary>
    /// <typeparam name="T">The type of the component that will trigger the events.</typeparam>
    public abstract class TriggerEventHandlerBase<T> : PhysicsEventHandlerBase<T> where T : Component
    {
        /// <summary>
        /// Forces a trigger event to be processed for the specified collider.
        /// </summary>
        /// <remarks>This method is intended to manually invoke a trigger event for the given collider, 
        /// typically in scenarios where automatic trigger detection is insufficient or bypassed.</remarks>
        /// <param name="collider">The collider for which the trigger event should be processed. Cannot be null.</param>
        public abstract void ForceTriggerEnterColliderCheck(T collider);
        /// <summary>
        /// Forces the system to re-evaluate whether the specified collider has exited a trigger zone.
        /// </summary>
        /// <remarks>This method is typically used to manually enforce trigger exit checks in scenarios
        /// where  automatic detection may not occur as expected. Ensure that the provided collider is valid  and
        /// associated with the relevant trigger zone.</remarks>
        /// <param name="collider">The collider to check for trigger exit conditions. Cannot be null.</param>
        public abstract void ForceTriggerExitColliderCheck(T collider);

        #region Protected Methods
        protected override bool IsStayInteractionValid(T component)
        {
            if (component == null) { return false; }
            if (!IsInteractionValid(component.gameObject)) { return false; }
            return true;
        }

        /// <summary>
        /// Handles the trigger enter event.
        /// </summary>
        /// <param name="collider">The collider that entered the trigger.</param>
        protected void TriggerEnter(T collider)
        {
            if (!IsInteractionValid(collider.gameObject)) { return; }
            HandleEnterInteractions(collider);
            if (!_eventType.HasFlag(PhysicInteractionEventType.Enter)) { return; }
            InteractionOnEnter?.Invoke(collider);
            if (_currentInteractions.Count == 1) // First interaction
            {
                InteractionOnFirstEnter?.Invoke(collider);
            }
        }

        /// <summary>
        /// Handles the trigger exit event.
        /// </summary>
        /// <param name="collider">The collider that exited the trigger.</param>
        protected void TriggerExit(T collider)
        {
            if (!IsInteractionValid(collider.gameObject)) { return; }
            HandleExitInteractions(collider);
            if (!_eventType.HasFlag(PhysicInteractionEventType.Exit)) { return; }
            InteractionOnExit?.Invoke(collider);
            if (_currentInteractions.Count == 0) // Last interaction exited
            {
                InteractionOnLastExit?.Invoke(collider);
            }
        }
        #endregion
    }
}
