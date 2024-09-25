using SombraStudios.Shared.Patterns.Creational.ObjectPool;
using UnityEngine;

namespace SombraStudios.Shared.Optimization
{
    /// <summary>
    /// This example creates a game object prefab using a pool so that old objects can be reused.
    /// </summary>
    public class UnityPoolExample : BaseObjectPool<Transform>
    {
        protected override void ActionOnDestroy(Transform t)
        {
            Destroy(t.gameObject);
        }

        protected override void ActionOnGet(Transform t)
        {
            t.gameObject.SetActive(true);
        }

        protected override void ActionOnRelease(Transform t)
        {
            t.gameObject.SetActive(false);
        }

        protected override Transform CreateFunc()
        {
            var go = Instantiate(_prefab, transform);

            // This is used to return the object to the pool.
            var returnToPool = go.gameObject.AddComponent<ReturnToPool>();
            returnToPool.pool = Pool;

            return go;
        }
    }
}