namespace SombraStudios.Shared.Utility.UnityGizmos.Interfaces
{
    /// <summary>
    /// Defines the interface for a Gizmo that represents a grid in Unity.
    /// </summary>
    public interface IGizmoGrid : IGizmo
    {
        /// <summary>
        /// Gets or sets the width of the grid.
        /// </summary>
        int Width { get; set; }
        /// <summary>
        /// Gets or sets the height of the grid.
        /// </summary>
        int Height { get; set; }
    }
}
