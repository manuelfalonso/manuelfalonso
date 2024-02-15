using System;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Stats
{
    /// <summary>
    /// Represents a stat with a base value and potential modifiers.
    /// </summary>
    [Serializable]
    public abstract class Stat<T, TModifier> : IStat<T> where TModifier : StatModifier<T>
    {
        /// <summary>
        /// The name of the stat.
        /// </summary>
        [SerializeField] protected string _name;
        /// <summary>
        /// The base value of the stat.
        /// </summary>
        [SerializeField] protected T _baseValue;
        /// <summary>
        /// The list of stat modifiers.
        /// </summary>
        [SerializeField] protected List<TModifier> _statModifiers;

        /// <summary>
        /// The modified value of the stat.
        /// </summary>
        protected T _modifiedValue;

        /// <summary>
        /// Event triggered when the stat is modified.
        /// </summary>
        public Action<T> StatModified;


        /// <summary>
        /// Constructs a new stat with a base value and name.
        /// </summary>
        protected Stat(T value, string name)
        {
            _name = name;
            _baseValue = value;
            _statModifiers = new List<TModifier>();
        }


        /// <summary>
        /// Gets the name of the stat.
        /// </summary>
        public virtual string Name => _name;

        /// <summary>
        /// Gets the base value of the stat.
        /// </summary>
        public virtual T GetBaseValue() => _baseValue;

        /// <summary>
        /// Gets the value of the stat, considering modifiers.
        /// </summary>
        public virtual T GetValue()
        {
            return _statModifiers.Count == 0 || _statModifiers == null ? _baseValue : _modifiedValue;
        }

        /// <summary>
        /// Adds a modifier to the stat and recalculates its value.
        /// </summary>
        public virtual void AddModifier(TModifier modifier) 
        {
            _statModifiers.Add(modifier);
            CalculateValue();
            StatModified?.Invoke(_modifiedValue);
        }

        /// <summary>
        /// Removes a modifier from the stat and recalculates its value.
        /// </summary>
        public virtual void RemoveModifier(TModifier modifier)
        {
            if (!_statModifiers.Contains(modifier)) { return; }

            _statModifiers.Remove(modifier);
            CalculateValue();
            StatModified?.Invoke(_modifiedValue);
        }

        /// <summary>
        /// Removes all modifiers from a specific source and recalculates the stat value.
        /// </summary>
        public void RemoveModifiersFromSource(object source)
        {
            for (int i = _statModifiers.Count - 1; i >= 0; i--)
            {
                if (_statModifiers[i].Source == source)
                {
                    _statModifiers.RemoveAt(i);
                    CalculateValue();
                    StatModified?.Invoke(_modifiedValue);
                }
            }
        }

        /// <summary>
        /// Checks if the stat has any modifier from a specific source.
        /// </summary>
        public bool HasModifierFromSource(object source)
        {
            for (int i = _statModifiers.Count - 1; i >= 0; i--)
            {
                if (_statModifiers[i].Source != source) { continue; }
                return true;
            }
            return false;
        }


        /// <summary>
        /// Calculates the value of the stat, considering its modifiers.
        /// </summary>
        protected abstract void CalculateValue();
    }
}
//EOF.