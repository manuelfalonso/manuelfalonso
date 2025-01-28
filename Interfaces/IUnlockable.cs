namespace SombraStudios.Shared.Interfaces
{
    /// <summary>
    /// Interface for objects that can be unlocked.
    /// </summary>
    public interface IUnlockable
    {
        /// <summary>
        /// Checks if the object is unlocked.
        /// </summary>
        /// <returns>True if the object is unlocked, false otherwise.</returns>
        bool IsUnlocked { get; protected set; }
    }
}