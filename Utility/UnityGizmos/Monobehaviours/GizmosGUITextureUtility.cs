using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos
{
    public class GizmosGUITextureUtility : GizmosUtility
    {
        [Header("GUI Texture")]
        [Tooltip("The size and position of the texture on the \"screen\" defined by the XY plane.")]
        [SerializeField] private Rect _gUITextureScreenRect;
        [Tooltip("The texture to be displayed.")]
        [SerializeField] private Texture _gUITextureTexture;
        [Tooltip("Inset from the rectangle's left edge.")]
        [SerializeField] private int _gUITextureLeftBorder = 0;
        [Tooltip("Inset from the rectangle's right edge.")]
        [SerializeField] private int _gUITextureRightBorder = 0;
        [Tooltip("Inset from the rectangle's top edge.")]
        [SerializeField] private int _gUITextureTopBorder = 0;
        [Tooltip("Inset from the rectangle's bottom edge.")]
        [SerializeField] private int _gUITextureBottomBorder = 0;
        [Tooltip("An optional material to apply the texture.")]
        [SerializeField] private Material _gUITextureMaterial;


        protected override void DrawGizmo()
        {
            DrawGUITexture();
        }


        private void DrawGUITexture()
        {
            Gizmos.DrawGUITexture(
                _gUITextureScreenRect,
                _gUITextureTexture,
                _gUITextureLeftBorder,
                _gUITextureRightBorder,
                _gUITextureTopBorder,
                _gUITextureBottomBorder,
                _gUITextureMaterial);
        }
    }
}
