using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    /// </summary>
    public class UnityPausingAndFocusMessagesHandler : MonoBehaviour
    {
        [Header("Pausing and Focus Messages")]
        [Tooltip("Sent to all GameObjects when the player gets focus.")]
        public UnityEvent OnApplicationGetFocusEvent = new ();
        [Tooltip("Sent to all GameObjects when the player loses focus.")]
        public UnityEvent OnApplicationLosesFocusEvent = new();
        [Tooltip("Sent to all GameObjects when the application pauses.")]
        public UnityEvent OnApplicationPauseEvent = new ();


        #region Unity Messages
        private void OnApplicationFocus(bool focus)
        {
            if (focus)
                OnApplicationGetFocusEvent.Invoke();
            else
                OnApplicationLosesFocusEvent.Invoke();
        }

        private void OnApplicationPause(bool pause)
        {
            OnApplicationPauseEvent.Invoke();
        }
        #endregion
    }
}
