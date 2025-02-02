using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.Scenes.Strategies
{
    /// <summary>
    /// Strategy for reloading the current active scene.
    /// </summary>
    [CreateAssetMenu(fileName = "NewReloadSceneStrategy", menuName = "Sombra Studios/Strategies/Scenes/Reload Scene")]
    public class ReloadSceneStrategy : SceneStrategy
    {
        /// <summary>
        /// Executes the scene reloading operation.
        /// </summary>
        public override void Execute()
        {
            if (_showLogs)
            {
                Logger.Log(LOG_CATEGORY, "Reloading current scene.");
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
