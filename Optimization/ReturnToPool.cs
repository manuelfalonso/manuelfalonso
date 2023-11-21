using UnityEngine;
using UnityEngine.Pool;

namespace SombraStudios.Shared.Optimization
{    
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
}