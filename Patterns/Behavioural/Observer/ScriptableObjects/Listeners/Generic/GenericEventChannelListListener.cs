using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// A listener for multiple event channels of type T.
    /// This listener subscribes to multiple event channels and invokes corresponding UnityEvents in response
    /// to the events.
    /// </summary>
    /// <typeparam name="T">The type of the event data.</typeparam>
    public abstract class GenericEventChannelListListener<T> : MonoBehaviour
    {
        /// <summary>
        /// A list of event-response pairs.
        /// Each pair associates an event channel with a UnityEvent to be invoked when the event is raised.
        /// </summary>
        [Tooltip("A list of event-response pairs, each linking an event channel to a UnityEvent.")]
        [SerializeField] 
        private List<EventChannelResponsePair<T>> _eventResponsePairs;

        /// <summary>
        /// Registers all events in the list when the GameObject is enabled.
        /// </summary>
        private void OnEnable() => RegisterEvents();

        /// <summary>
        /// Unregisters all events in the list when the GameObject is disabled.
        /// </summary>
        private void OnDisable() => UnregisterEvents();

        /// <summary>
        /// Subscribes each event channel in the list to its corresponding UnityEvent response.
        /// Logs an error if an event channel is missing.
        /// </summary>
        private void RegisterEvents()
        {
            foreach (var pair in _eventResponsePairs)
            {
                if (pair.EventChannel == null)
                {
                    Debug.LogError($"Missing Event Channel reference for {GetType().Name} on {name}. Skipping registration.", this);
                    continue;
                }

                pair.EventChannel.OnEventRaised -= pair.Response.Invoke;
                pair.EventChannel.OnEventRaised += pair.Response.Invoke;
            }
        }

        /// <summary>
        /// Unsubscribes each event channel in the list from its corresponding UnityEvent response.
        /// </summary>
        private void UnregisterEvents()
        {
            foreach (var pair in _eventResponsePairs)
            {
                if (pair.EventChannel == null) continue;

                pair.EventChannel.OnEventRaised -= pair.Response.Invoke;
            }
        }

        /// <summary>
        /// A class representing a pair of an event channel and its corresponding UnityEvent response.
        /// </summary>
        [System.Serializable]
#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
        public class EventChannelResponsePair<T>
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
        {
            /// <summary>
            /// The event channel that will raise events of type T.
            /// </summary>
            [Tooltip("The event channel to listen to.")]
            public GenericEventChannelSO<T> EventChannel;

            [Space]
            
            /// <summary>
            /// The UnityEvent to invoke when the associated event channel raises an event.
            /// </summary>
            [Tooltip("The UnityEvent to invoke when the event channel raises an event.")]
            public UnityEvent<T> Response;
        }
    }
}
