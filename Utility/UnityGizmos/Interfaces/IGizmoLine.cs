using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos
{
    /// <summary>
    /// Defines the interface for a Gizmo that represents a line in Unity.
    /// </summary>
    public interface IGizmoLine : IGizmo
    {
        /// <summary>
        /// Gets or sets the starting point of the line.
        /// </summary>
        Vector3 From { get; set; }
        /// <summary>
        /// Gets or sets the ending point of the line.
        /// </summary>
        Vector3 To { get; set; }
    }
}
