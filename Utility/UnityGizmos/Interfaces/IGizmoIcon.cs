using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos
{
    /// <summary>
    /// Defines the interface for a Gizmo that represents an icon in Unity.
    /// </summary>
    public interface IGizmoIcon : IGizmo
    {
        /// <summary>
        /// Gets or sets the center of the icon.
        /// </summary>
        Vector3 Center { get; set; }
        /// <summary>
        /// Gets or sets the name of the icon.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether scaling is allowed for the icon.
        /// </summary>
        bool AllowScaling { get; set; }
    }
}
