namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// Interface for blend shape properties.
    /// </summary>
    public interface IBlendShapeProperty
    {
        /// <summary>
        /// Gets the name of the blend shape.
        /// </summary>
        string BlendShapeName { get; }

        /// <summary>
        /// Gets the weight of the blend shape.
        /// </summary>
        float Weight { get; }
    }
}
