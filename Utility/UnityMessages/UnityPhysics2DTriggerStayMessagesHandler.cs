using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// Warning: this script can bring performance issues due to the number of calls.
    /// </summary>
    public class UnityPhysics2DTriggerStayMessagesHandler : MonoBehaviour
    {
        [Header("Physics 2D")]
        [Tooltip("Sent each frame where another object is within a trigger collider attached to this object " +
            "(2D physics only).")]
        public UnityEvent OnTriggerStay2DEvent = new();


        #region Unity Messages
        private void OnTriggerStay2D(Collider2D other)
        {
            OnTriggerStay2DEvent.Invoke();
        }
        #endregion
    }
}
