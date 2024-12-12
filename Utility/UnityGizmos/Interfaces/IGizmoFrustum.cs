using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos
{
    /// <summary>
    /// Defines the interface for a Gizmo that represents a frustum in Unity.
    /// </summary>
    public interface IGizmoFrustum : IGizmo
    {
        /// <summary>
        /// Gets or sets the center of the frustum.
        /// </summary>
        Vector3 FrustumCenter { get; set; }
        /// <summary>
        /// Gets or sets the field of view of the frustum.
        /// </summary>
        float FrustumFov { get; set; }
        /// <summary>
        /// Gets or sets the maximum range of the frustum.
        /// </summary>
        float FrustumMaxRange { get; set; }
        /// <summary>
        /// Gets or sets the minimum range of the frustum.
        /// </summary>
        float FrustumMinRange { get; set; }
        /// <summary>
        /// Gets or sets the aspect ratio of the frustum.
        /// </summary>
        float FrustumAspect { get; set; }
    }
}
