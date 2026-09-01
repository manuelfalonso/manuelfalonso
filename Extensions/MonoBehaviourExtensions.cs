using UnityEngine;

namespace SombraStudios.Shared.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="MonoBehaviour"/> classes.
    /// </summary>
    public static class MonoBehaviourExtensions
    {
        /// <summary>
        /// Ensures a component reference is assigned, falling back to a lookup on the source's own
        /// <see cref="GameObject"/> when the field is still null.
        /// </summary>
        /// <remarks>
        /// An already-assigned reference is left untouched, so a value wired up in the Inspector always
        /// wins over the lookup. Logs an error when neither source provides the component, since the
        /// common case is a caller that cannot continue without it.
        /// </remarks>
        /// <typeparam name="T">The component type to resolve.</typeparam>
        /// <param name="source">The behaviour whose <see cref="GameObject"/> is searched.</param>
        /// <param name="component">The field to fill in; respected if it is already assigned.</param>
        /// <returns>True if <paramref name="component"/> is assigned once the call returns; otherwise, false.</returns>
        public static bool EnsureComponent<T>(this MonoBehaviour source, ref T component) where T : Component
        {
            if (component != null)
            {
                return true;
            }

            if (source.TryGetComponent(out component))
            {
                return true;
            }

            Debug.LogError($"{source.GetType().Name} has no {typeof(T).Name} assigned and none was found on '{source.name}'.", source);
            return false;
        }
    }
}