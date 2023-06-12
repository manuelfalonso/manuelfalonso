using UnityEngine;

namespace SombraStudios.Tools
{
    // Increase Unity Logger performance by disabling "Use Player Log" in Resolution and Presentation Player Settings
    // This is the file that Debug Log writes to when it’s called.

    public static class StaticLogger
    {

        // This will remove generated garbage from Logs in builds
        [System.Diagnostics.Conditional("UNITY_EDITOR")]
        public static void Log(bool verbose, object message, UnityEngine.Object sender = null)
        {
            if (!verbose)
                return;

            if (sender != null)
                Debug.Log($"{sender.GetType()} => {message}", sender);
            else
                Debug.Log($"{message}");
        }
    }
}
