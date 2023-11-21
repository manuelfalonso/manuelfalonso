using UnityEngine;

namespace SombraStudios.Shared.DependencyInjection
{
    /// <summary>
    /// Acts as a container of dependencies
    /// Due to the DontDestroyOnLoad it should be as root object
    /// Dependant object (with InjectField attribute) should be its 
    /// childs or inside this same GameObject to work properly
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

        protected abstract void Setup();

        protected abstract void Configure();
    }
}
