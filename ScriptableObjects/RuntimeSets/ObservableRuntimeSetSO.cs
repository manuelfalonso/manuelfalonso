using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.ScriptableObjects.RuntimeSets
{
    /// <summary>
    /// An abstract base class for creating observable runtime sets of ScriptableObjects.
    /// </summary>
    /// <typeparam name="T">The type of elements in the runtime set.</typeparam>
    public abstract class ObservableRuntimeSetSO<T> : ScriptableObject
    {
        /// <summary>
        /// The list of items in the runtime set.
        /// </summary>
        protected readonly List<T> _items = new();

        /// <summary>
        /// Event triggered when an item is added to the runtime set.
        /// </summary>
        public event UnityAction<T> ItemAdded;

        /// <summary>
        /// Event triggered when an item is removed from the runtime set.
        /// </summary>
        public event UnityAction<T> ItemRemoved;

        /// <summary>
        /// Adds an item to the runtime set if it is not already present.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>True if the item was added, false if it was already present.</returns>
        public virtual bool Add(T item)
        {
            if (!_items.Contains(item))
            {
                ItemAdded?.Invoke(item);
                _items.Add(item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes an item from the runtime set if it is present.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item was removed, false if it was not present.</returns>
        public virtual bool Remove(T item)
        {
            if (_items.Contains(item))
            {
                ItemRemoved?.Invoke(item);
                _items.Remove(item);
                return true;
            }
            return false;
        }
    }
}