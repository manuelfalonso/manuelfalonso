namespace SombraStudios.Shared.Patterns.Behavioural.Strategy
{
    /// <summary>
    /// Defines a non-generic strategy pattern interface for executing actions.
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// Executes the strategy.
        /// </summary>
        void Execute();

        /// <summary>
        /// Checks if the strategy can be executed.
        /// </summary>
        /// <returns>True if the strategy can be executed; otherwise, false.</returns>
        bool CanExecute();

        /// <summary>
        /// Attempts to execute the strategy.
        /// </summary>
        /// <returns>True if the strategy was executed successfully; otherwise, false.</returns>
        bool TryToExecute();
    }
    
    /// <summary>
    /// Defines a strategy pattern interface for executing actions based on the provided data.
    /// </summary>
    /// <typeparam name="T">The type of data used by the strategy.</typeparam>
    public interface IStrategy<T>
    {
        /// <summary>
        /// Executes the strategy using the provided data.
        /// </summary>
        /// <param name="data">The data needed for the strategy execution.</param>
        void Execute(T data);

        /// <summary>
        /// Checks if the strategy can be executed with the provided data.
        /// </summary>
        /// <param name="data">The data to check for strategy execution eligibility.</param>
        /// <returns>True if the strategy can be executed; otherwise, false.</returns>
        bool CanExecute(T data);

        /// <summary>
        /// Attempts to execute the strategy with the provided data.
        /// </summary>
        /// <param name="data">The data needed for the strategy execution.</param>
        /// <returns>True if the strategy was executed successfully; otherwise, false.</returns>
        bool TryToExecute(T data);
    }
}