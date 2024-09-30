using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// </summary>
    public class UnityPhysics2DTriggerMessagesHandler : MonoBehaviour
    {
        [Header("Physics 2D")]
        [Tooltip("Sent when another object enters a trigger collider attached to this object (2D physics only).")]
        public UnityEvent OnTriggerEnter2DEvent = new ();
        [Tooltip("Sent when another object leaves a trigger collider attached to this object (2D physics only).")]
        public UnityEvent OnTriggerExit2DEvent = new ();


        #region Unity Messages
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEnter2DEvent.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnTriggerExit2DEvent.Invoke();
        }
        #endregion
    }
}
