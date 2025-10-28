using SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Events
{
    [CreateAssetMenu(
        fileName = "RaiseEventAction", 
        menuName = "Sombra Studios/Systems/Events/Raise Event Action", 
        order = 15)]
    public class RaiseEventActionSO : EventActionSO
    {
        [Header("Event Data")]
        [SerializeField] protected VoidEventChannelSO _gameEvent;

        public override void PauseAction(bool pause) { }

        public override IEnumerator StartAction()
        {
            if (!_active)
                yield break;

            if (_gameEvent == null)
                throw new MissingReferenceException();

            _gameEvent.RaiseEvent();

            IsCompleted = true;
        }
    }
}
