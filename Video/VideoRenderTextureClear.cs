using UnityEngine;
using UnityEngine.Video;

namespace SombraStudios.Shared.Video
{
    /// <summary>
    /// Fix to issue: RenderTexture retaining last video player frame
    /// This happens when using the same render texture to play another video after the previous one.
    /// Reference:
    /// https://forum.unity.com/threads/rendertexture-retaining-last-video-player-frame.498624/#post-3717181
    /// Know issue:
    /// This script may cause problem disabling OpenGL3 API, and enabling Vulkan.
    /// </summary>
    public class VideoRenderTextureClear : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _videoplayer;

        private void Awake()
        {
            RenderTexture.active = _videoplayer.targetTexture;
            GL.Clear(true, true, Color.black);
            RenderTexture.active = null;
        }
    }
}
