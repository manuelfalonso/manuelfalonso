namespace SombraStudios.Shared.Patterns.Behavioural.FiniteStateMachine
{
    /// <summary>
    /// Interface for a transition between states in a state machine.
    /// </summary>
    public interface ITransition
    {
        /// <summary>
        /// The state to transition to.
        /// </summary>
        IState To { get; }
        /// <summary>
        /// The condition predicate for the transition.
        /// </summary>
        IPredicate Condition { get; }
    }
}
