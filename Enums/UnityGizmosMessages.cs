using System;

namespace SombraStudios.Shared.Enums
{
    [Flags, Serializable]
    /// <summary>
    /// Enum representing Unity Gizmos drawing messages.
    /// </summary>
    public enum UnityGizmosMessages
    {
        None = 0,
        OnDrawGizmos = 1 << 0,
        OnDrawGizmosSelected = 1 << 1
    }
}
