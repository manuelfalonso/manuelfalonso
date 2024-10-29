using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// Warning: this script can bring performance issues due to the number of calls.
    /// </summary>
    public class UnityPhysicsTriggerStayMessagesHandler : MonoBehaviour
    {
        [Header("Physics")]
        [Tooltip("OnTriggerStay is called once per physics update for every Collider other that is touching " +
             "the trigger.")]
        public UnityEvent OnTriggerStayEvent = new();


        #region Unity Messages
        private void OnTriggerStay(Collider other)
        {
            OnTriggerStayEvent.Invoke();
        }
        #endregion
    }
}
