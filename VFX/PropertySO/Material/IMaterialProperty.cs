using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// Interface for defining and manipulating material shader properties.
    /// </summary>
    /// <typeparam name="T">Type of the property value.</typeparam>
    public interface IMaterialProperty<T>
    {
        /// <summary>
        /// Gets the name of the material shader property.
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// Gets the value of the material property.
        /// </summary>
        T Property { get; }

        /// <summary>
        /// Checks if the material has the specified shader property.
        /// </summary>
        /// <param name="id">The property ID (hashed name).</param>
        /// <param name="material">The material to check.</param>
        /// <returns>True if the property exists, otherwise false.</returns>
        bool HasProperty(int id, Material material);

        /// <summary>
        /// Retrieves the value of the specified shader property from the material.
        /// </summary>
        /// <param name="id">The property ID (hashed name).</param>
        /// <param name="material">The material to retrieve the property from.</param>
        /// <returns>The value of the property.</returns>
        T GetProperty(int id, Material material);

        /// <summary>
        /// Sets the value of the specified shader property on the material.
        /// </summary>
        /// <param name="id">The property ID (hashed name).</param>
        /// <param name="value">The value to set.</param>
        /// <param name="material">The material to modify.</param>
        void SetProperty(int id, T value, Material material);
    }
}
