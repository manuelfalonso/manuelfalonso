namespace SombraStudios.Shared.Patterns.Behavioural.FiniteStateMachine
{
    /// <summary>
    /// Interface for a condition predicate used in transitions.
    /// </summary>
    public interface IPredicate
    {
        /// <summary>
        /// Evaluates the condition and returns the result.
        /// </summary>
        /// <returns>True if the condition is met, otherwise false.</returns>
        bool Evaluate();
    }
}
