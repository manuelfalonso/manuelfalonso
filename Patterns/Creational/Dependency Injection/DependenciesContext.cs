using UnityEngine;

namespace SombraStudios.Shared.Patterns.Creational.DependencyInjection
{
    /// <summary>
    /// Acts as a container of dependencies.
    /// Due to the DontDestroyOnLoad, it should be as the root object.
    /// Dependent objects (with InjectField attribute) should be its children
    /// or inside this same GameObject to work properly.
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public abstract class DependenciesContext : MonoBehaviour
    {
        protected DependenciesCollection dependenciesCollection = new DependenciesCollection();
        private DependenciesProvider dependenciesProvider;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Setup();

            dependenciesProvider = new DependenciesProvider(dependenciesCollection);

            var children = GetComponentsInChildren<MonoBehaviour>(true);
            foreach (var child in children)
            {
                dependenciesProvider.Inject(child);
            }

            Configure();
        }

        /// <summary>
        /// Sets up the initial dependencies in the collection.
        /// </summary>
        protected abstract void Setup();

        /// <summary>
        /// Configures additional dependencies and behavior after injection.
        /// </summary>
        protected abstract void Configure();
    }
}
