using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace SombraStudios.Shared.Patterns.InversionOfControl.DependencyInjection
{
    /// <summary>
    /// Factory class for creating instances using dependency injection.
    /// </summary>
    public static class DependencyFactory
    {
        /// <summary>
        /// Delegate representing a function that creates an object with dependencies.
        /// </summary>
        public delegate object Delegate(DependenciesProvider dependencies);

        /// <summary>
        /// Creates a delegate to instantiate a class using a default constructor and injecting dependencies.
        /// </summary>
        /// <typeparam name="T">The type of the class.</typeparam>
        /// <returns>A delegate for creating instances of the specified class.</returns>
        public static Delegate FromClass<T>() where T : class, new()
        {
            return (dependencies) =>
            {
                var type = typeof(T);
                var obj = FormatterServices.GetUninitializedObject(type);

                dependencies.Inject(obj);

                type.GetConstructor(Type.EmptyTypes).Invoke(obj, null);

                return (T)obj;
            };
        }

        /// <summary>
        /// Creates a delegate to instantiate a prefab, inject dependencies into all MonoBehaviours, and return a specified type.
        /// </summary>
        /// <typeparam name="T">The type of MonoBehaviour in the prefab.</typeparam>
        /// <param name="prefab">The prefab to instantiate.</param>
        /// <returns>A delegate for creating instances of the specified MonoBehaviour type.</returns>
        public static Delegate FromPrefab<T>(T prefab) where T : MonoBehaviour
        {
            return (dependencies) =>
            {
                bool wasActive = prefab.gameObject.activeSelf;
                prefab.gameObject.SetActive(false);
                var instance = GameObject.Instantiate(prefab);
                prefab.gameObject.SetActive(wasActive);
                var childs = 
                    instance.GetComponentsInChildren<MonoBehaviour>(true);
                foreach (var child in childs)
                {
                    dependencies.Inject(child);
                }
                instance.gameObject.SetActive(wasActive);
                return instance.GetComponent<T>();
            };
        }

        /// <summary>
        /// Creates a delegate to inject dependencies into an existing GameObject and return a specified type.
        /// </summary>
        /// <typeparam name="T">The type of MonoBehaviour in the GameObject.</typeparam>
        /// <param name="instance">The existing GameObject to inject dependencies into.</param>
        /// <returns>A delegate for injecting dependencies into the specified GameObject and returning the specified type.</returns>
        public static Delegate FromGameObject<T>(T instance) where T : MonoBehaviour
        {
            return (dependencies) =>
            {
                var childs = 
                    instance.GetComponentsInChildren<MonoBehaviour>(true);
                foreach (var child in childs)
                {
                    dependencies.Inject(child);
                }
                return instance;
            };
        }
    }
}
