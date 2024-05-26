using System;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Types of collision threshold to trigger events.
    /// </summary>
    [Serializable]
    [Flags]
    public enum CollisionThresholdType
    {
        None = 0,
        Impulse = 1,
        Velocity = 2,
    }
}
