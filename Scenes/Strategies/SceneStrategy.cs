using SombraStudios.Shared.ScriptableObjects.Patterns.Behavioural.Strategy;

namespace SombraStudios.Shared.Scenes.Strategies
{
    /// <summary>
    /// Abstract base class for defining scene-related strategies.
    /// Inherits from <see cref="StrategySO"/>.
    /// </summary>
    public abstract class SceneStrategy : StrategySO
    {
        /// <summary>
        /// Log category identifier for scene strategy logs.
        /// </summary>
        protected new static readonly string LOG_CATEGORY = nameof(SceneStrategy);
    }

    /// <summary>
    /// Abstract generic base class for defining scene-related strategies with a specific type.
    /// Inherits from <see cref="StrategySO{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type parameter for the strategy.</typeparam>
    public abstract class SceneStrategy<T> : StrategySO<T>
    {
        /// <summary>
        /// Log category identifier for scene strategy logs.
        /// </summary>
        protected new static readonly string LOG_CATEGORY = nameof(SceneStrategy);
    }
}
