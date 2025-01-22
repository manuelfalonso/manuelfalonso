using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// Abstract base class for composite condition ScriptableObjects with a parameterized type.
    /// Inherits from ConditionSO with the same type parameter.
    /// </summary>
    /// <typeparam name="T">The type of data used by the condition.</typeparam>
    public abstract class CompositeConditionSO<T> : ConditionSO<T>
    {
        /// <summary>
        /// List of conditions to be checked.
        /// </summary>
        [Tooltip("List of conditions to be checked.")]
        [SerializeField] private List<ConditionSO<T>> _conditions;

        /// <summary>
        /// Indicates whether all conditions must be met.
        /// </summary>
        [Tooltip("Indicates whether all conditions must be met.")]
        [SerializeField] private bool _requireAll;

        /// <summary>
        /// Checks if the composite condition is met.
        /// </summary>
        /// <param name="data">The data needed to check the condition.</param>
        /// <returns>True if the composite condition is met, false otherwise.</returns>
        public override bool IsValid(T data)
        {
            if (_requireAll)
            {
                foreach (var condition in _conditions)
                {
                    if (!condition.IsValid(data))
                        return false;
                }
                return true;
            }

            foreach (var condition in _conditions)
            {
                if (condition.IsValid(data))
                    return true;
            }

            return false;
        }
    }
}