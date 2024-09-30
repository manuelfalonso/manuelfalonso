using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    /// </summary>
    public class UnityTransformMessagesHandler : MonoBehaviour
    {
        [Header("Transform Messages")]
        [Tooltip("This function is called when a direct or indirect parent of the transform of the GameObject " +
            "has changed.")]
        public UnityEvent OnTransformParentChangedEvent = new();
        [Tooltip("This function is called when the list of children of the transform of the GameObject has changed.")]
        public UnityEvent OnTransformChildrenChangedEvent = new ();


        #region Unity Messages
        private void OnTransformParentChanged()
        {
            OnTransformParentChangedEvent.Invoke();
        }

        private void OnTransformChildrenChanged()
        {
            OnTransformChildrenChangedEvent.Invoke();
        }
        #endregion
    }
}
