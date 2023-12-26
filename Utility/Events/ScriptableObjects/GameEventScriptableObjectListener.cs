using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Utility.Events.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Game Event Listener", menuName = "Sombra Studios/Game Events/Game Event Listener")]
    public class GameEventScriptableObjectListener : ScriptableObject
    {
        public UnityEvent Response;

        [SerializeField] private GameEvent _gameEvent;


        private void OnEnable() => RegisterListener();

        private void OnDisable() => UnregisterListener();


        // Called from Game Event Scriptable Object
        public void OnEventRaised()
        {
            Response.Invoke();
        }


        private void RegisterListener()
        {
            if (_gameEvent != null)
            {
                _gameEvent.UnregisterListener(this);
                _gameEvent.RegisterListener(this);
            }
        }

        private void UnregisterListener()
        {
            if (_gameEvent != null)
            {
                _gameEvent.UnregisterListener(this);
            }
        }
    }
}