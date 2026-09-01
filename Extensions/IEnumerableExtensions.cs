using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SombraStudios.Shared.Extensions
{
    /// <summary>  
    /// Provides extension methods for working with <see cref="IEnumerable{T}"/> collections.  
    /// </summary>  
    public static class IEnumerableExtensions
    {
        /// <summary>  
        /// Retrieves a specified number of unique random items from the given collection.  
        /// </summary>  
        /// <typeparam name="T">The type of elements in the collection.</typeparam>  
        /// <param name="items">The source collection to retrieve items from.</param>  
        /// <param name="count">The number of unique random items to retrieve.</param>  
        /// <returns>An <see cref="IEnumerable{T}"/> containing the unique random items.</returns>  
        /// <exception cref="ArgumentException">Thrown if <paramref name="count"/> is less than or equal to 0.</exception>  
        /// <remarks>
        /// Draws from <see cref="UnityEngine.Random"/>, so the selection is reproducible via
        /// <see cref="UnityEngine.Random.InitState(int)"/> and must be called from the main thread.
        /// </remarks>
        public static IEnumerable<T> GetUniqueRandomItems<T>(this IEnumerable<T> items, int count)
        {
            if (count <= 0)
                throw new ArgumentException("Count must be greater than 0", nameof(count));

            // Because the method uses yield return, the compiler transforms it into a state machine.
            // None of the method body runs until you start enumerating (foreach, .ToList(), etc.)
            // The stack trace/call site at the point of failure won't match where the bad argument was passed
            return GetUniqueRandomItemsIterator(items, count);
        }

        /// <summary>
        /// Returns an iterator that yields a specified number of unique random items from the given collection.
        /// </summary>
        /// <remarks>The method removes items from the collection as they are yielded, ensuring
        /// uniqueness. The order of the returned items is random.</remarks>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="items">The collection of items to select from. Must not be null.</param>
        /// <param name="count">The number of unique random items to yield. If the specified count exceeds the number of items in the
        /// collection, all items will be returned in random order.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that yields up to <paramref name="count"/> unique random items from the
        /// collection.</returns>
        private static IEnumerable<T> GetUniqueRandomItemsIterator<T>(IEnumerable<T> items, int count)
        {
            var collection = items.ToList();

            if (count > collection.Count)
            {
                count = collection.Count;
            }

            for (var i = 0; i < count; i++)
            {
                var index = UnityEngine.Random.Range(0, collection.Count);
                yield return collection[index];
                collection.RemoveAt(index);
            }
        }

        /// <summary>  
        /// Finds the closest <typeparamref name="T"/> object to a specified position.  
        /// </summary>  
        /// <typeparam name="T">The type of elements in the collection, which must inherit from <see cref="MonoBehaviour"/>.</typeparam>  
        /// <param name="items">The source collection to search.</param>  
        /// <param name="position">The position to compare distances against.</param>  
        /// <returns>The closest <typeparamref name="T"/> object to the specified position, or <c>null</c> if the collection is empty.</returns>  
        public static T GetClosest<T>(this IEnumerable<T> items, Vector3 position) where T : MonoBehaviour
        {
            T closest = null;
            var closestDistance = float.MaxValue;

            foreach (var item in items)
            {
                var distance = (item.transform.position - position).sqrMagnitude;

                if (distance < closestDistance)
                {
                    closest = item;
                    closestDistance = distance;
                }
            }

            return closest;
        }
    }
}
