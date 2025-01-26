#if !DEVELOPMENT_BUILD && !UNITY_EDITOR
#define RELEASE_BUILD
#endif

using UnityEngine;

namespace SombraStudios.Shared.Utility.Destroyer
{
    /// <summary>
    /// Destroys the GameObject this script is attached to if the build is a release build.
    /// </summary>
    public class ReleaseBuildGameObjectDestroyer : MonoBehaviour
    {
        private void Awake()
        {
#if RELEASE_BUILD
            // Destroy the GameObject if not in debug mode.
            Destroy(gameObject);
#endif
        }
    }
}
