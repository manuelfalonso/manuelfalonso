using System;

namespace SombraStudios.Shared.Systems.Stats
{
    /// <summary>
    /// Factory class to create stat instances based on the type of the stat data.
    /// </summary>
    public static class StatFactory
    {
        /// <summary>
        /// Creates a new stat instance based on the provided stat data.
        /// </summary>
        /// <param name="statData">The data to use for creating the stat.</param>
        /// <returns>A new stat instance.</returns>
        /// <exception cref="InvalidOperationException">Thrown when a stat instance cannot be created for the provided type.</exception>
        public static IBaseStat CreateStat(IBaseStat statData)
        {
            return statData.GetType().Name switch
            {
                nameof(FloatStat) => new FloatStat((statData as IStat<float>)?.GetBaseValue() ?? default, statData.Name),
                nameof(IntStat) => new IntStat((statData as IStat<int>)?.GetBaseValue() ?? default, statData.Name),
                nameof(BoolStat) => new BoolStat((statData as IStat<bool>)?.GetBaseValue() ?? default, statData.Name),
                _ => throw new InvalidOperationException($"Could not create a stat instance for type {statData.GetType().Name}"),
            };
        }
    }
}
//EOF.