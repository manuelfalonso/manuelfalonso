namespace SombraStudios.Tools.Coroutines
{

    using UnityEngine;

    /// <summary>
    /// Scene persistent Singleton MonoBehaviour to run Coroutines
    /// </summary>
    public class CoroutineManager : MonoBehaviour
    {
        private static CoroutineManager _instance;
        public static CoroutineManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject(nameof(CoroutineManager)).AddComponent<CoroutineManager>();
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }
    }
}
