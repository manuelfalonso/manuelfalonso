using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// </summary>
    public class UnityInitializationMessagesHandler : MonoBehaviour
    {
        [Header("Initialization")]
        [Tooltip("Awake is called when an enabled script instance is being loaded.")]
        public UnityEvent AwakeEvent = new ();
        [Tooltip("This function is called when the object becomes enabled and active.")]
        public UnityEvent OnEnableEvent = new ();
        [Tooltip("Start is called on the frame when a script is enabled just before any of the Update methods " +
            "are called the first time.")]
        public UnityEvent StartEvent = new ();


        #region Unity Messages
        private void Awake()
        {
            AwakeEvent.Invoke();
        }

        private void OnEnable()
        {
            OnEnableEvent.Invoke();
        }

        private void Start()
        {
            StartEvent.Invoke();
        }
        #endregion
    }
}
