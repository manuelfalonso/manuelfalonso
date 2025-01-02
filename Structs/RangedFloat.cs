using System;

namespace SombraStudios.Shared.Structs
{
    /// <summary>
    /// Represents a range of float values with a minimum and maximum value.
    /// </summary>
    [Serializable]
    public struct RangedFloat
    {
        /// <summary>
        /// The minimum value of the range.
        /// </summary>
        public float MinValue;

        /// <summary>
        /// The maximum value of the range.
        /// </summary>
        public float MaxValue;
        
        /// <summary>
        /// Gets a random value within the range [MinValue, MaxValue].
        /// </summary>
        public float RandomValue => UnityEngine.Random.Range(MinValue, MaxValue);
    }
}
