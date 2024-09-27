namespace SombraStudios.Shared.Systems.Stats
{
    /// <summary>
    /// Represents an integer stat.
    /// </summary>
    [System.Serializable]
    public class IntStat : Stat<int, StatIntModifier>
    {
        /// <summary>
        /// Constructs a new integer stat with a value and name.
        /// </summary>
        /// <param name="value">The base value of the stat.</param>
        /// <param name="name">The name of the stat.</param>
        public IntStat(int value, string name) : base(value, name) { }


        /// <summary>
        /// Calculates the modified value of the stat.
        /// </summary>
        protected override void CalculateValue()
        {
            _modifiedValue = _baseValue;
            if (_statModifiers.Count == 0) { return; }

            _statModifiers.Sort((x, y) => x.ModType.CompareTo(y.ModType));

            for (int i = 0; i < _statModifiers.Count; i++)
            {
                if (_statModifiers[i].ModType == ModifierOperationType.Additive)
                {
                    _modifiedValue += _statModifiers[i].Value;
                }
                else if (_statModifiers[i].ModType == ModifierOperationType.Multiplicative)
                {
                    _modifiedValue += (_modifiedValue * _statModifiers[i].Value) / 100;
                }
            }
        }
    }
}
//EOF.