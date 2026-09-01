using SombraStudios.Shared.Interfaces;

namespace SombraStudios.Shared.Patterns.Behavioural.FiniteStateMachine
{
    /// <summary>
    /// Implementation of a transition between states in a state machine, guarded by a condition.
    /// </summary>
    public class Transition : ITransition
    {
        public IState To { get; }
        public ICondition Condition { get; }


        public Transition(IState to, ICondition condition)
        {
            To = to;
            Condition = condition;
        }
    }
}
