using System;

namespace SombraStudios.Shared.Enums
{
    /// <summary>
    /// Enum representing Unity update loop messages.
    /// </summary>
    [Flags]
    public enum UnityUpdateMessages
    {
        None = 0,
        Update = 1 << 0,
        FixedUpdate = 1 << 1,
        LateUpdate = 1 << 2
    }
}
