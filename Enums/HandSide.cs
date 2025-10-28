using System;

namespace SombraStudios.Shared.Enums
{
    /// <summary>
    /// Enum representing the side of a hand.
    /// </summary>
    [Flags]
    public enum HandSide
    {
        None = 0,
        /// <summary>
        /// Left hand side.
        /// </summary>
        Left = 1 << 0,
        /// <summary>
        /// Right hand side.
        /// </summary>
        Right = 1 << 1,
    }
}
