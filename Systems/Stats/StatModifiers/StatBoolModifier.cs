namespace SombraStudios.Shared.Systems.Stats
{
    /// <summary>
    /// Represents a modifier for a boolean stat.
    /// </summary>
    public class StatBoolModifier : StatModifier<bool>
    {
        /// <summary>
        /// Constructs a new boolean stat modifier with a value and source.
        /// </summary>
        /// <param name="value">The value of the modifier.</param>
        /// <param name="source">The source of the modifier.</param>
        public StatBoolModifier(bool value, object source) : base(value, source) { }
    }
}
//EOF.