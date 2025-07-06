using System;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.ScriptableObjects.RuntimeSets
{
    /// <summary>
    /// A generic runtime set ScriptableObject that holds a list of items of type T.
    /// Provides methods to add, remove, and query items at runtime.
    /// </summary>
    /// <typeparam name="T">The type of items in the runtime set.</typeparam>
    public abstract class RuntimeSetSO<T> : ScriptableObject
    {
        protected const string LOG_CATEGORY = "[ConditionSO]";
        protected const string PROPERTIES_TITLE = "Properties";
        protected const string DEBUG_TITLE = "Debug";

        [Header(PROPERTIES_TITLE)]
        [SerializeField] protected bool _allowDuplicates;

        [Header(DEBUG_TITLE)] 
        /// <summary>
        /// Show debug logs.
        /// </summary>
        [Tooltip("Show debug logs.")]
        [SerializeField] protected bool _showLogs;

        /// <summary>
        /// Gets the number of items in the runtime set.
        /// </summary>
        public int Count => _items.Count;
        /// <summary>
        /// Gets a value indicating whether the runtime set is empty.
        /// </summary>
        public bool IsEmpty => _items.Count == 0;
        /// <summary>
        /// Gets the generic type of the runtime set.
        /// </summary>
        public Type GetGenericType => typeof(T);
        /// <summary>
        /// Returns the index of the specified item in the runtime set.
        /// </summary>
        /// <param name="item">The item to locate.</param>
        /// <returns>The zero-based index of the item if found; otherwise, -1.</returns>
        public int IndexOf(T item) => _items.IndexOf(item);
        /// <summary>
        /// Determines whether the runtime set contains a specific item.
        /// </summary>
        /// <param name="item">The item to locate.</param>
        /// <returns>True if the item is found; otherwise, false.</returns>
        public bool Contains(T item) => _items.Contains(item);
        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to get or set.</param>
        /// <returns>The item at the specified index.</returns>
        public T this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }

        /// <summary>
        /// The list of items in the runtime set.
        /// </summary>
        protected readonly List<T> _items = new();

        /// <summary>
        /// Adds an item to the runtime set.
        /// If duplicates are not allowed, the item is only added if it does not already exist.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public virtual void Add(T item)
        {
            if (_allowDuplicates)
            {
                _items.Add(item);
                return;
            }
            if (!_items.Contains(item))
            {
                _items.Add(item);
            }
            else if (_showLogs)
            {
                Debug.LogWarning($"{LOG_CATEGORY} - Attempted to add duplicate item: {item}");
            }
        }

        /// <summary>
        /// Adds an item to the runtime set if it is not already present.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>True if the item was added; otherwise, false.</returns>
        public virtual bool TryAdd(T item)
        {
            if (_allowDuplicates)
            {
                _items.Add(item);
                return true;
            }
            if (!_items.Contains(item))
            {
                _items.Add(item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the first occurrence of a specific item from the runtime set.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        public virtual void Remove(T item)
        {
            if (_items.Remove(item))
                return;

            if (_showLogs)
            {
                Debug.LogWarning($"{LOG_CATEGORY} - Attempted to remove non-existent item: {item}");
            }
        }

        /// <summary>
        /// Removes an item from the runtime set if it is present.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item was removed; otherwise, false.</returns>
        public virtual bool TryRemove(T item)
        {
            return _items.Remove(item);
        }

        /// <summary>
        /// Removes all items from the runtime set.
        /// </summary>
        public virtual void Clear()
        {
            _items.Clear();
        }
    }
}
