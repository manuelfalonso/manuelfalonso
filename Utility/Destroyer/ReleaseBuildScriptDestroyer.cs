#if DEVELOPMENT_BUILD || UNITY_EDITOR
#define DEBUG
#endif

using UnityEngine;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// Destroys specified script components if the build is not a development 
    /// build or if not running in the Unity editor.
    /// </summary>
    public class ReleaseBuildScriptDestroyer : MonoBehaviour
    {
        /// <summary>
        /// An array of script components to be destroyed in release builds.
        /// </summary>
        [Tooltip("An array of script components to be destroyed in release builds.")]
        [SerializeField] private Component[] _scriptsToDestroy; 

        private void Awake()
        {
#if !DEBUG
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
