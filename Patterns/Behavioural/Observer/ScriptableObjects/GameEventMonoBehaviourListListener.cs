using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// Listens to a list of ScriptableObject events and invokes corresponding responses.
    /// </summary>
    public class GameEventMonoBehaviourListListener : MonoBehaviour
    {
        /// <summary>
        /// The list of ScriptableObject events and their corresponding responses.
        /// </summary>
        [Tooltip("The list of ScriptableObject events and their corresponding responses.")]
        [SerializeField] private List<GameEventResponsePair> _eventResponsePairs;


        private void OnEnable() => RegisterListeners();

        private void OnDisable() => UnregisterListeners();


        // Called from Game Event Scriptable Object
        /// <summary>
        /// Called from a Game Event ScriptableObject when the event is raised.
        /// Invokes the corresponding response for the given event.
        /// </summary>
        /// <param name="gameEvent">The GameEvent that was raised.</param>
        public void OnEventRaised(GameEvent gameEvent)
        {
            foreach (var pair in _eventResponsePairs)
            {
                if (pair.GameEvent == gameEvent)
                {
                    pair.Response.Invoke();
                    break; // Break the loop once the correct event is found and invoked.
                }
            }
        }


        /// <summary>
        /// Registers listeners for all events in the list.
        /// </summary>
        private void RegisterListeners()
        {
            foreach (var pair in _eventResponsePairs)
            {
                pair.GameEvent.RegisterListener(this);
            }
        }

        /// <summary>
        /// Unregisters listeners for all events in the list.
        /// </summary>
        private void UnregisterListeners()
        {
            foreach (var pair in _eventResponsePairs)
            {
                pair.GameEvent.UnregisterListener(this);
            }
        }


        /// <summary>
        /// Represents a pair of GameEvent and UnityEvent response.
        /// </summary>
        [System.Serializable]
        public class GameEventResponsePair
        {
            /// <summary>
            /// The ScriptableObject GameEvent.
            /// </summary>
            [Tooltip("The ScriptableObject GameEvent.")]
            public GameEvent GameEvent;
            /// <summary>
            /// The UnityEvent response associated with the GameEvent.
            /// </summary>
            [Tooltip("The UnityEvent response associated with the GameEvent.")]
            public UnityEvent Response;
        }
    }
}
