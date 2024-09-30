using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    /// </summary>
    public class UnityControllerMessagesHandler : MonoBehaviour
    {
        [Header("Character Controller")]
        [Tooltip("OnControllerColliderHit is called when the controller hits a collider while performing a Move.")]
        public UnityEvent OnControllerColliderHitEvent = new ();


        #region Unity Messages
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            OnControllerColliderHitEvent.Invoke();
        }
        #endregion
    }
}
