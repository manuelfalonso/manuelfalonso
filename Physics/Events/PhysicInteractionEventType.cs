using System;

namespace SombraStudios.Shared.Physics.Events
{
    /// <summary>
    /// Types of physics interaction events.
    /// </summary>
    [Serializable]
    [Flags]
    public enum PhysicInteractionEventType
    {
        None = 0,
        Enter = 1,
        Stay = 2, 
        Exit = 4
    }
}
