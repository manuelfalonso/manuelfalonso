using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Utility.Coroutines
{
    public static class CoroutineUtils
    {
        static readonly Dictionary<float, WaitForSeconds> WaitForSecondsCache = new ();

        /// <summary>
        /// Retrieves a cached <see cref="WaitForSeconds"/> instance for the specified duration, or creates a new one if
        /// not already cached.
        /// </summary>
        /// <param name="seconds">The duration, in seconds, to wait. Must be a non-negative value.</param>
        /// <returns>A <see cref="WaitForSeconds"/> instance representing the specified duration. If the duration is already
        /// cached, the cached instance is returned; otherwise, a new instance is created and cached.</returns>
        public static WaitForSeconds GetWaitForSeconds(float seconds)
        {
            if (!WaitForSecondsCache.TryGetValue(seconds, out var wait))
            {
                wait = new WaitForSeconds(seconds);
                WaitForSecondsCache[seconds] = wait;
            }
            return wait;
        }
    }
}
