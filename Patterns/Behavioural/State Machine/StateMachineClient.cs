using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.StateMachine
{
    /// <summary>
    /// Base class for objects that use a state machine.
    /// </summary>
    public abstract class StateMachineClient : MonoBehaviour
    {
        protected StateMachine _stateMachine;


        protected virtual void Start()
        {
            _stateMachine = new StateMachine();
            SetUpStateMachine();
        }

        protected virtual void Update()
        {
            _stateMachine.Update();
        }

        protected virtual void FixedUpdate()
        {
            _stateMachine.FixedUpdate();
        }


        /// <summary>
        /// Sets up the state machine with transitions and states.
        /// </summary>
        protected abstract void SetUpStateMachine();

        /// <summary>
        /// Adds a transition from one state to another state based on a condition predicate.
        /// </summary>
        protected void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        /// <summary>
        /// Adds a transition from any state to a specific state based on a condition predicate.
        /// </summary>
        protected void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);
    }
}
