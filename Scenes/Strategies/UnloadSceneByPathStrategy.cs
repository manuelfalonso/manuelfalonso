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
    /// The given Scene name can either be the full Scene path, the path shown in the Build Settings window,
    /// or just the Scene name. If only the Scene name is given, this will unload the first Scene in the list
    /// that matches. If you have multiple Scenes with the same name but different paths, you should use the full
    /// Scene path.
    /// </summary>
    /// <remarks>
    /// It is not possible to UnloadSceneAsync if there are no scenes to load (e.g., a project with only one scene).
    /// </remarks>
    [CreateAssetMenu(fileName = "NewUnloadSceneStrategy", menuName = "Sombra Studios/Strategies/Scenes/Unload Scene by Path")]
    public class UnloadSceneByPathStrategy : SceneStrategy<string>
    {
        [Header(PROPERTIES_TITLE)]

        /// <summary>
        /// Options to configure how the scene is unloaded.
        /// </summary>
        [Tooltip("Options to configure how the scene is unloaded.")]
        public UnloadSceneOptions UnloadSceneOptions;

        /// <summary>
        /// Event that is raised to update the progress of scene unloading. 
        /// The value ranges from 0 to 1, where 0 represents no progress and 1 represents completion.
        /// </summary>
        [Tooltip("Event that is raised to update the progress of scene unloading. Its a value from 0 to 1.")]
        public FloatEventChannelSO UnloadProgressUpdated;

        /// <summary>
        /// Determines whether to unload unused assets after unloading the scene. 
        /// </summary>
        [Tooltip("Determines whether to unload unused assets after loading the scene. ")]
        public bool UnloadUnusedAssets = true;

        /// <summary>
        /// Checks if the scene can be unloaded based on the provided scene path.
        /// </summary>
        /// <param name="scenePath">The scene path of the scene to be unloaded.</param>
        /// <returns>True if the scene can be unloaded, false otherwise.</returns>
        public override bool CanExecute(string scenePath)
        {
            if (string.IsNullOrEmpty(scenePath))
            {
                Logger.LogError(LOG_CATEGORY, "Invalid scene path.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Executes the scene unloading process based on the provided scene path.
        /// </summary>
        /// <param name="scenePath">The path of the scene to be unloaded.</param>
        public override void Execute(string scenePath)
        {
            CoroutineManager.Instance.StartCoroutine(UnloadSceneAsync(scenePath, UnloadSceneOptions));
        }

        /// <summary>
        /// Unloads the scene asynchronously.
        /// </summary>
        /// <param name="scenePath">The path of the scene to be unloaded.</param>
        /// <param name="unloadSceneOptions">Options for unloading the scene.</param>
        /// <returns>An enumerator that manages the asynchronous unloading process.</returns>
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
