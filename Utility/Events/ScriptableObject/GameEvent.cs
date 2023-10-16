using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Utility.Events
{
    /// <summary>
    /// SO Event used to decoupled code instead of a normal class.
    /// Inject a reference to the GameEventSO you want the listeners to raise
    /// </summary>
    [CreateAssetMenu(fileName = "New Game Event", menuName = "Sombra Studios/Game Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventMonoBehaviourListener> _monoBehaviourListeners = new List<GameEventMonoBehaviourListener>();
        private List<GameEventScriptableObjectListener> _listeners = new List<GameEventScriptableObjectListener>();

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
        public void RegisterListener(GameEventMonoBehaviourListener listener)
        {
            _monoBehaviourListeners.Add(listener);
        }

        public void RegisterListener(GameEventScriptableObjectListener listener)
        {
            _listeners.Add(listener);
        }

        // Called from Game Event Listener
        public void UnregisterListener(GameEventMonoBehaviourListener listener)
        {
            _monoBehaviourListeners.Remove(listener);
        }

        public void UnregisterListener(GameEventScriptableObjectListener listener)
        {
            _listeners.Remove(listener);
        }
    }
}
