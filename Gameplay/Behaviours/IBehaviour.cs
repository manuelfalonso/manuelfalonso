namespace SombraStudios.Shared.Gameplay.Behaviours
{
    /// <summary>
    /// Interface for defining a behavior with enable/disable functionality.
    /// </summary>
    public interface IBehaviour
    {
        /// <summary>
        /// Indicates whether the behavior is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Toggles the state of the behavior.
        /// </summary>
        public void ToggleBehaviour();

        /// <summary>
        /// Applies the behavior.
        /// </summary>
        public void ExecuteBehaviour();
    }
}
