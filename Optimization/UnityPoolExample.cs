using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// This example creates a game object prefab using a pool so that old objects can be reused.
/// </summary>
public class UnityPoolExample : MonoBehaviour
{
    public Transform prefab;
    public Transform poolParent;

    // Collection checks will throw errors if we try to release an item that is already in the pool.
    public bool collectionChecks = true;
    public int defaultPoolSize = 10;

    IObjectPool<Transform> m_Pool;

    public IObjectPool<Transform> Pool
    {
        get
        {
            if (m_Pool == null)
            {
                m_Pool = new ObjectPool<Transform>(
                    CreatePooledItem,
                    OnTakeFromPool,
                    OnReturnedToPool,
                    OnDestroyPoolObject,
                    collectionChecks,
                    defaultPoolSize);
            }
            return m_Pool;
        }
    }

    Transform CreatePooledItem()
    {
        var go = Instantiate(prefab, poolParent);

        // This is used to return the object to the pool.
        var returnToPool = go.gameObject.AddComponent<ReturnToPool>();
        returnToPool.pool = Pool;

        return go;
    }

    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(Transform system)
    {
        system.gameObject.SetActive(true);
    }

    // Called when an item is returned to the pool using Release
    void OnReturnedToPool(Transform system)
    {
        system.gameObject.SetActive(false);
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(Transform system)
    {
        Destroy(system.gameObject);
    }
}

/// <summary>
/// This component returns the object to the pool when Its disabled.
/// </summary>
public class ReturnToPool : MonoBehaviour
{
    public IObjectPool<Transform> pool;

    void OnDisable()
    {
        // Return to the pool
        pool.Release(transform);
    }
}