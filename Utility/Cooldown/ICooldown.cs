namespace SombraStudios.Shared.Utility.Cooldown
{
    public interface ICooldown
    {
        /// <summary>
        /// Gets or sets the time duration of the cooldown.
        /// </summary>
        float Time { get; }
        /// <summary>
        /// Gets or sets whether the cooldown is currently active.
        /// </summary>
        bool IsActive { get; set; }
        /// <summary>
        /// Gets whether the cooldown is currently in progress.
        /// </summary>
        bool IsInCooldown { get; }

        /// <summary>
        /// Starts the cooldown.
        /// </summary>
        void StartCooldown();
        /// <summary>
        /// Stops the cooldown if it is currently active.
        /// </summary>
        void StopCooldown();
    }
}