using SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects;
using SombraStudios.Shared.Utility.Coroutines;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.Scenes.Strategies
{
    /// <summary>
    /// Strategy for asynchronously or synchronously loading a scene by its build index, with optional parameters. 
    /// If a single mode scene is loaded, Unity calls Resources.UnloadUnusedAssets automatically.
    /// </summary>
    [CreateAssetMenu(fileName = "NewLoadSceneByIndexStrategy", menuName = "Sombra Studios/Strategies/Scenes/Load Scene by Index")]
    public class LoadSceneByIndexStrategy : SceneStrategy<int>
    {
        [Header(PROPERTIES_TITLE)]
        /// <summary>
        /// Determines whether the scene should be loaded asynchronously.
        /// </summary>
        [Tooltip("Determines whether the scene should be loaded asynchronously.")]
        public bool AsyncLoad;

        /// <summary>
        /// Parameters to be used when loading the scene.
        /// </summary>
        [Tooltip("Parameters to be used when loading the scene.")]
        public LoadSceneParameters LoadSceneParameters;

        /// <summary>
        /// Event channel to update load progress. It will return values from 0 to 1.
        /// This is only applicable to async loading.
        /// </summary>
        [Tooltip("Event channel to update load progress. It will return values from 0 to 1. " +
            "This is only applicable to async loading.")]
        public FloatEventChannelSO LoadProgressUpdated;

        /// <summary>
        /// Determines whether the scene should be activated after loading.
        /// If false, the scene will be loaded but not activated until SceneActivationListener is invoked.
        /// This is only applicable to async loading.
        /// </summary>
        [Tooltip("Determines whether the scene should be activated after loading. " +
            "If false, the scene will be loaded but not activated until SceneActivationListener is invoked.")]
        public bool AllowSceneActivation = true;

        /// <summary>
        /// Event channel to listen for scene activation triggers when AllowSceneActivation is set to false.
        /// </summary>
        [Tooltip("Event channel to listen for scene activation triggers when AllowSceneActivation is set to false.")]
        public BoolEventChannelSO SceneActivationListener;

        /// <summary>
        /// Determines whether the newly loaded scene should be set as the active scene. The active Scene is the 
        /// Scene which will be used as the target for new GameObjects instantiated by scripts and from what 
        /// Scene the lighting settings are used. There must always be one Scene marked as the active Scene.
        /// </summary>
        [Tooltip("Determines whether the newly loaded scene should be set as the active scene. " +
            "The active Scene is the Scene which will be used as the target for new " +
            "GameObjects instantiated by scripts and from what Scene the lighting settings " +
            "are used.")]
        public bool SetActiveScene = true;

        /// <summary>
        /// Determines whether to unload unused assets after loading the scene. 
        /// This is only applicable to additive scenes.
        /// </summary>
        [Tooltip("Determines whether to unload unused assets after loading the scene. " +
                       "This is only applicable to additive scenes.")]
        public bool UnloadUnusedAssets = true;

        private string _scenePath;
        private AsyncOperation _currentAsyncOperation;

        #region Unity Messages

        private void OnEnable()
        {
            if (SceneActivationListener != null)
                SceneActivationListener.OnEventRaised += SetAllowSceneActivation;
        }

        private void OnDisable()
        {
            if (SceneActivationListener != null)
                SceneActivationListener.OnEventRaised -= SetAllowSceneActivation;
        }

        #endregion

        #region Public Methods

        public override bool CanExecute(int buildIndex)
        {
            _scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);

            if (string.IsNullOrEmpty(_scenePath))
            {
                Logger.LogError(LOG_CATEGORY, "Invalid scenePath");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes the scene loading operation based on the given build index.
        /// </summary>
        /// <param name="buildIndex">The build index of the scene to load.</param>
        public override void Execute(int buildIndex)
        {
            if (string.IsNullOrEmpty(_scenePath))
            {
                Logger.LogError(LOG_CATEGORY, "Build Index must be checked, call TryExecute instead.", this);
                return;
            }

            if (AsyncLoad)
                CoroutineManager.Instance.StartCoroutine(LoadSceneAsync(_scenePath, LoadSceneParameters));
            else
                SceneManager.LoadScene(_scenePath, LoadSceneParameters);
        }

        #endregion

        #region Private Methods

        private void SetAllowSceneActivation(bool active)
        {
            if (_currentAsyncOperation != null)
                _currentAsyncOperation.allowSceneActivation = active;
            else
                Logger.LogWarning(LOG_CATEGORY, "No async operation to set scene activation.");
        }

        /// <summary>
        /// Loads the scene asynchronously while updating progress.
        /// </summary>
        /// <param name="scenePath">The path of the scene to load.</param>
        /// <param name="loadSceneParameters">Parameters for loading the scene.</param>
        private IEnumerator LoadSceneAsync(string scenePath, LoadSceneParameters loadSceneParameters)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenePath, loadSceneParameters);
            _currentAsyncOperation = asyncLoad;
            asyncLoad.allowSceneActivation = AllowSceneActivation;

            while (!asyncLoad.isDone)
            {
                float progress = asyncLoad.progress;
                if (LoadProgressUpdated != null)
                    LoadProgressUpdated.RaiseEvent(progress);
                yield return null;
            }

            if (SetActiveScene)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByPath(scenePath));
            }

            if (loadSceneParameters.loadSceneMode == LoadSceneMode.Additive && UnloadUnusedAssets)
                Resources.UnloadUnusedAssets();
        }

        #endregion
    }
}
