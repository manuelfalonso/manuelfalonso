using System;

namespace SombraStudios.Shared.UI.SafeArea
{
    /// <summary>
    /// Edges of a UI container that are pushed inside the device safe area.
    /// </summary>
    [Flags]
    public enum SafeAreaEdges
    {
        // Decimal                     // Binary
        None = 0,                      // 0000
        Left = 1,                      // 0001
        Right = 2,                     // 0010
        Top = 4,                       // 0100
        Bottom = 8,                    // 1000

        Horizontal = Left | Right,     // 0011
        Vertical = Top | Bottom,       // 1100
        All = Horizontal | Vertical    // 1111
    }
}
