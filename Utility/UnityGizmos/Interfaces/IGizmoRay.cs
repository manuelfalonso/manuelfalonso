using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos.Interfaces
{
    /// <summary>
    /// Defines the interface for a Gizmo that represents a ray in Unity.
    /// </summary>
    public interface IGizmoRay : IGizmo
    {
        /// <summary>
        /// Gets or sets the starting point of the ray.
        /// </summary>
        Vector3 From { get; set; }
        /// <summary>
        /// Gets or sets the direction of the ray.
        /// </summary>
        Vector3 Direction { get; set; }
    }
}
