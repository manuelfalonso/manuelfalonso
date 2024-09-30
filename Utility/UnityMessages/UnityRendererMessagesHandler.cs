using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    /// </summary>
    public class UnityRendererMessagesHandler : MonoBehaviour
    {
        [Header("Renderer")]
        [Tooltip("OnBecameVisible is called when the renderer became visible by any camera.")]
        public UnityEvent OnBecameVisibleEvent = new ();
        [Tooltip("OnBecameInvisible is called when the renderer is no longer visible by any camera.")]
        public UnityEvent OnBecameInvisibleEvent = new ();


        #region Unity Messages
        private void OnBecameVisible()
        {
            OnBecameVisibleEvent.Invoke();
        }

        private void OnBecameInvisible()
        {
            OnBecameInvisibleEvent.Invoke();
        }
        #endregion
    }
}
