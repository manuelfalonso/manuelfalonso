namespace SombraStudios.Shared.Patterns.Creational.FactoryMethod
{
    /// <summary>
    /// Interface for product objects created by the factory method.
    /// </summary>
    public interface IProduct
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Initializes the product.
        /// </summary>
        void Initialize();
    }
}
