using UnityEngine;

namespace SombraStudios.Shared.Utility.Destroyer
{
    /// <summary>
    /// Destroys on Awake the GameObject this script is attached to depending on platform scripting symbols like
    /// UNITY_EDITOR, UNITY_STANDALONE, UNITY_ANDROID, etc.
    /// </summary>
    public class GameObjectDestroyer : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("Platform selection")]
        [Tooltip("Destroy the GameObject if running in the Unity editor.")]
        [SerializeField]
        private bool _destroyInEditor = false;
#endif

#if UNITY_STANDALONE
        [Tooltip("Destroy the GameObject if running on a standalone platform (Mac OS X, Windows or Linux).")]
        [SerializeField]
        private bool _destroyOnStandalone = false;
#endif

#if UNITY_ANDROID
        [Tooltip("Destroy the GameObject if running on an Android platform.")] [SerializeField]
        private bool _destroyOnAndroid = false;
#endif

#if UNITY_IOS
        [Tooltip("Destroy the GameObject if running on an iOS platform.")] [SerializeField]
        private bool _destroyOnIOS = false;
#endif

#if UNITY_WEBGL
        [Tooltip("Destroy the GameObject if running on a WebGL platform.")] [SerializeField]
        private bool _destroyOnWebGL = false;
#endif

#if DEVELOPMENT_BUILD
        [Header("Others")] [Tooltip("Destroy the GameObject if running on a development build.")] [SerializeField]
        private bool _destroyOnDevelopmentBuild = false;
#endif

        private void Awake()
        {
            if (ShouldDestroy())
            {
                Destroy(gameObject);
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