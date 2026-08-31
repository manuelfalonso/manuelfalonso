using System;
using UnityEngine;

namespace SombraStudios.Shared.Structs
{
    /// <summary>
    /// Represents a range of integer values with a minimum and maximum value.
    /// </summary>
    [Serializable]
    public struct RangedInt : IEquatable<RangedInt>
    {
        /// <summary>
        /// The minimum value of the range.
        /// </summary>
        [Tooltip("The minimum value of the range.")]
        public int MinValue;

        /// <summary>
        /// The maximum value of the range.
        /// </summary>
        [Tooltip("The maximum value of the range.")]
        public int MaxValue;

        public RangedInt(int minValue, int maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <summary>
        /// Returns a random value within the inclusive range [MinValue, MaxValue].
        /// </summary>
        /// <returns>A different value on each call.</returns>
        public readonly int GetRandom() => UnityEngine.Random.Range(MinValue, MaxValue + 1);

        /// <summary>
        /// Indicates whether this range has the same bounds as another range.
        /// </summary>
        /// <param name="other">The range to compare with.</param>
        /// <returns>True if both bounds are equal, false otherwise.</returns>
        public readonly bool Equals(RangedInt other)
            => MinValue == other.MinValue && MaxValue == other.MaxValue;

        public readonly override bool Equals(object obj) => obj is RangedInt other && Equals(other);

        public readonly override int GetHashCode() => HashCode.Combine(MinValue, MaxValue);

        public static bool operator ==(RangedInt left, RangedInt right) => left.Equals(right);

        public static bool operator !=(RangedInt left, RangedInt right) => !left.Equals(right);
    }
}