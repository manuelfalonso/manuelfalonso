namespace SombraStudios.Shared.Patterns.Behavioural.StackStateMachine
{
    /// <summary>
    /// Represents an abstract state in the state machine.
    /// </summary>
    public abstract class State<TContext> where TContext : struct
    {
        /// <summary>
        /// Reference to the state machine.
        /// </summary>
        protected readonly StackStateMachine<TContext> _stackStateMachine;

        /// <summary>
        /// Constructor to initialize the state with a state machine.
        /// </summary>
        /// <param name="stackStateMachine">The associated state machine.</param>
        protected State(StackStateMachine<TContext> stackStateMachine)
        {
            _stackStateMachine = stackStateMachine;
        }

        /// <summary>
        /// Abstract method called when entering the state.
        /// </summary>
        public abstract void Enter();

        /// <summary>
        /// Abstract method called to update the state.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Abstract method called when exiting the state.
        /// </summary>
        public abstract void Exit();
    }
}