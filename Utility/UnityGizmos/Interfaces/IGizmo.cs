using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos.Interfaces
{
    /// <summary>
    /// Defines the interface for drawing a Gizmo in Unity with a Editor script.
    /// </summary>
    public interface IGizmo
    {
        /// <summary>
        /// Gets the MonoBehaviour associated with the Gizmo.
        /// </summary>
        MonoBehaviour MonoBehaviour { get; }
        /// <summary>
        /// Gets or sets a value indicating whether Gizmos are enabled.
        /// </summary>
        bool GizmosEnabled { get; set; }
        /// <summary>
        /// Gets or sets the color of the Gizmo.
        /// </summary>
        Color GizmosColor { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the Gizmo should only be shown in play mode.
        /// </summary>
        bool ShowOnlyInPlayMode { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether the Gizmo's position is local.
        /// </summary>
        bool IsLocalPosition { get; set; }
    }
}
