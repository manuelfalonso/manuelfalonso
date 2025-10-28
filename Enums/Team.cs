using System;

namespace SombraStudios.Shared.Enums
{
    /// <summary>
    /// Enumeration representing different teams in a game.
    /// </summary>
    [Flags]
    public enum Team
    {
        None = 0,
        /// <summary>
        /// Represents its own team.
        /// </summary>
        Self = 1 << 0,
        /// <summary>
        /// Represents allied teams.
        /// </summary>
        Ally = 1 << 1,
        /// <summary>
        /// Represents enemy teams.
        /// </summary>
        Enemy = 1 << 2
    }
}
