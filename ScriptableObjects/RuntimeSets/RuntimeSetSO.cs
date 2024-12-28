using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.ScriptableObjects.RuntimeSets
{
    /// <summary>
    /// A generic runtime set ScriptableObject that holds a list of items of type T.
    /// </summary>
    /// <typeparam name="T">The type of items in the runtime set.</typeparam>
    public abstract class RuntimeSetSO<T> : ScriptableObject
    {
        /// <summary>
        /// The list of items in the runtime set.
        /// </summary>
        private readonly List<T> _items = new();
        
        /// <summary>
        /// Get the list of items in the runtime set.
        /// </summary>
        public List<T> Items => _items;

        /// <summary>
        /// Adds an item to the runtime set if it is not already present.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>True if the item was added; otherwise, false.</returns>
        public virtual bool Add(T item)
        {
            if (!_items.Contains(item))
            {
                _items.Add(item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes an item from the runtime set if it is present.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item was removed; otherwise, false.</returns>
        public virtual bool Remove(T item)
        {
            if (_items.Contains(item))
            {
                _items.Remove(item);
                return true;
            }
            return false;
        }
    }
}
