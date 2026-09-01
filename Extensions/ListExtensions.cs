using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IList{T}"/> collections. Operations that need indexed access
    /// or in-place mutation live here; sequence-only operations live in <see cref="IEnumerableExtensions"/>.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Shuffles the elements in the list using the Durstenfeld implementation of the Fisher-Yates algorithm.
        /// This method modifies the input list in-place, ensuring each permutation is equally likely, and returns the list for method chaining.
        /// Reference: http://en.wikipedia.org/wiki/Fisher-Yates_shuffle
        /// </summary>
        /// <remarks>
        /// Draws from <see cref="UnityEngine.Random"/>, so a shuffle is reproducible via
        /// <see cref="UnityEngine.Random.InitState(int)"/> and must be called from the main thread.
        /// </remarks>
        /// <param name="list">The list to be shuffled.</param>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        /// <returns>The shuffled list.</returns>
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int count = list.Count;
            while (count > 1)
            {
                --count;
                int index = Random.Range(0, count + 1);
                (list[index], list[count]) = (list[count], list[index]);
            }

            return list;
        }
    }
}
