using UnityEngine;

namespace SombraStudios.Patterns.Creational.Singleton
{
    /// <summary>
    /// Util Class for creating Singleton classes derived from it
    /// </summary>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
	    [SerializeField] private bool _dontDestroyOnLoad = false;
	
        private static T _instance;
        public static T Instance
        {
            get { return _instance; }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
			    Destroy(gameObject);      
            }
            else
            {
                _instance = (T) this;
			    // The GameObject will persist across multiple scenes.
			    if (_dontDestroyOnLoad)
                {
				    DontDestroyOnLoad(gameObject);
                }
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
}
