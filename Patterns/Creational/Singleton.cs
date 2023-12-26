using UnityEngine;

namespace SombraStudios.Shared.Patterns.Creational.Singleton
{
    /// <summary>
    /// Utility class for creating Singleton classes derived from it.
    /// </summary>
    /// <typeparam name="T">The type of the Singleton.</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// Determines whether the Singleton instance should persist across scene changes.
        /// </summary>
        [Tooltip("Determines whether the Singleton instance should persist across scene changes.")]
        [SerializeField] private bool _dontDestroyOnLoad = false;

        /// <summary>
        /// The static instance of the Singleton.
        /// </summary>
        protected static T _instance;

        /// <summary>
        /// Gets a value indicating whether the Singleton has an instance.
        /// </summary>
        public static bool HasInstance => _instance != null;
        /// <summary>
        /// Tries to get the instance of the Singleton.
        /// </summary>
        /// <returns>The instance of the Singleton if it exists; otherwise, null.</returns>
        public static T TryGetInstance() => HasInstance ? _instance : null;
        /// <summary>
        /// Gets the current instance of the Singleton.
        /// </summary>
        public static T Current => _instance;
        /// <summary>
        /// Gets the instance of the Singleton, creating it if necessary.
        /// </summary>
        public static T Instance
        {
            get {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();
                    if (_instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name + "AutoCreated";
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance; 
            }
        }


        protected virtual void Awake() => HandleInstance();

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }


        /// <summary>
        /// Handles the creation and destruction of the Singleton instance.
        /// </summary>
        private void HandleInstance()
        {
            if (!Application.isPlaying) { return; }

            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (T)this;

                // The GameObject will persist across multiple scenes.
                if (_dontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
        }
    }
}
