using System;

namespace SombraStudios.Shared.Enums
{
    [Flags]
    /// <summary>
    /// Enum representing target hands for an action.
    /// </summary>
    public enum HandTarget
    {
        /// <summary>
        /// Main hand as a target.
        /// </summary>            
        Main = 1,
        /// <summary>
        /// Off-hand as a target.
        /// </summary>
        OffHand = 2
    }
}
