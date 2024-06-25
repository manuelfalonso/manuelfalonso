#if DEVELOPMENT_BUILD || UNITY_EDITOR
#define DEBUG
#endif

using UnityEngine;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// Destroys the GameObject this script is attached to if the build is 
    /// not a development build or if not running in the Unity editor.
    /// </summary>
    public class ReleaseBuildGameObjectDestroyer : MonoBehaviour
    {
        private void Awake()
        {
#if !DEBUG
            // Destroy the GameObject if not in debug mode.
            Destroy(gameObject);
#endif
        }
    }
}
