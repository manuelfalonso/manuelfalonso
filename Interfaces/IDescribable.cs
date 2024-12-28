namespace SombraStudios.Shared.Interfaces
{
    /// <summary>
    /// Interface for objects that can provide a description.
    /// </summary>
    public interface IDescribable
    {
        /// <summary>
        /// Gets or sets the description of the object.
        /// </summary>
        string Description { get; set; }
    }
}