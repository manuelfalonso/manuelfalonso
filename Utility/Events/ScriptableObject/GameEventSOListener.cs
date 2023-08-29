using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Utility.Events
{
    [CreateAssetMenu(fileName = "New Game Event Listener", menuName = "Sombra Studios/Game Events/Game Event Listener")]
    public class GameEventSOListener : ScriptableObject, ISerializationCallbackReceiver
    {
        public UnityEvent Response;

        [SerializeField] private GameEventSO _gameEvent;


        // Called from Game Event Scriptable Object
        public void OnEventRaised()
        {
            Response.Invoke();
        }


        // Interface implementation
        public void OnBeforeSerialize()
        {
            if (_gameEvent != null)
            {
                _gameEvent.UnregisterListener(this);
            }
        }

        public void OnAfterDeserialize()
        {
            if (_gameEvent != null)
            {
                _gameEvent.UnregisterListener(this);
                _gameEvent.RegisterListener(this);
            }
        }
    }
}