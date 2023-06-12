using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace SombraStudios.Utility
{
    /// <summary>
    /// Util class to show log messages in a UI text component
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DebugLogPanel : MonoBehaviour
    {
        [Header("Visual Feedback")]
        [Tooltip("Granularity. Sometimes you may not want to see everything being sent to the console.")]
        [SerializeField]
        private LogType LogLevel;
        [SerializeField]
        private bool logAnyMessage = true;

        [Tooltip("Maximum number of messages before deleting the older messages.")]
        [SerializeField]
        private int maxNumberOfMessages = 15;

        [Tooltip("Check this if you want the stack trace printed after the message.")]
        [SerializeField]
        private bool includeStackTrace = false;

        [Header("Auditory Feedback")]
        [Tooltip("Play a sound when the message panel is updated.")]
        [SerializeField]
        private bool playSoundOnMessage;

        private bool newMessageArrived = false;

        private TextMeshProUGUI debugText;

        // The queue with the messages:
        private Queue<string> messageQueue;

        // The message sound, should you use one
        private AudioSource messageSound;

        void OnEnable()
        {
            messageQueue = new Queue<string>();
            debugText = GetComponent<TextMeshProUGUI>();
            messageSound = GetComponent<AudioSource>();
            Application.logMessageReceivedThreaded += 
                Application_logMessageReceivedThreaded;
        }

        private void Application_logMessageReceivedThreaded(
            string condition, string stackTrace, LogType type)
        {
            if (type == LogLevel || logAnyMessage)
            {
                if (messageSound != null && playSoundOnMessage)
                {
                    messageSound.Play();
                }

                newMessageArrived = true;

                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append("\n");
                Color targetColor;
                switch (type)
                {
                    case LogType.Error:
                        targetColor = Color.red;
                        break;
                    case LogType.Assert:
                        targetColor = Color.red;
                        break;
                    case LogType.Warning:
                        targetColor = Color.yellow;
                        break;
                    case LogType.Log:
                        targetColor = Color.white;
                        break;
                    case LogType.Exception:
                        targetColor = Color.red;
                        break;
                    default:
                        targetColor = Color.magenta;
                        break;
                }
                string colorString = ColorUtility.ToHtmlStringRGB(targetColor);

                // String Color format
                stringBuilder.Append("<color=#").Append(colorString).Append(">");
                // Type
                stringBuilder.Append(type.ToString()).Append(":");
                stringBuilder.Append(condition);

                if (includeStackTrace)
                {
                    stringBuilder.Append("\nStackTrace: ");
                    stringBuilder.Append(stackTrace);
                }
                stringBuilder.Append(" </color>");

                // Enqueue console message
                condition = stringBuilder.ToString();
                messageQueue.Enqueue(condition);

                if (messageQueue.Count > maxNumberOfMessages)
                {
                    messageQueue.Dequeue();
                }
            }
        }

        void OnDisable()
        {
            Application.logMessageReceivedThreaded -= Application_logMessageReceivedThreaded;
        }

        /// <summary>
        /// This Update method checks if a new message has arrived. The check is placed 
        /// here to ensure that only the main thread will try to access the Text Mesh.
        /// </summary>
        void Update()
        {
            if (newMessageArrived)
            {
                PrintQueue();
                newMessageArrived = false;
            }
        }

        /// <summary>
        /// Print the queue to the text mesh.
        /// </summary>
        void PrintQueue()
        {
            StringBuilder stringBuilder = new StringBuilder();
            string[] messageList = messageQueue.ToArray();

            //for (int i = 0; i < messageList.Length; i++) {
            for (int i = messageList.Length - 1; i >= 0; i--)
            {
                stringBuilder.Append(messageList[i]);
                stringBuilder.Append("\n");
            }

            string message = stringBuilder.ToString();
            debugText.text = message;
        }
    }
}