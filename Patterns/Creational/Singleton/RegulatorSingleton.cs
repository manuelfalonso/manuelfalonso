using UnityEngine;

namespace SombraStudios.Shared.Patterns.Creational.Singleton
{
    /// <summary>
    /// Persistent Regulator Singleton, will destroy any other older instances of the same type it finds on awake.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RegulatorSingleton<T> : MonoBehaviour where T : RegulatorSingleton<T>
    {
        /// <summary>
        /// The static instance of the Singleton.
        /// </summary>
        protected static T _instance;

        /// <summary>
        /// Gets a value indicating whether the Singleton has an instance.
        /// </summary>
        public static bool HasInstance => _instance != null;
        /// <summary>
        /// Gets the current instance of the Singleton.
        /// </summary>
        public static T Current => _instance;
        public float InitializationTime { get; private set; }
        /// <summary>
        /// Gets the instance of the Singleton, creating it if necessary.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();
                    // Faster but returns an arbitrary object of the type
                    //_instance = FindAnyObjectByType<T>();

                    if (_instance == null)
                    {
                        var obj = new GameObject(typeof(T).Name + " AutoCreated");
                        obj.hideFlags = HideFlags.HideAndDontSave;
                        _instance = obj.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }


        /// <summary>
        /// Make sure to call base.Awake() when overriding this method.
        /// </summary>
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
            InitializationTime = Time.time;
            DontDestroyOnLoad(gameObject);

            T[] oldInstances = FindObjectsByType<T>(FindObjectsSortMode.None);
            foreach (T oldInstance in oldInstances)
            {
                if (oldInstance.GetComponent<RegulatorSingleton<T>>().InitializationTime < InitializationTime)
                {
                    Destroy(oldInstance.gameObject);
                }
            }

            if (_instance == null)
            {
                _instance = (T)this;
                // or
                //_instance = this as T;
            }
        }
    }
}
