using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos.Interfaces
{
    /// <summary>
    /// Defines the interface for a Gizmo that represents a mesh in Unity.
    /// </summary>
    public interface IGizmoMesh : IGizmo
    {
        /// <summary>
        /// Gets or sets a value indicating whether the mesh is a wireframe.
        /// </summary>
        bool IsWireMesh { get; set; }
        /// <summary>
        /// Gets or sets the Mesh object.
        /// </summary>
        Mesh Mesh { get; set; }
        /// <summary>
        /// Gets or sets the position of the mesh.
        /// </summary>
        Vector3 Position { get; set; }
        /// <summary>
        /// Gets or sets the rotation of the mesh.
        /// </summary>
        Quaternion Rotation { get; set; }
        /// <summary>
        /// Gets or sets the scale of the mesh.
        /// </summary>
        Vector3 Scale { get; set; }
    }
}
