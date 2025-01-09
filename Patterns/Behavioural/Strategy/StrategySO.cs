using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Strategy
{
    /// <summary>
    /// Abstract base class for strategy pattern implementation using ScriptableObject.
    /// </summary>
    public abstract class StrategySO : ScriptableObject, IStrategy
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
        public virtual bool TryToExecute()
        {
            if (CanExecute())
            {
                Execute();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the strategy can be executed.
        /// </summary>
        /// <returns>True if the strategy can be executed, otherwise false.</returns>
        public virtual bool CanExecute() => true;

        /// <summary>
        /// Executes the strategy.
        /// </summary>
        public abstract void Execute();
    }

    /// <summary>
    /// Abstract base class for strategy pattern implementation using ScriptableObject.
    /// </summary>
    /// <typeparam name="T">The type of data that the strategy operates on.</typeparam>
    public abstract class StrategySO<T> : ScriptableObject, IStrategy<T> where T : struct
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
        public virtual bool TryToExecute(T data)
        {
            if (CanExecute(data))
            {
                Execute(data);
                return true;
            }

            return false;
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
}
