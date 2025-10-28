using System;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Utility.ReferenceBinder
{
    /// <summary>
    /// Utility for managing and efficiently accessing component references.
    /// Allows storing a list of components and accessing them by type using an internal dictionary.
    /// </summary>
    public class ReferenceBinder : MonoBehaviour
    {
        /// <summary>
        /// List of components that will be indexed by type.
        /// </summary>
        [SerializeField] private List<Component> _references = new();

        private Dictionary<Type, List<Component>> _referenceDictionary = new();        
        private bool _isDictionaryBuilt;

#if UNITY_EDITOR
        private void OnValidate()
        {
            // It rebuilds the dictionary in the editor when changes are made
            _isDictionaryBuilt = false;
        }
#endif

        private void Awake()
        {
            EnsureDictionaryBuilt();
        }

        /// <summary>
        /// Ensures the dictionary is built. If it's not, builds it.
        /// Uses lazy loading to improve performance.
        /// </summary>
        private void EnsureDictionaryBuilt()
        {
            if (!_isDictionaryBuilt)
            {
                BuildDictionary();
                _isDictionaryBuilt = true;
            }
        }

        /// <summary>
        /// Builds the internal dictionary that maps component types to their instances.
        /// Optimized to avoid unnecessary allocations.
        /// </summary>
        private void BuildDictionary()
        {
            if (_referenceDictionary == null)
            {
                _referenceDictionary = new Dictionary<Type, List<Component>>(_references.Count);
            }
            else
            {
                _referenceDictionary.Clear();
            }

            for (int i = 0; i < _references.Count; i++)
            {
                var reference = _references[i];
                if (reference == null)
                    continue;

                var type = reference.GetType();
                if (!_referenceDictionary.TryGetValue(type, out var list))
                {
                    list = new List<Component>();
                    _referenceDictionary[type] = list;
                }
                list.Add(reference);
            }
        }

        /// <summary>
        /// Attempts to get all references of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of component to search for.</typeparam>
        /// <param name="references">List of components of the specified type if found, null otherwise.</param>
        /// <returns>True if components of the specified type were found, false otherwise.</returns>
        public bool TryGetReferences<T>(out List<T> references) where T : Component
        {
            EnsureDictionaryBuilt();

            var type = typeof(T);
            if (_referenceDictionary.TryGetValue(type, out var components) && components.Count > 0)
            {
                references = new List<T>(components.Count);
                for (int i = 0; i < components.Count; i++)
                {
                    if (components[i] is T typedComponent)
                    {
                        references.Add(typedComponent);
                    }
                }
                return references.Count > 0;
            }
            references = null;
            return false;
        }

        /// <summary>
        /// Gets the first reference of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of component to search for.</typeparam>
        /// <returns>The first instance of the component of the specified type, null if not found.</returns>
        public T GetReference<T>() where T : Component
        {
            EnsureDictionaryBuilt();

            return _referenceDictionary.TryGetValue(typeof(T), out var list) && list.Count > 0
                ? list[0] as T
                : null;
        }

        /// <summary>
        /// Attempts to get the first reference of a specific type.
        /// </summary>
        /// <typeparam name="T">The type of component to search for.</typeparam>
        /// <param name="reference">The first instance of the component of the specified type if found, null otherwise.</param>
        /// <returns>True if a component of the specified type was found, false otherwise.</returns>
        public bool TryGetReference<T>(out T reference) where T : Component
        {
            EnsureDictionaryBuilt();

            var type = typeof(T);
            if (_referenceDictionary.TryGetValue(type, out var list) && list.Count > 0)
            {
                reference = list[0] as T;
                return reference != null;
            }
            reference = null;
            return false;
        }
    }
}
