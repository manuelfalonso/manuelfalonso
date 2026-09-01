using UnityEngine;

namespace SombraStudios.Shared.Extensions
{
    /// <summary>
    /// Provides extension methods for working with objects, including handling null references and destroyed Unity
    /// objects.
    /// </summary>
    /// <remarks>This class contains utility methods designed to simplify common object-related operations,
    /// particularly in Unity. It includes functionality to address Unity-specific behaviors.</remarks>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns the object itself if it exists, null otherwise.
        /// </summary>
        /// <remarks>
        /// This method helps differentiate between a null reference and a destroyed Unity object. Unity's "== null" check
        /// can incorrectly return true for destroyed objects, leading to misleading behaviour. The OrNull method use
        /// Unity's "null check", and if the object has been marked for destruction, it ensures an actual null reference is returned,
        /// aiding in correctly chaining operations and preventing NullReferenceExceptions.
        /// </remarks>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="obj">The object being checked.</param>
        /// <returns>The object itself if it exists and not destroyed, null otherwise.</returns>
        public static T OrNull<T>(this T obj) where T : Object => obj ? obj : null;
    }
}
