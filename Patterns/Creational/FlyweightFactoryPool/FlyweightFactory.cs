using SombraStudios.Shared.Patterns.Creational.Singleton;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace SombraStudios.Shared.Patterns.Creational.FlyweightFactoryPool
{
    /// <summary>
    /// Factory singleton that manages object pools for flyweight objects.
    /// Provides centralized pooling management with automatic preloading and efficient object reuse.
    /// </summary>
    public class FlyweightFactory : PersistentSingleton<FlyweightFactory>
    {
        [Header("Pool Configuration")]
        [SerializeField] private List<FlyweightPoolSettings> _settingsToPreload = new ();

        private readonly Dictionary<FlyweightPoolSettings, IObjectPool<FlyweightPooledObject>> _pools = new();

        /// <summary>
        /// Spawns an object from the pool associated with the specified settings.
        /// </summary>
        /// <param name="settings">The pool settings to use.</param>
        /// <returns>A pooled object instance, or null if settings are invalid.</returns>
        public static FlyweightPooledObject Spawn(FlyweightPoolSettings settings)
        {
            var pool = Instance.GetPoolFor(settings);
            return pool?.Get();
        }

        /// <summary>
        /// Returns an object to its associated pool.
        /// </summary>
        /// <param name="flyweightObject">The object to return to the pool.</param>
        public static void ReturnToPool(FlyweightPooledObject flyweightObject)
        {
            if (flyweightObject?.Settings == null) return;
            var pool = Instance.GetPoolFor(flyweightObject.Settings);
            pool?.Release(flyweightObject);
        }

        private void Start()
        {
            PreloadPools();
        }

        private void PreloadPools()
        {
            var settingCount = _settingsToPreload.Count;
            for (int i = 0; i < settingCount; i++)
            {
                var settings = _settingsToPreload[i];
                if (settings != null && settings.PopulatePool)
                {
                    GetPoolFor(settings);
                }
            }
        }

        private IObjectPool<FlyweightPooledObject> GetPoolFor(FlyweightPoolSettings settings)
        {
            if (settings == null)
            {
                Debug.LogError("FlyweightFactory: Settings provided is null. Cannot get or create pool.", this);
                return null;
            }

            if (_pools.TryGetValue(settings, out var existingPool)) return existingPool;

            var newPool = new ObjectPool<FlyweightPooledObject>(
                settings.CreateFunc,
                settings.ActionOnGet,
                settings.ActionOnRelease,
                settings.ActionOnDestroy,
                settings.CollectionCheck,
                settings.DefaultCapacity,
                settings.MaxPoolSize
            );

            if (settings.PopulatePool)
            {
                PopulatePool(settings, newPool);
            }

            _pools.Add(settings, newPool);
            return newPool;
        }

        private static void PopulatePool(FlyweightPoolSettings settings, IObjectPool<FlyweightPooledObject> pool)
        {
            var capacity = settings.DefaultCapacity;
            for (int i = 0; i < capacity; i++)
            {
                var pooledObject = settings.CreateFunc();
                settings.ActionOnRelease(pooledObject);
                pool.Release(pooledObject);
            }
        }
    }
}
