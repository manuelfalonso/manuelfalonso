using System;

namespace SombraStudios.Shared.Enums
{
    [Flags, Serializable]
    /// <summary>
    /// Enum representing Unity mouse interaction messages.
    /// </summary>
    public enum UnityMouseMessages
    {
        None = 0,
        OnMouseEnter = 1 << 0,
        OnMouseOver = 1 << 1,
        OnMouseExit = 1 << 2,
        OnMouseDown = 1 << 3,
        OnMouseUp = 1 << 4,
        OnMouseUpAsButton = 1 << 5
    }
}
