using System;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Types of collision threshold to trigger events.
    /// </summary>
    [Flags, Serializable]
    public enum CollisionThresholdType
    {
        None = 0,
        Impulse = 1 << 0,
        Velocity = 1 << 1,
    }
}
