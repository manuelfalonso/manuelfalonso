using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.UnityMessages
{
    /// <summary>
    /// Documentation:
    /// https://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    /// </summary>
    public class UnityEditorMessagesHandler : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("Editor Messages")]
        [Tooltip("Editor-only function that Unity calls when the script is loaded or a value changes in the" +
            " Inspector.")]
        public UnityEvent OnValidateEvent = new ();
        [Tooltip("Reset to default values.")]
        public UnityEvent ResetEvent = new ();


        #region Unity Messages
        private void OnValidate()
        {
            OnValidateEvent.Invoke();
        }

        private void Reset()
        {
            ResetEvent.Invoke();
        }
        #endregion
#endif
    }
}
