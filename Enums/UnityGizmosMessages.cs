using System;

namespace SombraStudios.Shared.Enums
{
    /// <summary>
    /// Enum representing Unity Gizmos drawing messages.
    /// </summary>
    [Flags]
    public enum UnityGizmosMessages
    {
        None = 0,
        OnDrawGizmos = 1 << 0,
        OnDrawGizmosSelected = 1 << 1
    }
}
