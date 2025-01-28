using System;
using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace SombraStudios.Shared.Utility.Loggers
{
    // Increase Unity Logger performance by disabling "Use Player Log" in Resolution and Presentation Player Settings
    // This is the file that Debug Log writes to when it's called.
    // Credits to E404 Game Studios for the original code with format category and color.
    
    /// <summary>
    /// A utility class for logging messages in Unity with support for categories and colors.
    /// It will only log messages when the DEBUG condition is met. (UNITY_EDITOR || DEVELOPMENT_BUILD)
    /// </summary>
    public static class Logger
    {
        private const string DEBUG_CONDITION = "DEBUG";
        private const string LOG_COLOR = nameof(Color.white);
        private const string WARNING_COLOR = nameof(Color.yellow);
        private const string ERROR_COLOR = nameof(Color.red);
        private const string EXCEPTION_COLOR = nameof(Color.magenta);

        #region INITIALIZATION

        /// <summary>
        /// Initializes the logger on application startup.
        /// Enables or disables Unity's logger based on the DEBUG condition.
        /// DEBUG behaves like “UNITY_EDITOR || DEVELOPMENT_BUILD”
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
#if DEBUG
            Debug.unityLogger.logEnabled = true;
#else
            Debug.unityLogger.logEnabled = false;
#endif
        }

        #endregion

        #region Log

        /// <summary>
        /// Logs a message with optional sender information.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="sender">The object that sends the log (optional).</param>
        [Conditional(DEBUG_CONDITION)]
        public static void Log(object message, Object sender = null)
        {
            if (sender != null)
                Debug.Log(FormatMessage(LOG_COLOR, message), sender);
            else
                Debug.Log(FormatMessage(LOG_COLOR, message));
        }

        /// <summary>
        /// Logs a categorized message with optional sender information.
        /// </summary>
        /// <param name="category">The category of the log.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="sender">The object that sends the log (optional).</param>
        [Conditional(DEBUG_CONDITION)]
        public static void Log(string category, object message, Object sender = null)
        {
            if (sender != null)
                Debug.Log(FormatMessageWithCategory(LOG_COLOR, category, message), sender);
            else
                Debug.Log(FormatMessageWithCategory(LOG_COLOR, category, message));
        }

        /// <summary>
        /// Logs a formatted message with optional sender information.
        /// </summary>
        /// <param name="format">The message format string.</param>
        /// <param name="sender">The object that sends the log (optional).</param>
        /// <param name="args">Arguments for formatting the message.</param>
        [Conditional(DEBUG_CONDITION)]
        public static void LogFormat(string format, Object sender = null, params object[] args)
        {
            if (sender != null)
                Debug.Log(FormatMessage(LOG_COLOR, string.Format(format, args)), sender);
            else
                Debug.Log(FormatMessage(LOG_COLOR, string.Format(format, args)));
        }

        /// <summary>
        /// Logs a categorized and formatted message with optional sender information.
        /// </summary>
        /// <param name="category">The category of the log.</param>
        /// <param name="format">The message format string.</param>
        /// <param name="sender">The object that sends the log (optional).</param>
        /// <param name="args">Arguments for formatting the message.</param>
        [Conditional(DEBUG_CONDITION)]
        public static void LogFormat(string category, string format, Object sender = null, params object[] args)
        {
            if (sender != null)
                Debug.Log(FormatMessageWithCategory(LOG_COLOR, category, string.Format(format, args)), sender);
            else
                Debug.Log(FormatMessageWithCategory(LOG_COLOR, category, string.Format(format, args)));
        }

        #endregion

        #region Log Warning

        /// <summary>
        /// Logs a warning message with optional sender information.
        /// </summary>
        /// <param name="message">The warning message to log.</param>
        /// <param name="sender">The object that sends the warning (optional).</param>
        [Conditional(DEBUG_CONDITION)]
        public static void LogWarning(object message, Object sender = null)
        {
            if (sender != null)
                Debug.LogWarning(FormatMessage(WARNING_COLOR, message), sender);
            else
                Debug.LogWarning(FormatMessage(WARNING_COLOR, message));
        }

        /// <summary>
        /// Logs a categorized warning message with optional sender information.
        /// </summary>
        /// <param name="category">The category of the warning.</param>
        /// <param name="message">The warning message to log.</param>
        /// <param name="sender">The object that sends the warning (optional).</param>
        [Conditional(DEBUG_CONDITION)]
        public static void LogWarning(string category, object message, Object sender = null)
        {
            if (sender != null)
                Debug.LogWarning(FormatMessageWithCategory(WARNING_COLOR, category, message), sender);
            else
                Debug.LogWarning(FormatMessageWithCategory(WARNING_COLOR, category, message));
        }

        /// <summary>
        /// Logs a warning formatted message with optional sender information.
        /// </summary>
        /// <param name="format">The message format string.</param>
        /// <param name="sender">The object that sends the log (optional).</param>
        /// <param name="args">Arguments for formatting the message.</param>
        [Conditional(DEBUG_CONDITION)]
        public static void LogWarningFormat(string format, Object sender = null, params object[] args)
        {
            if (sender != null)
                Debug.LogWarningFormat(FormatMessage(WARNING_COLOR, string.Format(format, args)), sender);
            else
                Debug.LogWarningFormat(FormatMessage(WARNING_COLOR, string.Format(format, args)));
        }

        /// <summary>
        /// Logs a categorized and formatted warning message with optional sender information.
        /// </summary>
        /// <param name="category">The category of the log.</param>
        /// <param name="format">The message format string.</param>
        /// <param name="sender">The object that sends the log (optional).</param>
        /// <param name="args">Arguments for formatting the message.</param>
        [Conditional(DEBUG_CONDITION)]
        public static void LogWarningFormat(string category, string format, Object sender = null, params object[] args)
        {
            if (sender != null)
                Debug.LogWarningFormat(
                    FormatMessageWithCategory(WARNING_COLOR, category, string.Format(format, args)), sender);
            else
                Debug.LogWarningFormat(
                    FormatMessageWithCategory(WARNING_COLOR, category, string.Format(format, args)));
        }
        #endregion

        #region Log Error

        /// <summary>
        /// Logs an error message with optional sender information.
        /// </summary>
        /// <param name="message">The error message to log.</param>
        /// <param name="sender">The object that sends the error (optional).</param>
        [Conditional(DEBUG_CONDITION)]
        public static void LogError(object message, Object sender = null)
        {
            if (sender != null)
                Debug.LogError(FormatMessage(ERROR_COLOR, message), sender);
            else
                Debug.LogError(FormatMessage(ERROR_COLOR, message));
        }

        /// <summary>
        /// Logs a categorized error message with optional sender information.
        /// </summary>
        /// <param name="category">The category of the warning.</param>
        /// <param name="message">The warning message to log.</param>
        /// <param name="sender">The object that sends the warning (optional).</param>
        [Conditional(DEBUG_CONDITION)]
        public static void LogError(string category, object message, Object sender = null)
        {
            if (sender != null)
                Debug.LogError(FormatMessageWithCategory(ERROR_COLOR, category, message), sender);
            else
                Debug.LogError(FormatMessageWithCategory(ERROR_COLOR, category, message));
        }

        /// <summary>
        /// Logs a formatted error message with optional sender information.
        /// </summary>
        /// <param name="format">The message format string.</param>
        /// <param name="sender">The object that sends the log (optional).</param>
        /// <param name="args">Arguments for formatting the message.</param>
        [Conditional(DEBUG_CONDITION)]
        public static void LogErrorFormat(string format, Object sender = null, params object[] args)
        {
            if (sender != null)
                Debug.LogErrorFormat(FormatMessage(ERROR_COLOR, string.Format(format, args)), sender);
            else
                Debug.LogErrorFormat(FormatMessage(ERROR_COLOR, string.Format(format, args)));
        }

        /// <summary>
        /// Logs a categorized and formatted error message with optional sender information.
        /// </summary>
        /// <param name="category">The category of the log.</param>
        /// <param name="format">The message format string.</param>
        /// <param name="sender">The object that sends the log (optional).</param>
        /// <param name="args">Arguments for formatting the message.</param>
        [Conditional(DEBUG_CONDITION)]
        public static void LogErrorFormat(string category, string format, Object sender = null, params object[] args)
        {
            if (sender != null)
                Debug.LogErrorFormat(
                    FormatMessageWithCategory(ERROR_COLOR, category, string.Format(format, args)), sender);
            else
                Debug.LogErrorFormat(
                    FormatMessageWithCategory(ERROR_COLOR, category, string.Format(format, args)));
        }

        #endregion

        #region Log Exception

        /// <summary>
        /// Logs an exception with optional sender information.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="sender">The object that sends the exception log (optional).</param>
        [Conditional(DEBUG_CONDITION)]
        public static void LogException(Exception exception, Object sender = null)
        {
            if (sender != null)
                Debug.LogError(FormatMessage(EXCEPTION_COLOR, exception.Message), sender);
            else
                Debug.LogError(FormatMessage(EXCEPTION_COLOR, exception.Message));
        }

        /// <summary>
        /// Logs a categorized exception with optional sender information.
        /// </summary>
        /// <param name="category">The category of the exception.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="sender">The object that sends the exception log (optional).</param>
        [Conditional(DEBUG_CONDITION)]
        public static void LogException(string category, Exception exception, Object sender = null)
        {
            if (sender != null)
                Debug.LogError(FormatMessageWithCategory(EXCEPTION_COLOR, category, exception.Message), sender);
            else
                Debug.LogError(FormatMessageWithCategory(EXCEPTION_COLOR, category, exception.Message));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Formats a log message with a specific color.
        /// </summary>
        /// <param name="color">The color to use for the log message.</param>
        /// <param name="message">The message to format.</param>
        /// <returns>A formatted log message.</returns>
        private static string FormatMessage(string color, object message)
        {
            return $"<color={color}>{message}</color>";
        }

        /// <summary>
        /// Formats a categorized log message with a specific color.
        /// </summary>
        /// <param name="color">The color to use for the log message.</param>
        /// <param name="category">The category of the log.</param>
        /// <param name="message">The message to format.</param>
        /// <returns>A formatted log message with category.</returns>
        private static string FormatMessageWithCategory(string color, string category, object message)
        {
            return $"<color={color}><b>[{category}]</b> {message}</color>";
        }

        #endregion
    }
}
