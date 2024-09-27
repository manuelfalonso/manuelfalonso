using System;
using System.Collections.Generic;
using System.Reflection;

namespace SombraStudios.Shared.Patterns.Creational.DependencyInjection
{
    /// <summary>
    /// Provides dependency injection functionality for creating and managing objects.
    /// </summary>
    public class DependenciesProvider
    {
        private Dictionary<Type, Dependency> dependencies = new Dictionary<Type, Dependency>();
        private Dictionary<Type, object> singletons = new Dictionary<Type, object>();

        /// <summary>
        /// Initializes a new instance of the DependenciesProvider class with the specified dependencies.
        /// </summary>
        /// <param name="dependencies">The collection of dependencies to be registered.</param>
        public DependenciesProvider(DependenciesCollection dependencies)
        {
            foreach (var dependency in dependencies)
            {
                this.dependencies.Add(dependency.type, dependency);
            }
        }

        /// <summary>
        /// Retrieves an instance of the specified type from the registered dependencies.
        /// </summary>
        /// <param name="type">The type of the object to retrieve.</param>
        /// <returns>An instance of the specified type.</returns>
        public object Get(Type type)
        {
            if (!dependencies.ContainsKey(type))
            {
                throw new ArgumentException("Type is not a dependent: " + type.FullName);
            }

            var dependency = dependencies[type];
            if (dependency.IsSingleton)
            {
                if (!singletons.ContainsKey(type))
                {
                    singletons.Add(type, dependency.Factory(this));
                }
                return singletons[type];
            }
            else
            {
                return dependency.Factory(this);
            }
        }

        /// <summary>
        /// Retrieves an instance of the specified generic type from the registered dependencies.
        /// </summary>
        /// <typeparam name="T">The type of the object to retrieve.</typeparam>
        /// <returns>An instance of the specified generic type.</returns>
        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        /// <summary>
        /// Injects dependencies into the specified object based on fields with the InjectField attribute.
        /// </summary>
        /// <param name="dependant">The object to inject dependencies into.</param>
        /// <returns>The object with injected dependencies.</returns>
        public object Inject(object dependant)
        {
            Type type = dependant.GetType();
            while (type != null)
            {
                var fields = type.GetFields(
                    BindingFlags.Public |
                    BindingFlags.NonPublic |
                    BindingFlags.DeclaredOnly |
                    BindingFlags.Instance);
                foreach (var field in fields)
                {
                    if (field.GetCustomAttribute<InjectFieldAttribute>(false) == null)
                        continue;

                    field.SetValue(dependant, Get(field.FieldType));
                }
                type = type.BaseType;
            }
            return dependant;
        }
    }
}
