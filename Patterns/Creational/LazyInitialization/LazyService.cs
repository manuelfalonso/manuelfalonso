namespace SombraStudios.Shared.Patterns.Creational.LazyInitialization
{
    /// <summary>  
    /// Provides a lazy initialization mechanism for UnityEngine.Object types.  
    /// Ensures that the instance is only created or retrieved when accessed for the first time.  
    /// </summary>  
    /// <typeparam name="T">The type of UnityEngine.Object to initialize lazily.</typeparam>  
    public class LazyService<T> where T : UnityEngine.Object
    {
        /// <summary>  
        /// Holds the cached instance of the object.  
        /// </summary>  
        private static T _instance;

        /// <summary>  
        /// Gets the lazily initialized instance of the object.  
        /// If the instance is not already initialized, it will be retrieved using Unity's FindFirstObjectByType method.  
        /// </summary>  
        public static T Instance => _instance != null
            ? _instance
            : _instance = UnityEngine.Object.FindFirstObjectByType<T>();
    }
}
