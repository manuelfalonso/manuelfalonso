using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.Scenes.Strategies
{
    /// <summary>
    /// Strategy for loading the next scene in the build index.
    /// </summary>
    [CreateAssetMenu(fileName = "NewLoadNextSceneStrategy", menuName = "Sombra Studios/Strategies/Scenes/Load Next Scene")]
    public class LoadNextSceneStrategy : SceneStrategy
    {
        /// <summary>
        /// The scene index that will be loaded next.
        /// </summary>
        private int _sceneToLoadIndex;

        /// <summary>
        /// Determines whether the next scene can be loaded.
        /// </summary>
        /// <returns>True if the next scene is valid, otherwise false.</returns>
        public override bool CanExecute()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            _sceneToLoadIndex = currentSceneIndex + 1;
            bool canLoad = _sceneToLoadIndex < SceneManager.sceneCountInBuildSettings;
            
            if (canLoad)
            {
                if (_showLogs)
                    Logger.Log(LOG_CATEGORY, $"Loading next scene at index: {_sceneToLoadIndex}");
                return true;
            }

            Logger.LogError(LOG_CATEGORY, "Scene to load is invalid.");
            return false;
        }

        /// <summary>
        /// Executes the scene loading operation.
        /// </summary>
        public override void Execute()
        {
            SceneManager.LoadScene(_sceneToLoadIndex);
        }
    }
}
