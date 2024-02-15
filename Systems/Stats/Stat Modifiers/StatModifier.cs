using UnityEngine;

namespace SombraStudios.Shared.Systems.Stats
{
    /// <summary>
    /// Represents a modifier for a stat.
    /// </summary>
    [System.Serializable]
    public abstract class StatModifier<T>
    {
        /// <summary>
        /// The source of the modifier.
        /// </summary>
        public object _source;

        /// <summary>
        /// The value of the modifier.
        /// </summary>
        [SerializeField] protected T _value;

        /// <summary>
        /// Gets the source of the modifier.
        /// </summary>
        public object Source => _source;
        /// <summary>
        /// Gets the value of the modifier.
        /// </summary>
        public T Value => _value;


        /// <summary>
        /// Constructs a new stat modifier with a value and source.
        /// </summary>
        /// <param name="value">The value of the modifier.</param>
        /// <param name="source">The source of the modifier.</param>
        public StatModifier(T value, object source)
        {
            _value = value;
            _source = source;
        }

        /// <summary>
        /// Constructs a new stat modifier with a value and no source.
        /// </summary>
        /// <param name="value">The value of the modifier.</param>
        public StatModifier(T value) : this(value, null) { }
    }
}
//EOF.