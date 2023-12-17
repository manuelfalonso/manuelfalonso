using System;

namespace SombraStudios.Shared.Enums
{
    /// <summary>
    /// Enumeration representing different teams in a game.
    /// </summary>
    [Flags]
    public enum Team
    {
        /// <summary>
        /// Represents its own team.
        /// </summary>
        Self = 1,
        /// <summary>
        /// Represents allied teams.
        /// </summary>
        Ally = 2,
        /// <summary>
        /// Represents enemy teams.
        /// </summary>
        Enemy = 4
    }
}
