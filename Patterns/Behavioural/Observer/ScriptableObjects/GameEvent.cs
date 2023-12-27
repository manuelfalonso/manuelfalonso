using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject event used to decouple code instead of a normal class.
    /// Inject a reference to the GameEventSO you want the listeners to raise.
    /// </summary>
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Sombra Studios/Game Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        /// <summary>
        /// List of MonoBehaviour listeners for this event.
        /// </summary>
        private List<GameEventMonoBehaviourListener> _monoBehaviourListeners = new List<GameEventMonoBehaviourListener>();
        /// <summary>
        /// List of ScriptableObject listeners for this event.
        /// </summary>
        private List<GameEventScriptableObjectListener> _listeners = new List<GameEventScriptableObjectListener>();


        /// <summary>
        /// Raises the event, notifying all registered listeners.
        /// </summary>
        public void Raise()
        {
            for (int i = _monoBehaviourListeners.Count - 1; i >= 0; i--)
            {
                _monoBehaviourListeners[i].OnEventRaised();
            }

            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
        }

        // Called from Game Event Listener
        /// <summary>
        /// Registers a MonoBehaviour listener for this event.
        /// </summary>
        /// <param name="listener">The listener to register.</param>
        public void RegisterListener(GameEventMonoBehaviourListener listener)
        {
            _monoBehaviourListeners.Add(listener);
        }

        /// <summary>
        /// Registers a ScriptableObject listener for this event.
        /// </summary>
        /// <param name="listener">The listener to register.</param>
        public void RegisterListener(GameEventScriptableObjectListener listener)
        {
            _listeners.Add(listener);
        }

        // Called from Game Event Listener
        /// <summary>
        /// Unregisters a MonoBehaviour listener for this event.
        /// </summary>
        /// <param name="listener">The listener to unregister.</param>
        public void UnregisterListener(GameEventMonoBehaviourListener listener)
        {
            _monoBehaviourListeners.Remove(listener);
        }

        /// <summary>
        /// Unregisters a ScriptableObject listener for this event.
        /// </summary>
        /// <param name="listener">The listener to unregister.</param>
        public void UnregisterListener(GameEventScriptableObjectListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
