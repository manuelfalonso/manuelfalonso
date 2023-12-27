using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects
{
    /// <summary>
    /// MonoBehaviour-based listener for a ScriptableObject event using a Unity Event.
    /// Add necessary actions to the response UnityEvent.
    /// Limitations:
    /// - Must define a GameEventSO for each type of data structure needed.
    /// - UnityEvents in the Editor don't support multi-parameters unless they are dynamic.
    /// - If the object is disabled, it will stop listening.
    /// </summary>
    public class GameEventMonoBehaviourListener : MonoBehaviour
    {
        /// <summary>
        /// The ScriptableObject event to listen to.
        /// </summary>
        [Tooltip("The ScriptableObject event to listen to.")]
        [SerializeField] private GameEvent _gameEvent;
        /// <summary>
        /// The Unity Event response to invoke when the ScriptableObject event is raised.
        /// </summary>
        [Tooltip("The Unity Event response to invoke when the ScriptableObject event is raised.")]
        [SerializeField] private UnityEvent Response;


        private void OnEnable() => _gameEvent.RegisterListener(this);

        private void OnDisable() => _gameEvent.UnregisterListener(this);


        // Called from Game Event Scriptable Object
        /// <summary>
        /// Invokes the response UnityEvent.
        /// </summary>
        public void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}
