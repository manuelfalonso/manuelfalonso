using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject-based listener for a ScriptableObject event using a Unity Event.
    /// </summary>
    [CreateAssetMenu(fileName = "New Event Channel Listener", menuName = "Sombra Studios/Events/Event Channel Listener")]
    public class VoidEventChannelSOListener : ScriptableObject
    {
        [Header("Listen to Event Channels")]

        /// <summary>
        /// The ScriptableObject event to listen to.
        /// </summary>
        [Tooltip("The ScriptableObject event to listen to.")]
        [SerializeField] private VoidEventChannelSO _eventChannel;

        [Space]

        /// <summary>
        /// The Unity Event response to invoke when the ScriptableObject event is raised.
        /// </summary>
        [Tooltip("The Unity Event response to invoke when the ScriptableObject event is raised.")]
        public UnityEvent Response;

        private void OnEnable() => RegisterEvent();

        private void OnDisable() => UnregisterEvent();

        /// <summary>
        /// Registers the event listener to the event channel.
        /// </summary>
        private void RegisterEvent()
        {
            if (_eventChannel == null)
            {
                Debug.LogError($"Missing Event Channel reference for {GetType().Name} on {name}. Disabling script.", this);
                return;
            }

            _eventChannel.OnEventRaised -= OnEventRaised;
            _eventChannel.OnEventRaised += OnEventRaised;
        }

        /// <summary>
        /// Unregisters the event listener from the event channel.
        /// </summary>
        private void UnregisterEvent()
        {
            if (_eventChannel == null) return;

            _eventChannel.OnEventRaised -= OnEventRaised;
        }

        /// <summary>
        /// Invokes the response UnityEvent.
        /// </summary>
        private void OnEventRaised()
        {
            Response?.Invoke();
        }
    }
}