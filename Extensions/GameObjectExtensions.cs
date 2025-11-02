using UnityEngine;

namespace SombraStudios.Shared.Extensions
{
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Hides the specified <see cref="GameObject"/> from the Unity Hierarchy view.
        /// </summary>
        /// <remarks>This method sets the <see cref="GameObject.hideFlags"/> property to <see
        /// cref="HideFlags.HideInHierarchy"/>, making the object invisible in the Unity Hierarchy view. The object
        /// remains active in the scene and can still be accessed programmatically.</remarks>
        /// <param name="gameObject">The <see cref="GameObject"/> to hide. Must not be <see langword="null"/>.</param>
        public static void HideInHierarchy(this GameObject gameObject)
        {
            if (gameObject != null)
            {
                gameObject.hideFlags = HideFlags.HideInHierarchy;
            }
        }

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
