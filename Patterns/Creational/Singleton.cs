using UnityEngine;

namespace SombraStudios.Shared.Patterns.Creational.Singleton
{
    /// <summary>
    /// Utility class for creating Singleton classes derived from it.
    /// </summary>
    /// <typeparam name="T">The type of the Singleton.</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        [SerializeField] private bool _dontDestroyOnLoad = false;

        private static T _instance;
        /// <summary>
        /// Gets the instance of the Singleton.
        /// </summary>
        public static T Instance
        {
            get { return _instance; }
        }


        protected virtual void Awake()
        {
            HandleInstance();
        }

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
