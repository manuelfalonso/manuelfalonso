using SombraStudios.Shared.Interfaces;

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
        /// The condition that must be valid for this transition to be taken.
        /// </summary>
        ICondition Condition { get; }
    }
}
