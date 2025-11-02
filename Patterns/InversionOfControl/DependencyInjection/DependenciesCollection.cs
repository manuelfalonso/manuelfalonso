using System.Collections.Generic;
using System.Collections;

namespace SombraStudios.Shared.Patterns.InversionOfControl.DependencyInjection
{
    /// <summary>
    /// Collection of dependencies used by the DependenciesProvider.
    /// </summary>
    public class DependenciesCollection : IEnumerable<Dependency>
    {
        private List<Dependency> dependencies = new List<Dependency>();

        /// <summary>
        /// Adds a dependency to the collection.
        /// </summary>
        /// <param name="dependency">The dependency to add.</param>
        public void Add(Dependency dependency) => dependencies.Add(dependency);

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<Dependency> GetEnumerator() => dependencies.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => dependencies.GetEnumerator();
    }
}
