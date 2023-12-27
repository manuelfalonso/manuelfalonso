using UnityEngine;

namespace SombraStudios.Shared.Patterns.Creational.DependencyInjection.Example
{
    public class ExampleDependencyPlainClass
    {
        [InjectField]
        private ExampleDependencyNested dependencyNested;

        public void DoSomethingComplex()
        {
            dependencyNested.DoSomethingSimple();

            Debug.Log($"Something complex can happened in plain classes too");
        }
    }
}
