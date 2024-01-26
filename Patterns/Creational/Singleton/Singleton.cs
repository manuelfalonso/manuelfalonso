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

            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = (T)this;
                // or
                //_instance = this as T;
            }
        }
    }
}
