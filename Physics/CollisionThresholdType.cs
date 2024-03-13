using System;

namespace SombraStudios.Shared.Physics
{
    /// <summary>
    /// Types of collision threshold to trigger events.
    /// </summary>
    [Serializable]
    public enum CollisionThresholdType
    {
        Impulse, Velocity, ImpulseOrVelocity
    }
}
