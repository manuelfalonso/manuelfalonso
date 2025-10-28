using System;

namespace SombraStudios.Shared.Enums
{
    /// <summary>
    /// Enum representing target hands for an action.
    /// </summary>
    [Flags]
    public enum HandTarget
    {
        None = 0,
        /// <summary>
        /// Main hand as a target.
        /// </summary>            
        Main = 1 << 0,
        /// <summary>
        /// Off-hand as a target.
        /// </summary>
        OffHand = 1 << 1,
    }
}
