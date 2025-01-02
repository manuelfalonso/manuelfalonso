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
        public int MinValue;

        /// <summary>
        /// The maximum value of the range.
        /// </summary>
        public int MaxValue;
        
        /// <summary>
        /// Gets a random value within the range [MinValue, MaxValue).
        /// </summary>
        public float RandomValue => UnityEngine.Random.Range(MinValue, MaxValue);
    }
}
