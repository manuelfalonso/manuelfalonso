using Codice.CM.Common.Replication;
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

        /// <summary>
        /// Retrieves a component of the specified type from the given <see cref="GameObject"/>.  If the component does
        /// not exist, it is added to the <see cref="GameObject"/>.
        /// </summary>
        /// <typeparam name="T">The type of the component to retrieve or add. Must derive from <see cref="Component"/>.</typeparam>
        /// <param name="gameObject">The <see cref="GameObject"/> to retrieve the component from or add the component to.</param>
        /// <returns>The existing component of type <typeparamref name="T"/> if found; otherwise, a newly added component of type
        /// <typeparamref name="T"/>.</returns>
        public static T GetOrAdd<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.TryGetComponent<T>(out var existingComponent))
            {
                return existingComponent;
            }
            else
            {
                return gameObject.AddComponent<T>();
            }
        }
    }
}
