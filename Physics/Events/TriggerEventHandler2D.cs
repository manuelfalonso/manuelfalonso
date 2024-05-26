using UnityEngine;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Handles trigger events for a 2D Rigidbody.
    /// Inherits from TriggerEventHandlerBase and specifies Collider2D as the generic argument.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class TriggerEventHandler2D : TriggerEventHandlerBase<Collider2D>
    {
        #region Unity Messages
        private void OnTriggerEnter2D(Collider2D collider)
        {
            TriggerEnter(collider);
        }

        private void OnTriggerStay2D(Collider2D collider)
        {
            TriggerStay(collider);
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            TriggerExit(collider);
        }
        #endregion
    }
}
