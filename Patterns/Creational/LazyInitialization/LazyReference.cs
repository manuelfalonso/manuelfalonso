using System;

namespace SombraStudios.Shared.Patterns.Creational.LazyInitialization
{
    /// <summary>  
    /// A generic class for lazy initialization of UnityEngine.Object types.  
    /// The value is only initialized when accessed for the first time.  
    /// </summary>  
    /// <typeparam name="T">The type of UnityEngine.Object to initialize lazily.</typeparam>  
    public class LazyReference<T> where T : UnityEngine.Object
    {
        /// <summary>  
        /// Indicates whether the value has been initialized.  
        /// </summary>  
        public bool IsInitialized => _value != null;

        /// <summary>  
        /// Gets the lazily initialized value.  
        /// If the value is not already initialized, it will be initialized using the provided initializer function.  
        /// </summary>  
        public T Value => _value ??= _initializer();

        private T _value;
        private readonly Func<T> _initializer;

        /// <summary>  
        /// Initializes a new instance of the <see cref="LazyReference{T}"/> class.  
        /// </summary>  
        /// <param name="initializer">  
        /// A function that initializes the value when it is accessed for the first time.  
        /// </param>  
        /// <exception cref="ArgumentNullException">Thrown if the initializer function is null.</exception>  
        public LazyReference(Func<T> initializer)
        {
            _initializer = initializer ?? throw new ArgumentNullException(nameof(initializer));
        }
    }
}
