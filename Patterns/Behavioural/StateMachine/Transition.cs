namespace SombraStudios.Shared.Patterns.Behavioural.StateMachine
{
    /// <summary>
    /// Implementation of a transition between states in a state machine using condition predicates.
    /// </summary>
    public class Transition : ITransition
    {
        public IState To { get; }
        public IPredicate Condition { get; }


        public Transition(IState to, IPredicate condition)
        {
            To = to;
            Condition = condition;
        }
    }
}
