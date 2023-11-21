using System;
using System.Collections.Generic;
using System.Linq;

namespace SombraStudios.Shared.Extensions
{
    /// <summary>
    /// Extension methods for IEnumerables
    /// </summary>
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> GetUniqueRandomItems<T>(this IEnumerable<T> items, int count)
        {
            if (count <= 0)
            {
                throw new ArgumentException("Count must be greater than 0");
            }

            var collection = items.ToList();
            var rng = new System.Random(DateTime.Now.Millisecond);

            if (count > collection.Count)
            {
                count = collection.Count;
            }

            for (var i = 0; i < count; i++)
            {
                var index = rng.Next(collection.Count);
                var item = collection[index];
                yield return item;
                collection.RemoveAt(index);
            }
        }
    }
}
