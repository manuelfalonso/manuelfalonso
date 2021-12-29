using UnityEngine;

/// <summary>
/// Inherit to create a single, global-accesible instance of a class, 
/// available at all times.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Monobehaviour
{
    private static T _instance = null;
    public static T Instance
    {
        get 
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
                if (_instance == null)
                {
                    var singletonObj = new GameObject();
                    singletonObj.name = typeof(T).ToString();
                    _instance = singletonObj.AddComponent<T>();
                }
            }
            return _instance; 
        }
    }

    public virtual void Awake()
    {
        if (_instance != null)
        {
			Destroy(gameObject);
            return;
        }

        _instance = GetComponent<T>();

        // The GameObject will persist across multiple scenes.
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
            return;
    }
}
