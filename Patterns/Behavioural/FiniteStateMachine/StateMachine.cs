using System;
using System.Collections.Generic;

namespace SombraStudios.Shared.Patterns.Behavioural.FiniteStateMachine
{
    /// <summary>
    /// Represents a finite state machine.
    /// </summary>
    public class StateMachine
    {
        private StateNode _current;
        private Dictionary<Type, StateNode> _nodes = new();
        private HashSet<ITransition> _anyTransitions = new();


        /// <summary>
        /// Updates the state machine.
        /// </summary>
        public void Update()
        {
            var transition = GetTransition();
            if (transition != null) { ChangeState(transition.To); }

            _current.State?.OnUpdate();
        }

        /// <summary>
        /// Updates the fixed state machine.
        /// </summary>
        public void FixedUpdate()
        {
            _current.State?.OnFixedUpdate();
        }

        /// <summary>
        /// Sets the current state of the state machine.
        /// </summary>
        public void SetState(IState state)
        {
            _current = _nodes[state.GetType()];
            _current.State?.OnEnter();
        }

        /// <summary>
        /// Adds a transition between states in the state machine.
        /// </summary>
        public void AddTransition(IState from, IState to, IPredicate condition)
        {
            GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
        }

        /// <summary>
        /// Adds a transition from any state to a specific state in the state machine.
        /// </summary>
        public void AddAnyTransition(IState to, IPredicate condition)
        {
            _anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
        }


        /// <summary>
        /// Retrieves the transition that evaluates to true.
        /// </summary>
        private ITransition GetTransition()
        {
            foreach (var transition in _anyTransitions)
            {
                if (transition.Condition.Evaluate()) { return transition; }
            }

            foreach (var transition in _current.Transitions)
            {
                if (transition.Condition.Evaluate()) { return transition; }
            }

            return null;
        }

        /// <summary>
        /// Changes the current state of the state machine.
        /// </summary>
        private void ChangeState(IState state)
        {
            if (_current.State == state) { return; }

            var previousState = _current.State;
            var nextState = _nodes[state.GetType()].State;

            previousState?.OnExit();
            nextState?.OnEnter();

            _current = _nodes[nextState.GetType()];
        }

        /// <summary>
        /// Retrieves an existing node or adds a new one to the state machine.
        /// </summary>
        private StateNode GetOrAddNode(IState state)
        {
            var node = _nodes.GetValueOrDefault(state.GetType());

            if (node == null)
            {
                node = new StateNode(state);
                _nodes.Add(state.GetType(), node);
            }

            return node;
        }

        /// <summary>
        /// Represents a node in the state machine graph.
        /// </summary>
        private class StateNode
        {
            /// <summary>
            /// The state associated with this node.
            /// </summary>
            public IState State { get; }
            /// <summary>
            /// The transitions originating from this state.
            /// </summary>
            public HashSet<ITransition> Transitions { get; }


            public StateNode(IState state)
            {
                State = state;
                Transitions = new();
            }


            /// <summary>
            /// Adds a transition to the set of transitions.
            /// </summary>
            public void AddTransition(ITransition transition)
            {
                Transitions.Add(transition);
            }

            /// <summary>
            /// Adds a transition with a specified condition to the set of transitions.
            /// </summary>
            public void AddTransition(IState to, IPredicate condition)
            {
                Transitions.Add(new Transition(to, condition));
            }
        }
    }
}
