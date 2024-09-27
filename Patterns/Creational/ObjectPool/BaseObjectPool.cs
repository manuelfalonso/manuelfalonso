using UnityEngine;
using UnityEngine.Pool;

namespace SombraStudios.Shared.Patterns.Creational.ObjectPool
{
    /// <summary>
    /// Base class for implementing an object pool for components.
    /// </summary>
    /// <typeparam name="T">The type of component to pool.</typeparam>
    public abstract class BaseObjectPool<T> : MonoBehaviour where T : Component
    {
        /// <summary>
        /// Prefab to pool.
        /// </summary>
        [Tooltip("Prefab to pool.")]
        [SerializeField] protected T _prefab;

        /// <summary>
        /// If true the pool will be setup on awake with the default capacity.
        /// </summary>
        [Tooltip("If true the pool will be setup on awake with the default capacity.")]
        [SerializeField] private bool _setupOnAwake = true;

        [Header("Object Pool Settings")]
        /// <summary>
        /// Collection checks are performed when an instance is returned back to the pool. An exception will be 
        /// thrown if the instance is already in the pool. Collection checks are only performed in the Editor.
        /// </summary>
        [Tooltip("Collection checks are performed when an instance is returned back to the pool. An " +
            "exception will be thrown if the instance is already in the pool. Collection checks are only " +
            "performed in the Editor.")]
        [SerializeField] private bool _collectionCheck = true;
        /// <summary>
        /// The default capacity the stack will be created with.
        /// </summary>
        [Tooltip("The default capacity the stack will be created with")]
        [SerializeField] private int _defaultCapacity = 10;
        /// <summary>
        /// The maximum size of the pool. When the pool reaches the max size then any further instances returned to 
        /// the pool will be ignored and can be garbage collected. This can be used to prevent the pool growing to 
        /// a very large size.
        /// </summary>
        [Tooltip("The maximum size of the pool. When the pool reaches the max size then any further instances " +
            "returned to the pool will be ignored and can be garbage collected. This can be used to prevent the " +
            "pool growing to a very large size")]
        [SerializeField] private int _maxSize = 10000;

        private IObjectPool<T> _pool;

        /// <summary>
        /// Gets the object pool instance.
        /// </summary>
        public IObjectPool<T> Pool
        {
            get
            {
                _pool ??= new ObjectPool<T>(CreateFunc,
                    ActionOnGet,
                    ActionOnRelease,
                    ActionOnDestroy,
                    _collectionCheck,
                    _defaultCapacity,
                    _maxSize);
                return _pool;
            }
        }

        private void Awake()
        {
            if (_setupOnAwake) { SetupPool(); }
        }

        /// <summary>
        /// Sets up the object pool with the default capacity.
        /// </summary>
        private void SetupPool()
        {
            _pool = new ObjectPool<T>(CreateFunc,
                ActionOnGet,
                ActionOnRelease,
                ActionOnDestroy,
                _collectionCheck,
                _defaultCapacity,
                _maxSize);

            for (int i = 0; i < _defaultCapacity; i++)
            {
                var t = CreateFunc();
                ActionOnRelease(t);
            }
        }

        /// <summary>
        /// Used to create a new instance when the pool is empty.
        /// </summary>
        /// <returns>A new instance of the pooled object.</returns>
        protected abstract T CreateFunc();

        /// <summary>
        /// Called when the instance is taken from the pool.
        /// </summary>
        /// <param name="t">The object taken from the pool.</param>
        protected abstract void ActionOnGet(T t);

        /// <summary>
        /// Called when the instance is returned to the pool. This can be used to clean up or disable the instance.
        /// </summary>
        /// <param name="t">The object returned to the pool.</param>
        protected abstract void ActionOnRelease(T t);

        /// <summary>
        /// Called when the element could not be returned to the pool due to the pool reaching the maximum size.
        /// </summary>
        /// <param name="t">The object to be destroyed.</param>
        protected abstract void ActionOnDestroy(T t);
    }
}
