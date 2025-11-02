using UnityEngine;

namespace SombraStudios.Shared.Patterns.InversionOfControl.DependencyInjection.Example
{
    /// <summary>
    /// We can have all of our dependencies obtained from outside 
    /// without caring about where they are, or even if they are 
    /// singletons or not.
    /// </summary>
    public class ExampleDependant : MonoBehaviour
    {
        [InjectField]
        private ExampleDependencyMonobehaviour dependency = null;
        [InjectField]
        private ExampleDependencyPlainClass dependency2 = null;

        private void Awake()
        {
            // Without attribute and the reflection thing, you should manually register the dependcy
            //dependency = DependenciesContext.Dependencies.Get<ExampleDependencyMonobehaviour>();
            // o
            //dependency = (ExampleDependencyMonobehaviour)DependenciesContext.Dependencies.Get(dependency.GetType());

            dependency.DoSomethingComplex();

            dependency2.DoSomethingComplex();
        }
    }
}
