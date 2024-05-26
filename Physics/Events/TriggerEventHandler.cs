using UnityEngine;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Handles trigger events for a 3D Rigidbody.
    /// Inherits from TriggerEventHandlerBase and specifies Collider as the generic argument.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class TriggerEventHandler : TriggerEventHandlerBase<Collider>
    {
        #region Unity Messages
        private void OnTriggerEnter(Collider collider)
        {
            TriggerEnter(collider);
        }

        private void OnTriggerStay(Collider collider)
        {
            TriggerStay(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            TriggerExit(collider);
        }
        #endregion
    }
}
