using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>  
    /// MonoBehaviour-based listener for multiple ScriptableObject events using Unity Events.  
    /// </summary>  
    public class VoidEventMonoBehaviourListListener : MonoBehaviour
    {
        [Header("Listen to Event Channels")]

        /// <summary>  
        /// The list of ScriptableObject events and their corresponding responses.  
        /// </summary>  
        [Tooltip("The list of ScriptableObject events and their corresponding responses.")]
        [SerializeField] private List<EventChannelResponsePair> _eventResponsePairs;


        private void OnEnable() => RegisterEvents();

        private void OnDisable() => UnregisterEvents();


        /// <summary>  
        /// Registers the event listeners to the event channels.  
        /// </summary>  
        private void RegisterEvents()
        {
            foreach (var pair in _eventResponsePairs)
            {
                if (pair.EventChannel == null)
                {
                    Debug.LogError($"Missing Event Channel reference for {GetType().Name} on {name}. Disabling script.", this);
                    continue;
                }

                pair.EventChannel.OnEventRaised -= pair.Response.Invoke;
                pair.EventChannel.OnEventRaised += pair.Response.Invoke;
            }
        }

        /// <summary>  
        /// Unregisters the event listeners from the event channels.  
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
        /// Represents a pair of EventChannel and UnityEvent response.  
        /// </summary>  
        [System.Serializable]
        public class EventChannelResponsePair
        {
            /// <summary>  
            /// The ScriptableObject EventChannel.  
            /// </summary>  
            [Tooltip("The ScriptableObject EventChannel.")]
            public VoidEventChannelSO EventChannel;

            [Space]

            /// <summary>  
            /// The UnityEvent response associated with the EventChannel.  
            /// </summary>  
            [Tooltip("The UnityEvent response associated with the EventChannel.")]
            public UnityEvent Response;
        }
    }
}
