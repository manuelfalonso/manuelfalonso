using System;

namespace SombraStudios.Shared.Enums
{
    /// <summary>
    /// Enum representing the three primary axes in 3D space.
    /// </summary>
    [Flags]
    public enum Axis
    {
        /// <summary>
        /// The X axis.
        /// </summary>
        X = 1,

        /// <summary>
        /// The Y axis.
        /// </summary>
        Y = 2,

        /// <summary>
        /// The Z axis.
        /// </summary>
        Z = 4
    }
}
