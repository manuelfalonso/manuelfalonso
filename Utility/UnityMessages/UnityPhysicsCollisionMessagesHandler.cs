using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// </summary>
    public class UnityPhysicsCollisionMessagesHandler : MonoBehaviour
    {
        [Header("Physics")]
        [Tooltip("OnCollisionEnter is called when this collider/rigidbody has begun touching another rigidbody" +
            "/collider.")]
        public UnityEvent OnCollisionEnterEvent = new ();
        [Tooltip("OnCollisionExit is called when this collider/rigidbody has stopped touching another rigidbody/" +
            "collider.")]
        public UnityEvent OnCollisionExitEvent = new ();


        #region Unity Messages
        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnterEvent.Invoke();
        }

        private void OnCollisionExit(Collision collision)
        {
            OnCollisionExitEvent.Invoke();
        }
        #endregion
    }
}
