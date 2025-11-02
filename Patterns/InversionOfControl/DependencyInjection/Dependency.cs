using System;

namespace SombraStudios.Shared.Patterns.InversionOfControl.DependencyInjection
{
    /// <summary>
    /// Represents a dependency to be injected into classes.
    /// Type => is what the classes will request for injection
    /// Func => function that returns an object (GameObject.Instantiate 
    /// or Resources.Load)
    /// IsSingleton => For the sake of simplicity, we’ll only consider two 
    /// lifetimes, either singleton or not singleton (same instance vs a 
    /// new instance every time is request)
    /// </summary>
    public struct Dependency
    {
        /// <summary>
        /// Gets or sets the type of the dependency.
        /// </summary>
        public Type type { get; set; }
        /// <summary>
        /// Gets or sets the factory function that returns an object.
        /// </summary>
        public DependencyFactory.Delegate Factory { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the dependency is a singleton.
        /// </summary>
        public bool IsSingleton { get; set; }
    }
}
