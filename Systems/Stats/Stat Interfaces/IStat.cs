namespace SombraStudios.Shared.Systems.Stats
{
    /// <summary>
    /// Represents a stat with a base value and a potentially modified value.
    /// </summary>
    public interface IStat<T> : IBaseStat
    {
        /// <summary>
        /// Gets the value of the stat, considering any modifiers.
        /// </summary>
        T GetValue();
        /// <summary>
        /// Gets the base value of the stat, without considering any modifiers.
        /// </summary>
        T GetBaseValue();
    }
}
//EOF.