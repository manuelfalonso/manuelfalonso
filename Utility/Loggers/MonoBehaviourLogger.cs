// Credits to Jason Storey
using UnityEngine;

namespace SombraStudios.Shared.Utility.Loggers
{
    /// <summary>
    /// A MonoBehaviour-based logger service that logs messages to the Unity console with optional color-coded prefixes.
    /// </summary>
    /// <remarks>
    /// This logger allows enabling or disabling logs and supports custom prefixes with color formatting.
    /// </remarks>
    [AddComponentMenu("_SombraStudios/Services/Logger")]
    public class MonoBehaviourLogger : MonoBehaviour, ILoggerService
    {
        [Header("Settings")]

        /// <summary>
        /// Determines whether logs should be displayed in the Unity console.
        /// </summary>
        [Tooltip("Determines whether logs should be displayed in the Unity console.")]
        [SerializeField] private bool _showLogs = true;

        /// <summary>
        /// A prefix added before each log message, useful for identifying the source of logs.
        /// </summary>
        [Tooltip("A prefix added before each log message, useful for identifying the source of logs.")]
        [SerializeField] private string _prefix = string.Empty;

        /// <summary>
        /// The color used for the prefix in log messages.
        /// </summary>
        [Tooltip("The color used for the prefix in log messages.")]
        [SerializeField] private Color _prefixColor = Color.white;

        /// <summary>
        /// The hexadecimal string representation of the prefix color for use in Unity logs.
        /// </summary>
        private string _hexColor = string.Empty;

        /// <summary>
        /// Updates the hex color representation when changes are made in the Unity Inspector.
        /// </summary>
        private void OnValidate()
        {
            _hexColor = "#" + ColorUtility.ToHtmlStringRGBA(_prefixColor);
        }

        /// <summary>
        /// Logs a message to the Unity console.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Log(string message)
        {
            Log(message, this);
        }

        /// <summary>
        /// Logs a message to the Unity console.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void Log(object message)
        {
            Log(message, this);
        }

        /// <summary>
        /// Logs a message to the Unity console with an optional sender reference.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="sender">The object that sent the log message (default: this logger).</param>
        public void Log(object message, Object sender = null)
        {
            if (!_showLogs) return;
            Debug.Log($"<color={_hexColor}>{_prefix}</color> {message}", sender);
        }
    }
}
