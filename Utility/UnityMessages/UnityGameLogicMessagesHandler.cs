using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// </summary>
    public class UnityGameLogicMessagesHandler : MonoBehaviour
    {
        [Header("Game Logic")]
        [Tooltip("Update is called every frame, if the MonoBehaviour is enabled.")]
        public UnityEvent UpdateEvent = new ();
        [Tooltip("Interval in frames to manage Update calls.")]
        [SerializeField] private int _interval = 1;


        #region Unity Messages
        private void Update()
        {
            if (Time.frameCount % _interval == 0)
            {
                UpdateEvent.Invoke();
            }
        }
        #endregion
    }
}
