using UnityEngine;

namespace SombraStudios.Shared.Structs
{
    /// <summary>
    /// Represents a range of integer values with a minimum and maximum value.
    /// </summary>
    public struct RangedInt
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

        /// <summary>
        /// Gets a random value within the range MinValue and MaxValue.
        /// </summary>
        public readonly float RandomValue => Random.Range(MinValue, MaxValue + 1);
    }
}
