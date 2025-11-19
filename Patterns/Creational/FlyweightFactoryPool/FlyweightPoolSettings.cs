using SombraStudios.Shared.Extensions;
using UnityEngine;

namespace SombraStudios.Shared.Patterns.Creational.FlyweightFactoryPool
{
    /// <summary>
    /// ScriptableObject that defines the configuration settings for a Flyweight Factory Pool.
    /// This asset can be created through the Unity menu and contains all the parameters
    /// needed to manage object pooling behavior including pool size limits, validation settings,
    /// and lifecycle callbacks for pooled objects.
    /// </summary>
    [CreateAssetMenu(fileName = "FlyweightFactoryPool", menuName = "Sombra Studios/Patterns/Creational/Flyweight Factory Pool")]
    public class FlyweightPoolSettings : ScriptableObject
    {
        /// <summary>
        /// Prefab to pool.
        /// </summary>
        [Tooltip("Prefab to pool.")]
        [SerializeField] protected GameObject _prefab;

        /// <summary>
        /// Determines whether the pool should be pre-populated with instances on initialization.
        /// When true, the pool will create initial instances up to the default capacity.
        /// </summary>
        [Tooltip("Determines whether the pool should be pre-populated with instances on initialization.")]
        [SerializeField] private bool _prepopulatePool = true;

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
        [SerializeField] private int _maxSize = 100;

        public bool PopulatePool => _prepopulatePool;
        public bool CollectionCheck => _collectionCheck;
        public int DefaultCapacity => _defaultCapacity;
        public int MaxPoolSize => _maxSize;

        /// <summary>
        /// Used to create a new instance when the pool is empty.
        /// </summary>
        /// <returns>A new instance of the pooled object.</returns>
        public virtual FlyweightPooledObject CreateFunc()
        {
            var go = Instantiate(_prefab, FlyweightFactory.Instance.gameObject.transform);
            go.SetActive(false);

            var flyweight = go.GetOrAdd<FlyweightPooledObject>();
            flyweight.Settings = this;

            return flyweight;
        }

        /// <summary>
        /// Called when the instance is taken from the pool.
        /// </summary>
        /// <param name="t">The object taken from the pool.</param>
        public virtual void ActionOnGet(FlyweightPooledObject t) => t.gameObject.SetActive(true);

        /// <summary>
        /// Called when the instance is returned to the pool. This can be used to clean up or disable the instance.
        /// </summary>
        /// <param name="t">The object returned to the pool.</param>
        public virtual void ActionOnRelease(FlyweightPooledObject t) => t.gameObject.SetActive(false);

        /// <summary>
        /// Called when the element could not be returned to the pool due to the pool reaching the maximum size.
        /// </summary>
        /// <param name="t">The object to be destroyed.</param>
        public virtual void ActionOnDestroy(FlyweightPooledObject t) => Destroy(t.gameObject);
    }
}
