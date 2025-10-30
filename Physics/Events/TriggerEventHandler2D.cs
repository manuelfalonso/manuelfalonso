using UnityEngine;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Handles trigger events for a 3D Rigidbody.
    /// Requires a Collider component to be attached to the same GameObject and set as a trigger.
    /// The trigger or the interaction also requires a Rigidbody component to be attached to the GameObject 
    /// or any parent.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class TriggerEventHandler2D : TriggerEventHandlerBase<Collider2D>
    {
        #region Unity Messages
        private void OnTriggerEnter2D(Collider2D collider)
        {
            TriggerEnter(collider);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            TriggerExit(collider);
        }
        #endregion

        #region Public Methods
        public override void ForceTriggerEnterColliderCheck(Collider2D collider)
        {
            if (collider == null) { return; }
            var handlerColliders = GetComponents<Collider2D>();

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

        public override void ForceTriggerExitColliderCheck(Collider2D collider)
        {
            if (collider == null) { return; }
            var handlerColliders = GetComponents<Collider2D>();
            bool intersects = false;

            for (int i = 0; i < handlerColliders.Length; i++)
            {
                var col = handlerColliders[i];

                if (col == null) { continue; }
                if (!col.isTrigger) { continue; }

                if (col.bounds.Intersects(collider.bounds))
                {
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
