using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos
{
    /// <summary>
    /// Defines the interface for a Gizmo that represents a sphere in Unity.
    /// </summary>
    public interface IGizmoSphere : IGizmo
    {
        /// <summary>
        /// Gets or sets a value indicating whether the sphere is a wireframe.
        /// </summary>
        bool IsWireSphere { get; set; }
        /// <summary>
        /// Gets or sets the center of the sphere.
        /// </summary>
        Vector3 Center { get; set; }
        /// <summary>
        /// Gets or sets the radius of the sphere.
        /// </summary>
        float Radius { get; set; }
    }
}
