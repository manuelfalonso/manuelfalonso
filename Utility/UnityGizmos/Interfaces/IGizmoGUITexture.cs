using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos.Interfaces
{
    /// <summary>
    /// Defines the interface for a Gizmo that represents a GUI texture in Unity.
    /// </summary>
    public interface IGizmoGUITexture : IGizmo
    {
        /// <summary>
        /// Gets or sets the screen rectangle for the GUI Texture.
        /// </summary>
        Rect GUITextureScreenRect { get; set; }
        /// <summary>
        /// Gets or sets the Texture object for the GUI Texture.
        /// </summary>
        Texture GUITextureTexture { get; set; }
        /// <summary>
        /// Gets or sets the left border size for the GUI Texture.
        /// </summary>
        int GUITextureLeftBorder { get; set; }
        /// <summary>
        /// Gets or sets the right border size for the GUI Texture.
        /// </summary>
        int GUITextureRightBorder { get; set; }
        /// <summary>
        /// Gets or sets the top border size for the GUI Texture.
        /// </summary>
        int GUITextureTopBorder { get; set; }
        /// <summary>
        /// Gets or sets the bottom border size for the GUI Texture.
        /// </summary>
        int GUITextureBottomBorder { get; set; }
        /// <summary>
        /// Gets or sets the Material object for the GUI Texture.
        /// </summary>
        Material GUITextureMaterial { get; set; }
    }
}
