using UnityEngine;

namespace SombraStudios.Shared.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="GameObject"/>.
    /// </summary>
    public static class GameObjectExtensions
    {
        /// <summary>
        /// Hides the specified <see cref="GameObject"/> from the Unity Hierarchy view.
        /// </summary>
        /// <remarks>
        /// Adds <see cref="HideFlags.HideInHierarchy"/> to <see cref="Object.hideFlags"/> without disturbing
        /// any other flag the object already carries, such as <see cref="HideFlags.DontSave"/>. The object
        /// remains active in the scene and can still be accessed programmatically. Does nothing if
        /// <paramref name="gameObject"/> is null.
        /// </remarks>
        /// <param name="gameObject">The <see cref="GameObject"/> to hide.</param>
        public static void HideInHierarchy(this GameObject gameObject)
        {
            if (gameObject != null)
            {
                gameObject.hideFlags |= HideFlags.HideInHierarchy;
            }
        }

        /// <summary>
        /// Reveals a <see cref="GameObject"/> previously hidden by <see cref="HideInHierarchy"/>.
        /// </summary>
        /// <remarks>
        /// Clears only <see cref="HideFlags.HideInHierarchy"/> and leaves every other flag intact. Does
        /// nothing if <paramref name="gameObject"/> is null.
        /// </remarks>
        /// <param name="gameObject">The <see cref="GameObject"/> to reveal.</param>
        public static void ShowInHierarchy(this GameObject gameObject)
        {
            if (gameObject != null)
            {
                gameObject.hideFlags &= ~HideFlags.HideInHierarchy;
            }
        }

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
