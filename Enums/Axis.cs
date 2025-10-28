using System;

namespace SombraStudios.Shared.Enums
{
    /// <summary>
    /// Enum representing the three primary axes in 3D space.
    /// </summary>
    [Flags]
    public enum Axis
    {
        None = 0,
        /// <summary>
        /// The X axis.
        /// </summary>
        X = 1 << 0,
        /// <summary>
        /// The Y axis.
        /// </summary>
        Y = 1 << 1,
        /// <summary>
        /// The Z axis.
        /// </summary>
        Z = 1 << 2,
    }
}
