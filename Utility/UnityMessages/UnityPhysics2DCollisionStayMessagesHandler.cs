using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// Warning: this script can bring performance issues due to the number of calls.
    /// </summary>
    public class UnityPhysics2DCollisionStayMessagesHandler : MonoBehaviour
    {
        [Header("Physics 2D")]
        [Tooltip("Sent each frame where a collider on another object is touching this object's collider (2D " +
            "physics only).")]
        public UnityEvent OnCollisionStay2DEvent = new();


        #region Unity Messages
        private void OnCollisionStay2D(Collision2D collision)
        {
            OnCollisionStay2DEvent.Invoke();
        }
        #endregion
    }
}
