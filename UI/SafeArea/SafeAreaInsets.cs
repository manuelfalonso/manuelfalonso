using System;

namespace SombraStudios.Shared.UI.SafeArea
{
    /// <summary>
    /// Distance each edge of a container must move inwards to sit inside the safe area.
    /// Units match the rects the insets were calculated from (screen pixels, panel units...).
    /// </summary>
    public readonly struct SafeAreaInsets : IEquatable<SafeAreaInsets>
    {
        /// <summary>
        /// Insets of zero on every edge.
        /// </summary>
        public static readonly SafeAreaInsets Zero = default;

        /// <summary>
        /// Inset from the left edge.
        /// </summary>
        public float Left { get; }

        /// <summary>
        /// Inset from the right edge.
        /// </summary>
        public float Right { get; }

        /// <summary>
        /// Inset from the top edge.
        /// </summary>
        public float Top { get; }

        /// <summary>
        /// Inset from the bottom edge.
        /// </summary>
        public float Bottom { get; }

        /// <summary>
        /// Creates a set of insets.
        /// </summary>
        public SafeAreaInsets(float left, float right, float top, float bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        /// <inheritdoc />
        public bool Equals(SafeAreaInsets other)
        {
            return Left.Equals(other.Left) && Right.Equals(other.Right)
                && Top.Equals(other.Top) && Bottom.Equals(other.Bottom);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is SafeAreaInsets other && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(Left, Right, Top, Bottom);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"(Left: {Left}, Right: {Right}, Top: {Top}, Bottom: {Bottom})";
        }
    }
}
