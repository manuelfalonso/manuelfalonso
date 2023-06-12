using System;

namespace SombraStudios.DependencyInjection
{
    /// <summary>
    /// Type => is what the classes will request for injection
    /// Func => function that returns an object (GameObject.Instantiate 
    /// or Resources.Load)
    /// IsSingleton => For the sake of simplicity, we’ll only consider two 
    /// lifetimes, either singleton or not singleton (same instance vs a 
    /// new instance every time is request)
    /// </summary>
    public struct Dependency
    {
        public Type type { get; set; }
        public DependencyFactory.Delegate Factory { get; set; }
        public bool IsSingleton { get; set; }
    }
}
