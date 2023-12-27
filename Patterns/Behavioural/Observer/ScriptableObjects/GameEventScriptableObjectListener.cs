using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// ScriptableObject-based listener for a ScriptableObject event using a Unity Event.
    /// </summary>
    [CreateAssetMenu(fileName = "New Game Event Listener", menuName = "Sombra Studios/Game Events/Game Event Listener")]
    public class GameEventScriptableObjectListener : ScriptableObject
    {
        /// <summary>
        /// The Unity Event response to invoke when the ScriptableObject event is raised.
        /// </summary>
        [Tooltip("The Unity Event response to invoke when the ScriptableObject event is raised.")]
        public UnityEvent Response;

        /// <summary>
        /// The ScriptableObject event to listen to.
        /// </summary>
        [Tooltip("The ScriptableObject event to listen to.")]
        [SerializeField] private GameEvent _gameEvent;


        private void OnEnable() => RegisterListener();

        private void OnDisable() => UnregisterListener();


        /// <summary>
        /// Called from the ScriptableObject event. Invokes the response UnityEvent.
        /// </summary>
        public void OnEventRaised()
        {
            Response.Invoke();
        }


        /// <summary>
        /// Registers the listener to the associated ScriptableObject event.
        /// </summary>
        private void RegisterListener()
        {
            if (_gameEvent != null)
            {
                _gameEvent.UnregisterListener(this);
                _gameEvent.RegisterListener(this);
            }
        }

        /// <summary>
        /// Unregisters the listener from the associated ScriptableObject event.
        /// </summary>
        private void UnregisterListener()
        {
            if (_gameEvent != null)
            {
                _gameEvent.UnregisterListener(this);
            }
        }
    }
}