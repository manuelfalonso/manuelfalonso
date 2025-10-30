using UnityEngine;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Handles trigger events for a 3D Rigidbody.
    /// Requires a Collider component to be attached to the same GameObject and set as a trigger.
    /// The trigger or the interaction also requires a Rigidbody component to be attached to the GameObject 
    /// or any parent.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TriggerEventHandler : TriggerEventHandlerBase<Collider>
    {
        #region Unity Messages
        private void OnTriggerEnter(Collider collider)
        {
            TriggerEnter(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            TriggerExit(collider);
        }
        #endregion

        #region Public Methods
        public override void ForceTriggerEnterColliderCheck(Collider collider)
        {
            if (collider == null) { return; }
            var handlerColliders = GetComponents<Collider>();

            for (int i = 0; i < handlerColliders.Length; i++)
            {
                var col = handlerColliders[i];

                if (col == null) { continue; }
                if (!col.isTrigger) { continue; }

                if (col.bounds.Intersects(collider.bounds))
                {
                    TriggerEnter(collider);
                    return;
                }
            }
        }

        public override void ForceTriggerExitColliderCheck(Collider collider)
        {
            if (collider == null) { return; }
            var handlerColliders = GetComponents<Collider>();
            bool intersects = false;

            for (int i = 0; i < handlerColliders.Length; i++)
            {
                var col = handlerColliders[i];

                if (col == null) { continue; }
                if (!col.isTrigger) { continue; }

                if (col.bounds.Intersects(collider.bounds))
                {
                    TriggerExit(collider);
                    intersects = true;
                    break;
                }
            }

            if (!intersects)
                TriggerExit(collider);
        }
        #endregion
    }
}
