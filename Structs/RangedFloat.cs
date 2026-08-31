using System;
using UnityEngine;

namespace SombraStudios.Shared.Structs
{
    /// <summary>
    /// Represents a range of float values with a minimum and maximum value.
    /// </summary>
    [Serializable]
    public struct RangedFloat : IEquatable<RangedFloat>
    {
        /// <summary>
        /// The minimum value of the range.
        /// </summary>
        [Tooltip("The minimum value of the range.")]
        public float MinValue;

        /// <summary>
        /// The maximum value of the range.
        /// </summary>
        [Tooltip("The maximum value of the range.")]
        public float MaxValue;

        public RangedFloat(float minValue, float maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <summary>
        /// Returns a random value within the range [MinValue, MaxValue].
        /// </summary>
        /// <returns>A different value on each call.</returns>
        public readonly float GetRandom() => UnityEngine.Random.Range(MinValue, MaxValue);

        /// <summary>
        /// Indicates whether this range has the same bounds as another range.
        /// </summary>
        /// <param name="other">The range to compare with.</param>
        /// <returns>True if both bounds are equal, false otherwise.</returns>
        public readonly bool Equals(RangedFloat other)
            => MinValue.Equals(other.MinValue) && MaxValue.Equals(other.MaxValue);

        public readonly override bool Equals(object obj) => obj is RangedFloat other && Equals(other);

        public readonly override int GetHashCode() => HashCode.Combine(MinValue, MaxValue);

        public static bool operator ==(RangedFloat left, RangedFloat right) => left.Equals(right);

        public static bool operator !=(RangedFloat left, RangedFloat right) => !left.Equals(right);
    }
}