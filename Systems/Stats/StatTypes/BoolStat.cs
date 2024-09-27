using System.Linq;

namespace SombraStudios.Shared.Systems.Stats
{
    /// <summary>
    /// Represents a boolean stat.
    /// </summary>
    [System.Serializable]
    public class BoolStat : Stat<bool, StatBoolModifier>
    {
        /// <summary>
        /// Constructs a new boolean stat with a value and name.
        /// </summary>
        /// <param name="value">The base value of the stat.</param>
        /// <param name="name">The name of the stat.</param>
        public BoolStat(bool value, string name) : base(value, name) { }


        /// <summary>
        /// Calculates the modified value of the stat.
        /// </summary>
        protected override void CalculateValue()
        {
            _modifiedValue = _statModifiers.Count > 0 ? _statModifiers.Last().Value : _baseValue;
        }
    }
}
//EOF.