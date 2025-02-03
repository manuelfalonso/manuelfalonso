using System.Collections.Generic;
using UnityEngine;
using Logger = SombraStudios.Shared.Utility.Loggers.Logger;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// Abstract base class for composite condition ScriptableObjects with a parameterized type.
    /// Inherits from ConditionSO with the same type parameter.
    /// </summary>
    /// <typeparam name="T">The type of data used by the condition.</typeparam>
    public abstract class CompositeConditionSO<T> : ConditionSO<T>
    {
        [Header(PROPERTIES_TITLE)]

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
                    if (condition == this)
                    {
                        Logger.LogWarning(LOG_CATEGORY, "Composite condition cannot contain itself!", this);
                        continue;
                    }

                    if (!condition.IsValid(data))
                        return false;
                }
                return true;
            }

            foreach (var condition in _conditions)
            {
                if (condition == this)
                {
                    Logger.LogWarning(LOG_CATEGORY, "Composite condition cannot contain itself!", this);
                    continue;
                }

                if (condition.IsValid(data))
                    return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Abstract base class for composite condition ScriptableObjects.
    /// Inherits from ConditionSO.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCompositeConditional", menuName = "Sombra Studios/Conditions/Composite Condition")]
    public class CompositeConditionSO : ConditionSO
    {
        [Header(PROPERTIES_TITLE)]

        /// <summary>
        /// List of conditions to be checked.
        /// </summary>
        [Tooltip("List of conditions to be checked.")]
        [SerializeField] private List<ConditionSO> _conditions;

        /// <summary>
        /// Indicates whether all conditions must be met.
        /// </summary>
        [Tooltip("Indicates whether all conditions must be met.")]
        [SerializeField] private bool _requireAll;

        /// <summary>
        /// Checks if the composite condition is met.
        /// </summary>
        /// <returns>True if the composite condition is met, false otherwise.</returns>
        public override bool IsValid()
        {
            if (_requireAll)
            {
                foreach (var condition in _conditions)
                {
                    if (condition == this)
                    {
                        Logger.LogWarning(LOG_CATEGORY, "Composite condition cannot contain itself!", this);
                        continue; 
                    }

                    if (!condition.IsValid())
                        return false;
                }
                return true;
            }

            foreach (var condition in _conditions)
            {
                if (condition == this)
                {
                    Logger.LogWarning(LOG_CATEGORY, "Composite condition cannot contain itself!", this);
                    continue;
                }

                if (condition.IsValid())
                    return true;
            }

            return false;
        }
    }
}