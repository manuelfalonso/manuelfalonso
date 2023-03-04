using System.Collections.Generic;
using System.Collections;

namespace DependencyInjection
{

    public class DependenciesCollection : IEnumerable<Dependency>
    {
        private List<Dependency> dependencies = new List<Dependency>();

        // Register dependencies
        public void Add(Dependency dependency) => dependencies.Add(dependency);

        public IEnumerator<Dependency> GetEnumerator() => dependencies.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => dependencies.GetEnumerator();
    }
}
