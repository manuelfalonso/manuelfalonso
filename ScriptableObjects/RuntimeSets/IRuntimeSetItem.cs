namespace SombraStudios.Shared.ScriptableObjects.RuntimeSets
{
    /// <summary>
    /// Interface for items that can be added to or removed from a runtime set.
    /// </summary>
    /// <typeparam name="T">The type of items in the runtime set.</typeparam>
    public interface IRuntimeSetItem<T>
    {
        /// <summary>
        /// Gets or sets the runtime set to which the item belongs.
        /// </summary>
        RuntimeSetSO<T> RuntimeSet { get; set; }

        /// <summary>
        /// Adds the item to the runtime set.
        /// </summary>
        void AddToRuntimeSet();

        /// <summary>
        /// Removes the item from the runtime set.
        /// </summary>
        void RemoveFromRuntimeSet();
    }
}
