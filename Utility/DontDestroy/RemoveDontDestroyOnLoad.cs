using UnityEngine;
using UnityEngine.SceneManagement;

namespace SombraStudios.Shared.Utility.DontDestroy
{
    /// <summary>  
    /// Utility script to remove a GameObject from DontDestroyOnLoad and move it back to the active scene.  
    /// Can be configured to run at specific points in the object's lifecycle.  
    /// </summary>  
    public class RemoveDontDestroyOnLoad : MonoBehaviour
    {
        /// <summary>  
        /// Specifies when the removal from DontDestroyOnLoad should occur.  
        /// </summary>  
        [SerializeField] private RunType _whenToRun = RunType.Custom;

        private void Awake()
        {
            if (_whenToRun == RunType.Awake)
            {
                Remove();
            }
        }

        private void OnEnable()
        {
            if (_whenToRun == RunType.OnEnable)
            {
                Remove();
            }
        }

        private void Start()
        {
            if (_whenToRun == RunType.Start)
            {
                Remove();
            }
        }

        private void OnDisable()
        {
            if (_whenToRun == RunType.OnDisable)
            {
                Remove();
            }
        }

        /// <summary>  
        /// Removes the GameObject from DontDestroyOnLoad and moves it to the active scene.  
        /// Logs an error if the GameObject is not a root object.  
        /// </summary>  
        public void Remove()
        {
            // Ensure the GameObject is a root object before moving it to the active scene  
            if (transform.parent == null)
            {
                SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            }
            else
            {
                Debug.LogError("RemoveDontDestroyOnLoad: The GameObject is not a root object." +
                    " Cannot remove from DontDestroyOnLoad.", this);
            }
        }

        /// <summary>  
        /// Enum to specify when the removal from DontDestroyOnLoad should occur.  
        /// </summary>  
        public enum RunType
        {
            /// <summary>  
            /// Remove on Awake.  
            /// </summary>  
            Awake,

            /// <summary>  
            /// Remove on OnEnable.  
            /// </summary>  
            OnEnable,

            /// <summary>  
            /// Remove on Start.  
            /// </summary>  
            Start,

            /// <summary>  
            /// Remove on OnDisable.  
            /// </summary>  
            OnDisable,

            /// <summary>  
            /// Custom removal, triggered manually.  
            /// </summary>  
            Custom
        }
    }
}
