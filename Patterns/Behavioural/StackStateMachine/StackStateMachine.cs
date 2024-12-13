using System.Collections.Generic;

namespace SombraStudios.Shared.Patterns.Behavioural.StackStateMachine
{
    /// <summary>
    /// Implements a stack-based state machine. Aka Pushdown Automata.
    /// 1- Create a Context struct to hold the data that the states need to share.
    /// 2- Called the constructor with this context.
    /// 3- Push the initial state 
    /// Credits to: git-ammend
    /// </summary>
    public class StackStateMachine<TContext> where TContext : struct
    {
        /// <summary>
        /// Stack of states.
        /// </summary>
        private readonly Stack<State<TContext>> _stateStack = new();

        /// <summary>
        /// Gets the current state of the state machine.
        /// </summary>
        public State<TContext> CurrentState => _stateStack.Count > 0 ? _stateStack.Peek() : null;

        /// <summary>
        /// Context of the state machine.
        /// </summary>
        public TContext Context;

        /// <summary>
        /// Constructor to initialize the state machine with a context.
        /// </summary>
        /// <param name="context">The initial context.</param>
        public StackStateMachine(TContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Pushes a new state onto the stack and calls its Enter method.
        /// </summary>
        /// <param name="newState">The new state to push.</param>
        public void PushState(State<TContext> newState)
        {
            if (CurrentState != null) CurrentState.Exit();
            _stateStack.Push(newState);
            newState.Enter();
        }

        /// <summary>
        /// Pops the current state off the stack and calls its Exit method.
        /// </summary>
        public void PopState()
        {
            if (_stateStack.Count == 0) return;

            CurrentState.Exit();
            _stateStack.Pop();
            if (CurrentState != null) CurrentState.Enter();
        }

        /// <summary>
        /// Calls the Update method of the current state.
        /// </summary>
        public void Update() => CurrentState?.Update();

        /// <summary>
        /// Sets a new context for the state machine.
        /// </summary>
        /// <param name="context">The new context.</param>
        public void SetContext(TContext context) => Context = context;
    }
}