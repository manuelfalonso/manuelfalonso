using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// </summary>
    public class UnityPhysics2DCollisionMessagesHandler : MonoBehaviour
    {
        [Header("Physics 2D")]
        [Tooltip("Sent when an incoming collider makes contact with this object's collider (2D physics only).")]
        public UnityEvent OnCollisionEnter2DEvent = new ();
        [Tooltip("Sent when a collider on another object stops touching this object's collider (2D physics only).")]
        public UnityEvent OnCollisionExit2DEvent = new ();


        #region Unity Messages
        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnter2DEvent.Invoke();
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            OnCollisionExit2DEvent.Invoke();
        }
        #endregion
    }
}
