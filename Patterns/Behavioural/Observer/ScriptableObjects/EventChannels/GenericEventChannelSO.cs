using SombraStudios.Shared.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// A generic event channel scriptable object that allows raising events with a parameter of type T.
    /// </summary>
    /// <typeparam name="T">The type of the parameter for the event.</typeparam>
    public abstract class GenericEventChannelSO<T> : ScriptableObject, IDescribable
    {
        /// <summary>
        /// The action to perform. Listeners subscribe to this UnityAction.
        /// </summary>
        [Tooltip("The action to perform. Listeners subscribe to this UnityAction")]
        public UnityAction<T> OnEventRaised;

        [SerializeField] private string _description;
        
        public string Description { get => _description; set => _description = value; }

        /// <summary>
        /// Raises the event with the specified parameter.
        /// </summary>
        /// <param name="parameter">The parameter to pass to the event listeners.</param>
        public void RaiseEvent(T parameter)
        {
            if (OnEventRaised == null)
                return;

            OnEventRaised.Invoke(parameter);
        }
    }
}
