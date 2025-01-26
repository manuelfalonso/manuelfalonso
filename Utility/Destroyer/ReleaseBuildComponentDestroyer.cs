#if !DEVELOPMENT_BUILD && !UNITY_EDITOR
#define RELEASE_BUILD
#endif

using UnityEngine;

namespace SombraStudios.Shared.Utility.Destroyer
{
    /// <summary>
    /// Destroys specified script components if the build is a release build.
    /// </summary>
    public class ReleaseBuildComponentDestroyer : MonoBehaviour
    {
        /// <summary>
        /// An array of script components to be destroyed in release builds.
        /// </summary>
        [Tooltip("An array of script components to be destroyed in release builds.")]
        [SerializeField] private Component[] _componentsToDestroy; 

        private void Awake()
        {
#if RELEASE_BUILD
            foreach (var item in _scriptsToDestroy)
            {
                if (item != null)
                {
                    // Destroy the script if not in debug mode.
                    Destroy(item);
                }
            }
            // Destroy the script if not in debug mode.
            Destroy(this);
#endif
        }
    }
}
