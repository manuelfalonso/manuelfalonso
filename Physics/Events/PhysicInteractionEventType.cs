using System;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Types of physics interaction events.
    /// </summary>
    [Flags, Serializable]
    public enum PhysicInteractionEventType
    {
        None = 0,
        Enter = 1 << 0,
        Stay = 1 << 1, 
        Exit = 1 << 2,
    }
}
