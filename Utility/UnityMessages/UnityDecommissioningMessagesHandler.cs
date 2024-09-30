using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// </summary>
    public class UnityDecommissioningMessagesHandler : MonoBehaviour
    {
        [Header("Decommissioning")]
        [Tooltip("Sent to all GameObjects before the application quits.")]
        public UnityEvent OnApplicationQuitEvent = new ();
        [Tooltip("This function is called when the behaviour becomes disabled.")]
        public UnityEvent OnDisableEvent = new ();
        [Tooltip("Destroying the attached Behaviour will result in the game or Scene receiving OnDestroy.")]
        public UnityEvent OnDestroyEvent = new ();


        #region Unity Messages
        private void OnApplicationQuit()
        {
            OnApplicationQuitEvent.Invoke();
        }

        private void OnDisable()
        {
            OnDisableEvent.Invoke();
        }

        private void OnDestroy()
        {
            OnDestroyEvent.Invoke();
        }
        #endregion
    }
}
