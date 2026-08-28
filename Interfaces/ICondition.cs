namespace SombraStudios.Shared.Interfaces
{
    /// <summary>
    /// Defines an interface for checking conditions based on the provided data.
    /// </summary>
    /// <typeparam name="T">The type of data used by the condition.</typeparam>
    public interface ICondition<in T>
    {
        /// <summary>
        /// Checks if the condition is met.
        /// </summary>
        /// <param name="data">The data needed to check the condition.</param>
        /// <returns>True if the condition is met, false otherwise.</returns>
        bool IsValid(T data);
    }

    /// <summary>
    /// Defines a contract for evaluating a condition.
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// Determines whether the current state or configuration is valid.
        /// </summary>
        /// <returns><see langword="true"/> if the current state or configuration is valid; otherwise, <see langword="false"/>.</returns>
        bool IsValid();
    }
}