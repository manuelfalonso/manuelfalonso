using UnityEngine;

namespace SombraStudios.Shared.Utility.Destroyer
{
    /// <summary>
    /// Destroys on Awake the scripts specified depending on platform scripting symbols like
    /// UNITY_EDITOR, UNITY_STANDALONE, UNITY_ANDROID, etc.
    /// </summary>
    public class ComponentDestroyer : MonoBehaviour
    {
        [Tooltip("An array of script components to be destroyed.")]
        [SerializeField] private Component[] _componentsToDestroy; 
        
        [Header("Platform selection")]
        [Tooltip("Destroy the GameObject if running in the Unity editor.")]
        [SerializeField] private bool _destroyInEditor;

        [Tooltip("Destroy the GameObject if running on a standalone platform (Mac OS X, Windows or Linux).")]
#pragma warning disable CS0414 // Disable warning about unused fields
        [SerializeField] private bool _destroyOnStandalone;

        [Tooltip("Destroy the GameObject if running on an Android platform.")] 
        [SerializeField] private bool _destroyOnAndroid;

        [Tooltip("Destroy the GameObject if running on an iOS platform.")] 
        [SerializeField] private bool _destroyOnIOS;

        [Tooltip("Destroy the GameObject if running on a WebGL platform.")] 
        [SerializeField] private bool _destroyOnWebGL;

        [Header("Others")] 
        [Tooltip("Destroy the GameObject if running on a development build.")] 
        [SerializeField] private bool _destroyOnDevelopmentBuild;
#pragma warning restore CS0414 // Re-enable warning for unused fields
        
        private void Awake()
        {
            if (ShouldDestroy())
            {
                foreach (var item in _componentsToDestroy)
                {
                    if (item != null)
                    {
                        Destroy(item);
                    }
                }
            }
        }
        
        private bool ShouldDestroy()
        {
#if DEVELOPMENT_BUILD
            if (_destroyOnDevelopmentBuild)
            {
                return true;
            }
#endif

#if UNITY_EDITOR
            if (_destroyInEditor)
            {
                return true;
            }
#endif

#if UNITY_STANDALONE
            if (_destroyOnStandalone)
            {
                return true;
            }
#endif

#if UNITY_ANDROID
            if (_destroyOnAndroid)
            {
                return true;
            }
#endif

#if UNITY_IOS
            if (_destroyOnIOS)
            {
                return true;
            }
#endif

#if UNITY_WEBGL
            if (_destroyOnWebGL)
            {
                return true;
            }
#endif

            return false;
        }
    }
}
