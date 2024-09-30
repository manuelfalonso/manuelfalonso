using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// </summary>
    public class UnityPhysicsTriggerMessagesHandler : MonoBehaviour
    {
        [Header("Physics")]
        [Tooltip("When a GameObject collides with another GameObject, Unity calls OnTriggerEnter.")]
        public UnityEvent OnTriggerEnterEvent = new ();
        // TODO: Uncomment if needed. It generates performance issues.
        //[Tooltip("OnTriggerStay is called once per physics update for every Collider other that is touching " +
        //    "the trigger.")]
        //public UnityEvent OnTriggerStayEvent = new ();
        [Tooltip("OnTriggerExit is called when the Collider other has stopped touching the trigger.")]
        public UnityEvent OnTriggerExitEvent = new ();


        #region Unity Messages
        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterEvent.Invoke();
        }

        //private void OnTriggerStay(Collider other)
        //{
        //    OnTriggerStayEvent.Invoke();
        //}

        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitEvent.Invoke();
        }
        #endregion
    }
}
