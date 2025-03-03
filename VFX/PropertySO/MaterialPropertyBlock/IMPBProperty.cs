using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// Interface for Material Property Block properties.
    /// </summary>
    /// <typeparam name="T">The type of the property value.</typeparam>
    public interface IMPBProperty<T>
    {
        /// <summary>
        /// Gets or sets the name of the material shader property.
        /// </summary>
        string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        T Property { get; set; }

        /// <summary>
        /// Determines whether the material property block has the property.
        /// </summary>
        /// <param name="mpb">The material property block.</param>
        /// <returns>True if the material property block has the property; otherwise, false.</returns>
        bool HasProperty(MaterialPropertyBlock mpb);

        /// <summary>
        /// Gets the value of the property from the material property block.
        /// </summary>
        /// <param name="mpb">The material property block.</param>
        /// <returns>The value of the property.</returns>
        T GetProperty(MaterialPropertyBlock mpb);

        /// <summary>
        /// Sets the value of the property in the material property block.
        /// </summary>
        /// <param name="mpb">The material property block.</param>
        /// <param name="value">The value of the property.</param>
        void SetProperty(MaterialPropertyBlock mpb, T value);
    }
}
