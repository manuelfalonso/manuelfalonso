namespace SombraStudios.Shared.Patterns.Behavioural.StateMachine
{
    /// <summary>
    /// Interface for a state in a state machine.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Called when entering the state.
        /// </summary>
        void OnEnter();
        /// <summary>
        /// Called every frame while the state is active.
        /// </summary>
        void OnUpdate();
        /// <summary>
        /// Called every fixed frame while the state is active.
        /// </summary>
        void OnFixedUpdate();
        /// <summary>
        /// Called when exiting the state.
        /// </summary>
        void OnExit();
    }
}
