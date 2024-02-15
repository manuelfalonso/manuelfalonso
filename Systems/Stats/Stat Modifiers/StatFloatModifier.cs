using UnityEngine;

namespace SombraStudios.Shared.Systems.Stats
{
    /// <summary>
    /// Represents a modifier for a float stat.
    /// </summary>
    public class StatFloatModifier : StatModifier<float>
    {
        /// <summary>
        /// The type of operation the modifier performs.
        /// </summary>
        [SerializeField] private ModifierOperationType _modType = ModifierOperationType.Additive;

        /// <summary>
        /// Gets the type of operation the modifier performs.
        /// </summary>
        public ModifierOperationType ModType => _modType;


        /// <summary>
        /// Constructs a new float stat modifier with a value, operation type, and source.
        /// </summary>
        /// <param name="value">The value of the modifier.</param>
        /// <param name="modifierOperationType">The type of operation the modifier performs.</param>
        /// <param name="source">The source of the modifier.</param>
        public StatFloatModifier(float value, ModifierOperationType modifierOperationType, object source) : base(value, source)
        {
            _value = value;
            _modType = modifierOperationType;
        }

        /// <summary>
        /// Constructs a new float stat modifier with a value and operation type, and no source.
        /// </summary>
        /// <param name="value">The value of the modifier.</param>
        /// <param name="modifierOperationType">The type of operation the modifier performs.</param>
        public StatFloatModifier(float value, ModifierOperationType modifierOperationType) : this(value, modifierOperationType, null) { }
    }
}
//EOF.