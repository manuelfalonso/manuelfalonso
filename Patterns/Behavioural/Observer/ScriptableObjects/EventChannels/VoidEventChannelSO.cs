using SombraStudios.Shared.ScriptableObjects;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject event used to decouple code instead of a normal class.
    /// </summary>
    [CreateAssetMenu(fileName = "NewVoidEventChannel", menuName = "Sombra Studios/Events/Void Event Channel")]
    public class VoidEventChannelSO : DescriptionSO
    {
        /// <summary>
        /// Event triggered when the event is raised.
        /// </summary>
        public UnityAction OnEventRaised;

        /// <summary>
        /// Raises the event, notifying all registered listeners.
        /// </summary>
        public void RaiseEvent()
        {
            OnEventRaised?.Invoke();
        }
    }
}
