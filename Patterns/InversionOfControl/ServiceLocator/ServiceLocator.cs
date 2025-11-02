using SombraStudios.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SombraStudios.Shared.Patterns.InversionOfControl.ServiceLocator
{
    // Credits to git-ammend 
    public class ServiceLocator : MonoBehaviour
    {
        private static ServiceLocator _globalLocator;
        private static Dictionary<Scene, ServiceLocator> _sceneLocators = new();
        private static List<GameObject> _tempSceneGameObjects = new();

        private readonly ServiceManager _serviceManager = new();

        private const string GLOBAL_LOCATOR_NAME = "ServiceLocator [Global]";
        private const string SCENE_LOCATOR_NAME = "ServiceLocator [Scene]";

        #region Unity Messages
        void OnDestroy()
        {
            if (this == _globalLocator)
            {
                _globalLocator = null;
            }
            else if (_sceneLocators.ContainsValue(this))
            {
                _sceneLocators.Remove(gameObject.scene);
            }
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Gets the global ServiceLocator instance. Creates new if none exists.
        /// </summary>
        public static ServiceLocator Global
        {
            get
            {
                // Return existing global locator if available
                if (_globalLocator != null) { return _globalLocator; }

                // Try to find an existing ServiceLocatorGlobal in the scene
                if (FindFirstObjectByType<ServiceLocatorGlobal>() is ServiceLocatorGlobal foundLocator)
                {
                    foundLocator.BootstrapOnDemand();
                    return _globalLocator;
                }

                // Create a new GameObject with ServiceLocatorGlobal component
                var go = new GameObject(GLOBAL_LOCATOR_NAME, typeof(ServiceLocator));
                go.AddComponent<ServiceLocatorGlobal>().BootstrapOnDemand();

                return _globalLocator;
            }
        }

        /// <summary>
        /// Returns the <see cref="ServiceLocator"/> configured for the scene of a MonoBehaviour. Falls back to the global instance.
        /// </summary>
        public static ServiceLocator ForSceneOf(MonoBehaviour mb)
        {
            Scene scene = mb.gameObject.scene;

            if (_sceneLocators.TryGetValue(scene, out var locator) && locator != mb)
            {
                return locator;
            }

            // Try to find an existing ServiceLocatorScene in the scene
            _tempSceneGameObjects.Clear();

            scene.GetRootGameObjects(_tempSceneGameObjects);

            foreach (var go in _tempSceneGameObjects.Where(x => x.GetComponent<ServiceLocatorScene>() != null))
            {
                if (go.TryGetComponent<ServiceLocatorScene>(out var foundLocator) && foundLocator.ServiceLocator != mb)
                {
                    foundLocator.BootstrapOnDemand();
                    return foundLocator.ServiceLocator;
                }
            }

            return Global;
        }

        /// <summary>
        /// Gets the closest ServiceLocator instance to the provided 
        /// MonoBehaviour in hierarchy, the ServiceLocator for its scene, or the global ServiceLocator.
        /// </summary>
        public static ServiceLocator For(MonoBehaviour mb)
        {
            return mb.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(mb) ?? Global;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Registers a service to the ServiceLocator using the service's type.
        /// </summary>
        /// <param name="service">The service to register.</param>  
        /// <typeparam name="T">Class type of the service to be registered.</typeparam>
        /// <returns>The ServiceLocator instance after registering the service.</returns>
        public ServiceLocator Register<T>(T service)
        {
            _serviceManager.Register(service);
            return this;
        }

        /// <summary>
        /// Registers a service to the ServiceLocator using a specific type.
        /// </summary>
        /// <param name="type">The type to use for registration.</param>
        /// <param name="service">The service to register.</param>  
        /// <returns>The ServiceLocator instance after registering the service.</returns>
        public ServiceLocator Register(Type type, object service)
        {
            _serviceManager.Register(type, service);
            return this;
        }

        /// <summary>
        /// Gets a service of a specific type. If no service of the required type is found, an error is thrown.
        /// </summary>
        /// <param name="service">Service of type T to get.</param>  
        /// <typeparam name="T">Class type of the service to be retrieved.</typeparam>
        /// <returns>The ServiceLocator instance after attempting to retrieve the service.</returns>
        public ServiceLocator Get<T>(out T service) where T : class
        {
            if (TryGetService(out service)) return this;

            if (TryGetNextInHierarchy(out ServiceLocator container))
            {
                container.Get(out service);
                return this;
            }

            throw new ArgumentException($"ServiceLocator.Get: Service of type {typeof(T).FullName} not registered");
        }

        /// <summary>
        /// Allows retrieval of a service of a specific type. An error is thrown if the required service does not exist.
        /// </summary>
        /// <typeparam name="T">Class type of the service to be retrieved.</typeparam>
        /// <returns>Instance of the service of type T.</returns>
        public T Get<T>() where T : class
        {
            Type type = typeof(T);
            T service = null;

            if (TryGetService(type, out service)) return service;

            if (TryGetNextInHierarchy(out ServiceLocator container))
                return container.Get<T>();

            throw new ArgumentException($"Could not resolve type '{typeof(T).FullName}'.");
        }

        /// <summary>
        /// Tries to get a service of a specific type. Returns whether or not the process is successful.
        /// </summary>
        /// <param name="service">Service of type T to get.</param>  
        /// <typeparam name="T">Class type of the service to be retrieved.</typeparam>
        /// <returns>True if the service retrieval was successful, false otherwise.</returns>
        public bool TryGet<T>(out T service) where T : class
        {
            Type type = typeof(T);
            service = null;

            if (TryGetService(type, out service))
                return true;

            return TryGetNextInHierarchy(out ServiceLocator container) && container.TryGet(out service);
        }
        #endregion

        #region Internal Methods
        internal void ConfigureAsGlobal(bool dontDestroyOnLoad)
        {
            if (_globalLocator == this)
            {
                Debug.LogWarning("ServiceLocator.ConfigureAsGlobal: Already configured as global", this);
            }
            else if (_globalLocator != null)
            {
                Debug.LogError("ServiceLocator.ConfigureAsGlobal: Another ServiceLocator is already configured as global", this);
            }
            else
            {
                _globalLocator = this;
                if (dontDestroyOnLoad) DontDestroyOnLoad(gameObject);
            }
        }

        internal void ConfigureForScene()
        {
            Scene scene = gameObject.scene;

            if (_sceneLocators.ContainsKey(scene))
            {
                Debug.LogError("ServiceLocator.ConfigureForScene: Another ServiceLocator is already configured for this scene", this);
                return;
            }

            _sceneLocators.Add(scene, this);
        }
        #endregion

        #region Private Static Methods
        // https://docs.unity3d.com/ScriptReference/RuntimeInitializeOnLoadMethodAttribute.html
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetStatics()
        {
            _globalLocator = null;
            _sceneLocators = new Dictionary<Scene, ServiceLocator>();
            _tempSceneGameObjects = new List<GameObject>();
        }

#if UNITY_EDITOR
        [MenuItem("GameObject/Sombra Studios/ServiceLocator/Add Global")]
        private static void AddGlobalServiceLocator()
        {
            var go = new GameObject(GLOBAL_LOCATOR_NAME, typeof(ServiceLocatorGlobal));
            EditorUtility.SetDirty(go);
        }

        [MenuItem("GameObject/Sombra Studios/ServiceLocator/Add Scene")]
        private static void AddSceneServiceLocator()
        {
            var go = new GameObject(SCENE_LOCATOR_NAME, typeof(ServiceLocatorScene));
            EditorUtility.SetDirty(go);
        }
#endif
        #endregion

        #region Private Methods
        private bool TryGetService<T>(out T service) where T : class
        {
            return _serviceManager.TryGet(out service);
        }

        private bool TryGetService<T>(Type type, out T service) where T : class
        {
            return _serviceManager.TryGet(out service);
        }

        private bool TryGetNextInHierarchy(out ServiceLocator locator)
        {
            if (this == Global)
            {
                locator = null;
                return false;
            }

            locator = transform.parent.OrNull()?.GetComponentInParent<ServiceLocator>().OrNull() ?? ForSceneOf(this);
            return locator != null;
        }
        #endregion
    }
}
