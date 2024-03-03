namespace SombraStudios.Shared.Patterns.Behavioural.StateMachine
{
    /// <summary>
    /// Base class for a state in a state machine.
    /// </summary>
    public abstract class BaseState : IState
    {
        public virtual void OnEnter() { }
        public virtual void OnUpdate() { }
        public virtual void OnFixedUpdate() { }
        public virtual void OnExit() { }
    }
}
