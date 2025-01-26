using SombraStudios.Shared.Structs;
using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Strategy
{
    /// <summary>
    /// Abstract base class for strategy pattern implementation using ScriptableObject.
    /// </summary>
    /// <typeparam name="T">The type of data that the strategy operates on.</typeparam>
    public abstract class StrategySO<T> : ScriptableObject, IStrategy<T>
    {
        [Header(DEBUG_TITLE)]
        /// <summary>
        /// Whether to show logs.
        /// </summary>
        [Tooltip("Whether to show logs.")]
        [SerializeField] protected bool _showLogs;

        protected const string PROPERTIES_TITLE = "Properties";
        protected const string DEBUG_TITLE = "Debug";

        /// <summary>
        /// Tries to execute the strategy if it can be executed.
        /// </summary>
        /// <param name="data">The data that the strategy operates on.</param>
        /// <returns>True if the strategy was executed successfully, otherwise false.</returns>
        public virtual bool TryToExecute(T data)
        {
            if (!CanExecute(data)) return false;
            Execute(data);
            return true;
        }
        
        /// <summary>
        /// Tries to execute the strategy if it can be executed.
        /// Don't have return value to be able to use with UnityEvents depending on the T data type to be
        /// serializable by Unity.
        /// </summary>
        /// <param name="data">The data that the strategy operates on.</param>
        public virtual void TryToExecuteWithUnityEvent(T data)
        {
            TryToExecute(data);
        }

        /// <summary>
        /// Determines whether the strategy can be executed.
        /// </summary>
        /// <param name="data">The data that the strategy operates on.</param>
        /// <returns>True if the strategy can be executed, otherwise false.</returns>
        public virtual bool CanExecute(T data) => true;

        /// <summary>
        /// Executes the strategy.
        /// </summary>
        /// <param name="data">The data that the strategy operates on.</param>
        public abstract void Execute(T data);
    }

    /// <summary>
    /// Abstract base class for strategy pattern implementation using ScriptableObject.
    /// </summary>
    public abstract class StrategySO : StrategySO<VoidStruct>
    {
        /// <summary>
        /// Tries to execute the strategy if it can be executed.
        /// </summary>
        /// <returns>True if the strategy was executed successfully, otherwise false.</returns>
        public virtual bool TryToExecute()
        {
            if (!CanExecute()) return false;
            Execute();
            return true;
        }

        public override bool TryToExecute(VoidStruct data) => TryToExecute();

        /// <summary>
        /// Determines whether the strategy can be executed.
        /// </summary>
        /// <returns>True if the strategy can be executed, otherwise false.</returns>
        public virtual bool CanExecute() => true;

        public override bool CanExecute(VoidStruct data) => CanExecute();

        /// <summary>
        /// Executes the strategy.
        /// </summary>
        public abstract void Execute();

        public override void Execute(VoidStruct data) => Execute();
    }
}