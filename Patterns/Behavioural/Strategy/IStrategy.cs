namespace SombraStudios.Shared.Patterns.Behavioural.Strategy
{
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
        public void Execute(T data);

        /// <summary>
        /// Checks if the strategy can be executed with the provided data.
        /// </summary>
        /// <param name="data">The data to check for strategy execution eligibility.</param>
        /// <returns>True if the strategy can be executed; otherwise, false.</returns>
        public bool CanExecute(T data);

        /// <summary>
        /// Attempts to execute the strategy with the provided data.
        /// </summary>
        /// <param name="data">The data needed for the strategy execution.</param>
        /// <returns>True if the strategy was executed successfully; otherwise, false.</returns>
        public bool TryToExecute(T data);
    }
}
