using UnityEngine;

namespace SombraStudios.Shared.Examples
{
    /// <summary>
    /// Logs on every Unity Message event method
    /// Documentation:
    /// https://docs.unity3d.com/Manual/ExecutionOrder.html
    /// Note: this script does not consider Internal animation update
    /// The message event methods are in execution order.
    /// </summary>
    public class UnityScriptLifecycle : MonoBehaviour
    {
        [Header("Initialization")]
        [SerializeField] private bool _logAwake;
        [SerializeField] private bool _logOnEnable;

        [Header("Editor")]
        [Tooltip("Reset is called when the script is attached and not in playmode.")]
        [SerializeField] private bool _logReset;

        [Header("Initialization")]
        [Tooltip("Start is only ever called once for a give script.")]
        [SerializeField] private bool _logStart;

        [Header("Physics")]
        [SerializeField] private bool _logFixedUpdate;

        [Header("Game Logic")]
        [SerializeField] private bool _logUpdate;
        [SerializeField] private bool _logLateUpdate;

        [Header("Gizmo rendering")]
        [Tooltip("OnDrawGizmos is only called while working in the editor.")]
        [SerializeField] private bool _logOnDrawGizmos;

        [Header("GUI rendering")]
        [Tooltip("OnGUI is called multiple time per frame update.")]
        [SerializeField] private bool _logOnGUI;

        [Header("Pausing")]
        [Tooltip("OnApplicationPause is called after the frame where the pause occurs but issues another" +
            "frame before actually pausing.")]
        [SerializeField] private bool _logOnApplicationPause;

        [Header("Decommissioning")]
        [SerializeField] private bool _logOnApplicationQuit;
        [Tooltip("OnDisable is called only when the script was disabled during the frame." +
            "OnEnable will be called if it is enabled again.")]
        [SerializeField] private bool _logOnDisable;
        [SerializeField] private bool _logOnDestroy;


        #region Initialization
        private void Awake()
        {
            DebugLog(_logAwake, $"Awake");
        }

        private void OnEnable()
        {
            DebugLog(_logOnEnable, $"OnEnable");
        }
        #endregion

        #region Editor
        // Reset is called when the script is attached and not in playmode.
        private void Reset()
        {
            DebugLog(_logReset, $"Reset");
        }
        #endregion

        #region Initialization
        // Start is only ever called once for a give script. Event if another scene is loaded and has DontDestroy.
        private void Start()
        {
            DebugLog(_logStart, $"Start");
        }
        #endregion

        #region Physics
        private void FixedUpdate()
        {
            DebugLog(_logFixedUpdate, $"FixedUpdate");
        }

        // Here goes OnTriggerXXX
        // Here goes OnCollisionXXX
        // Here goes yield WaitForFixedUpdate
        #endregion

        #region Input Events
        // Here goes OnMouseXXX
        #endregion

        #region Game Logic
        private void Update()
        {
            DebugLog(_logUpdate, $"Update");
        }

        // If a coroutine has yielded previously but is now due to resume the execution takes place during this
        // part of the update.
        // Here goes yield null
        // Here goes yield WaitForSeconds
        // Here goes yield WWW
        // Here goes yield StartCoroutine

        private void LateUpdate()
        {
            DebugLog(_logLateUpdate, $"LateUpdate");
        }
        #endregion

        #region SceneRendering
        // Here goes OnPreCull
        // Here goes OnWillRenderObject
        // Here goes OnBecameVisible
        // Here goes OnPreRender
        // Here goes OnRenderObject
        // Here goes OnPostRender
        // Here goes OnRenderImage
        #endregion

        #region Gizmo rendering
        // OnDrawGizmos is only called while working in the editor.
        private void OnDrawGizmos()
        {
            DebugLog(_logOnDrawGizmos, $"OnDrawGizmos");
        }
        #endregion

        #region GUI rendering
        // OnGUI is called multiple time per frame update.
        private void OnGUI()
        {
            DebugLog(_logOnGUI, $"OnGUI");
        }
        #endregion

        #region End of frame
        // Here goes yield WaitForEndOfFrame
        #endregion

        #region Pausing
        // OnApplicationPause is called after the frame where the pause occurs but issues another
        // frame before actually pausing.
        private void OnApplicationPause(bool pause)
        {
            DebugLog(_logOnApplicationPause, $"OnApplicationPause {pause}");
        }
        #endregion

        #region Decommissioning
        private void OnApplicationQuit()
        {
            DebugLog(_logOnApplicationQuit, $"OnApplicationQuit");
        }

        // OnDisable is called only when the script was disabled during the frame.
        // OnEnable will be called if it is enabled again.
        private void OnDisable()
        {
            DebugLog(_logOnDisable, $"OnDisable");
        }

        private void OnDestroy()
        {
            DebugLog(_logOnDestroy, $"OnDestroy");
        }
        #endregion


        private void DebugLog(bool showMessage, string message)
        {
            if (showMessage) { Debug.Log(message, this); }
        }
    }
}
