using UnityEngine;

namespace SombraStudios.Shared.ScriptableObjects.RuntimeSets
{
    /// <summary>  
    /// A ScriptableObject that holds a single reference of type T.  
    /// </summary>  
    /// <typeparam name="T">The type of the reference.</typeparam>  
    public abstract class SingleReferenceSO<T> : ScriptableObject
    {
        /// <summary>  
        /// The reference of type T.  
        /// </summary>  
        private T _reference;

        /// <summary>  
        /// Gets or sets the reference.  
        /// </summary>  
        public T Reference
        {
            get
            {
                if (_reference == null)
                {
                    Debug.LogError("No reference assigned!", this);
                }

                return _reference;
            }
            set => _reference = value;
        }
    }
}