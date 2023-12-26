using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.Events.ScriptableObjects
{
    /// <summary>
    /// SO Event Listener using a Unity Event.
    /// Add to the response UnityEvent every neccesary action.
    /// Limitations: 
    /// Must define GameEventSO for each type of data structure needed.
    /// UnityEvents in Editor dont support multi parameter unless they are dynamic.
    /// If the object is disabled will stop listening.
    /// </summary>
    public class GameEventMonoBehaviourListener : MonoBehaviour
    {
        [SerializeField] private GameEvent _gameEvent;
        [SerializeField] private UnityEvent Response;


        private void OnEnable() => _gameEvent.RegisterListener(this);

        private void OnDisable() => _gameEvent.UnregisterListener(this);


        // Called from Game Event Scriptable Object
        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}
