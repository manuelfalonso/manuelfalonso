using UnityEngine;

namespace SombraStudios.Shared.Patterns.Creational.FactoryMethod
{
    /// <summary>
    /// Abstract factory class for creating product instances.
    /// </summary>
    public abstract class Factory : MonoBehaviour
    {
        /// <summary>
        /// Creates a product at the specified position with optional rotation and parent.
        /// </summary>
        /// <param name="position">The position to create the product at.</param>
        /// <param name="rotation">The optional rotation to apply to the product. Defaults to Quaternion.identity.</param>
        /// <param name="parent">The optional parent transform to set for the product. Defaults to null.</param>
        /// <returns>The created product.</returns>
        public abstract IProduct CreateProduct(Vector3 position, Quaternion rotation = default, Transform parent = null);
    }
}
