namespace SombraStudios.Shared.ScriptableObjects.RuntimeSets
{
    /// <summary>
    /// Interface for items that can be added to or removed from an observable runtime set.
    /// </summary>
    /// <typeparam name="T">The type of elements in the runtime set.</typeparam>
    public interface IObservableRuntimeSetItem<T>
    {
        /// <summary>
        /// Gets or sets the runtime set to which the item belongs.
        /// </summary>
        ObservableRuntimeSetSO<T> RuntimeSet { get; set; }

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