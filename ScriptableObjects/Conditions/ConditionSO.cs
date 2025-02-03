using SombraStudios.Shared.Interfaces;
using SombraStudios.Shared.Structs;
using UnityEngine;

namespace SombraStudios.Shared.ScriptableObjects.Conditions
{
    /// <summary>
    /// Abstract base class for condition ScriptableObjects with a parameterized type.
    /// Implements the ICondition interface.
    /// </summary>
    /// <typeparam name="T">The type of data used by the condition.</typeparam>
    public abstract class ConditionSO<T> : ScriptableObject, ICondition<T>
    {
        protected const string LOG_CATEGORY = "ConditionSO";
        protected const string DEBUG_TITLE = "Debug";
        protected const string PROPERTIES_TITLE = "Properties";
        
        [Header(DEBUG_TITLE)] 
        /// <summary>
        /// Show debug logs.
        /// </summary>
        [Tooltip("Show debug logs.")]
        [SerializeField] protected bool _showLogs;
        
        public abstract bool IsValid(T data);
    }

    /// <summary>
    /// Abstract base class for condition ScriptableObjects without a parameterized type.
    /// Inherits from ConditionSO with VoidStruct as the type parameter.
    /// </summary>
    public abstract class ConditionSO : ConditionSO<VoidStruct>
    {
        /// <summary>
        /// Checks if the condition is met.
        /// </summary>
        /// <returns>True if the condition is met, false otherwise.</returns>
        public abstract bool IsValid();

        public override bool IsValid(VoidStruct data)
        {
            return IsValid();
        }
    }
}