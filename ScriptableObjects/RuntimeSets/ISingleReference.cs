namespace SombraStudios.Shared.ScriptableObjects.RuntimeSets
{
    /// <summary>  
    /// Interface for objects that hold a single reference of type T.  
    /// </summary>  
    /// <typeparam name="T">The type of the reference.</typeparam>  
    public interface ISingleReference<T>
    {
        /// <summary>  
        /// Gets or sets the single reference.  
        /// </summary>  
        SingleReferenceSO<T> SOReference { get; set; }

        /// <summary>
        /// Set the object to the reference.
        /// </summary>
        void SetReference();

        /// <summary>
        /// Clear the object from the reference.
        /// </summary>
        void ClearReference();
    }
}
