using System;

namespace SombraStudios.Shared.Physics
{
    /// <summary>
    /// Types of collision events.
    /// </summary>
    [Serializable]
    [Flags]
    public enum CollisionEventType
    {
        None = 0,
        Enter = 1,
        Stay = 2, 
        Exit = 4
    }
}
