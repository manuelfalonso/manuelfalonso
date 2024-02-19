using UnityEngine;

namespace SombraStudios.Shared.Utility.UnityGizmos
{
    /// <summary>
    /// Defines the interface for a Gizmo that represents a cube in Unity.
    /// </summary>
    public interface IGizmoCube : IGizmo
    {
        /// <summary>
        /// Gets or sets a value indicating whether the cube is a wireframe.
        /// </summary>
        bool IsWireCube { get; set; }
        /// <summary>
        /// Gets or sets the center of the cube.
        /// </summary>
        Vector3 Center { get; set; }
        /// <summary>
        /// Gets or sets the size of the cube.
        /// </summary>
        Vector3 Size { get; set; }
    }
}
