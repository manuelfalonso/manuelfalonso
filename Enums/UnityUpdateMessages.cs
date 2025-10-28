using System;

namespace SombraStudios.Shared.Enums
{
    [Flags, Serializable]
    /// <summary>
    /// Enum representing Unity update loop messages.
    /// </summary>
    public enum UnityUpdateMessages
    {
        None = 0,
        Update = 1 << 0,
        FixedUpdate = 1 << 1,
        LateUpdate = 1 << 2
    }
}
