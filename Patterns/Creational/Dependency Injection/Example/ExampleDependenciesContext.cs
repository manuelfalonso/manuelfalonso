using UnityEngine;

namespace SombraStudios.Shared.Patterns.Creational.DependencyInjection.Example
{
    /// <summary>
    /// MonoBehaviour in the scene
    /// </summary>
    public class ExampleDependenciesContext : DependenciesContext
    {
        [SerializeField]
        private ExampleDependencyMonobehaviour exampleDependency;
        [SerializeField]
        private ExampleDependencyNested exampleDependencyNested;

        protected override void Setup()
        {
            dependenciesCollection.Add(new Dependency 
            { 
                type = typeof(ExampleDependencyMonobehaviour),
                Factory = DependencyFactory.FromGameObject(exampleDependency),
                IsSingleton = true
            });

            dependenciesCollection.Add(new Dependency
            {
                type = typeof (ExampleDependencyPlainClass),
                Factory = DependencyFactory.FromClass<ExampleDependencyPlainClass>(),
                IsSingleton = false
            });

            dependenciesCollection.Add(new Dependency
            {
                type = typeof(ExampleDependencyNested),
                Factory = DependencyFactory.FromPrefab(exampleDependencyNested),
                IsSingleton = true
            });
        }

        protected override void Configure()
        {

        }
    }
}
