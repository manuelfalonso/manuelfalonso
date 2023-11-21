using UnityEngine;

namespace SombraStudios.Shared.Extensions
{
    /// <summary>
    /// Extension methods for MonoBehaviour classes
    /// </summary>
    public static class MonoBehaviourExtensions
    {
        public static void SafeInit<T>(this MonoBehaviour source, ref T component) where T : Component
        {
            if (component == null)
            {
                if (!source.TryGetComponent(out component))
                {
                    Debug.LogError("Reference variable not assigned and no component found.");
                }
            }
        }
    }
}
