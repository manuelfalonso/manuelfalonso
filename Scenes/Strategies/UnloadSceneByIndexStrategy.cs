using SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects;
using SombraStudios.Shared.Utility.Coroutines;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.Scenes.Strategies
{
    /// <summary>
    /// Destroys all GameObjects associated with the given Scene and removes the Scene from the SceneManager.
    /// </summary>
    /// <remarks>
    /// It is not possible to UnloadSceneAsync if there are no scenes to load (e.g., a project with only one scene).
    /// </remarks>
    [CreateAssetMenu(fileName = "NewUnloadSceneStrategy", menuName = "Sombra Studios/Strategies/Scenes/Unload Scene by Index")]
    public class UnloadSceneByIndexStrategy : SceneStrategy<int>
    {
        [Header(PROPERTIES_TITLE)]

        /// <summary>
        /// Options to configure how the scene is unloaded.
        /// </summary>
        [Tooltip("Options to configure how the scene is unloaded.")]
        public UnloadSceneOptions UnloadSceneOptions;

        /// <summary>
        /// Event that is raised to update the progress of scene unloading. Its a value from 0 to 1.
        /// </summary>
        [Tooltip("Event that is raised to update the progress of scene unloading. Its a value from 0 to 1.")]
        public FloatEventChannelSO UnloadProgressUpdated;

        /// <summary>
        /// Determines whether to unload unused assets after unloading the scene. 
        /// </summary>
        [Tooltip("Determines whether to unload unused assets after loading the scene. ")]
        public bool UnloadUnusedAssets = true;

        private string _scenePath;

        /// <summary>
        /// Checks if the scene can be unloaded based on the provided build index.
        /// </summary>
        /// <param name="buildIndex">The build index of the scene to be unloaded.</param>
        /// <returns>True if the scene can be unloaded, false otherwise.</returns>
        public override bool CanExecute(int buildIndex)
        {
            _scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);

            if (string.IsNullOrEmpty(_scenePath))
            {
                Logger.LogError(LOG_CATEGORY, "Invalid scene build index.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes the scene unloading process.
        /// </summary>
        /// <param name="buildIndex">The build index of the scene to be unloaded.</param>
        public override void Execute(int buildIndex)
        {
            CoroutineManager.Instance.StartCoroutine(UnloadSceneAsync(_scenePath, UnloadSceneOptions));
        }

        /// <summary>
        /// Unloads the scene asynchronously.
        /// </summary>
        /// <param name="scenePath">The path of the scene to be unloaded.</param>
        /// <param name="unloadSceneOptions">Options for unloading the scene.</param>
        /// <returns>An enumerator for the unloading process.</returns>
        private IEnumerator UnloadSceneAsync(string scenePath, UnloadSceneOptions unloadSceneOptions)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scenePath, unloadSceneOptions);

            // Wait until the scene is fully unloaded
            while (!asyncUnload.isDone)
            {
                // Raise event to update unload progress
                if (UnloadProgressUpdated != null)
                    UnloadProgressUpdated.RaiseEvent(asyncUnload.progress);

                yield return null;
            }

            // Optionally unload unused assets
            if (UnloadUnusedAssets)
            {
                Resources.UnloadUnusedAssets();
            }
        }
    }
}
