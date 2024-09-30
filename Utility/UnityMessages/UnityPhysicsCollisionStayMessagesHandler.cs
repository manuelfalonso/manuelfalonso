using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// Warning: this script can bring performance issues due to the number of calls.
    /// </summary>
    public class UnityPhysicsCollisionStayMessagesHandler : MonoBehaviour
    {
        [Header("Physics")]
        [Tooltip("OnCollisionStay is called once per frame for every Collider or Rigidbody that touches another " +
             "Collider or Rigidbody.")]
        public UnityEvent OnCollisionStayEvent = new();


        #region Unity Messages
        private void OnCollisionStay(Collision collision)
        {
            OnCollisionStayEvent.Invoke();
        }
        #endregion
    }
}
