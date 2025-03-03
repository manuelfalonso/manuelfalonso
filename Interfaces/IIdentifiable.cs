namespace SombraStudios.Shared.Interfaces
{
    /// <summary>
    /// Interface for objects that can be identified by a unique identifier.
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Gets the unique identifier for the object.
        /// </summary>
        string Id { get; }
    }
}
