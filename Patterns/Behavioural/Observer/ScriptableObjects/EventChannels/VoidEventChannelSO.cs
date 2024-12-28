using SombraStudios.Shared.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject event used to decouple code instead of a normal class.
    /// </summary>
    [CreateAssetMenu(fileName = "NewVoidEventChannel", menuName = "Sombra Studios/Events/Void Event Channel")]
    public class VoidEventChannelSO : ScriptableObject, IDescribable
    {
        /// <summary>
        /// Event triggered when the event is raised.
        /// </summary>
        public UnityAction OnEventRaised;

        [SerializeField] private string _description;
        
        public string Description { get => _description; set => _description = value; }
        
        /// <summary>
        /// Raises the event, notifying all registered listeners.
        /// </summary>
        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}
