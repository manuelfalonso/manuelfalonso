using UnityEngine;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.Scenes.Strategies
{
    /// <summary>
    /// Strategy for quitting the application. Can be used as part of a scene strategy pattern.
    /// </summary>
    [CreateAssetMenu(fileName = "NewQuitStrategy", menuName = "Sombra Studios/Strategies/Scenes/Quit")]
    public class QuitStrategy : SceneStrategy
    {
        /// <summary>
        /// Executes the quit strategy, logging the action and quitting the application.
        /// </summary>
        public override void Execute()
        {
            if (_showLogs)
            {
                Logger.Log(LOG_CATEGORY, "Quitting application...");
            }

#if UNITY_EDITOR
            // Stops play mode in the Unity Editor
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // Quits the application in a built version
            Application.Quit();
#endif
        }
    }
}
