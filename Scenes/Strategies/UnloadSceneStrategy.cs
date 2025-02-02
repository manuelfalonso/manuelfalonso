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
    [CreateAssetMenu(fileName = "NewUnloadSceneStrategy", menuName = "Sombra Studios/Strategies/Scenes/Unload Scene by Scene")]
    public class UnloadSceneStrategy : SceneStrategy
    {
        [Header(PROPERTIES_TITLE)]

        /// <summary>
        /// The Scene to be unloaded.
        /// </summary>
        [HideInInspector] public Scene Scene;

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
        [Tooltip("Determines whether to unload unused assets after unloading the scene.")]
        public bool UnloadUnusedAssets = true;

        /// <summary>
        /// Check if the scene is valid before unloading it.
        /// </summary>
        public override bool CanExecute()
        {
            if (!Scene.IsValid())
            {
                Logger.LogError(LOG_CATEGORY, "Invalid scene.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Executes the scene unloading process.
        /// </summary>
        public override void Execute()
        {
            CoroutineManager.Instance.StartCoroutine(UnloadSceneAsync(Scene, UnloadSceneOptions));
        }

        /// <summary>
        /// Unloads the scene asynchronously.
        /// </summary>
        /// <param name="scene">The Scene object to be unloaded.</param>
        /// <param name="unloadSceneOptions">Options for unloading the scene.</param>
        /// <returns>An enumerator that manages the asynchronous unloading process.</returns>
        private IEnumerator UnloadSceneAsync(Scene scene, UnloadSceneOptions unloadSceneOptions)
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scene, unloadSceneOptions);

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
