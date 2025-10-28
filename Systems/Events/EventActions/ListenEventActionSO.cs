using SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Events
{
    [CreateAssetMenu(
        fileName = "ListenEventAction", 
        menuName = "Sombra Studios/Systems/Events/Listen Event Action", 
        order = 16)]
    public class ListenEventActionSO : EventActionSO
    {
        [Header("Event Data")]
        [SerializeField] private VoidEventChannelSO _event;

        public override void PauseAction(bool pause) { }

        public override IEnumerator StartAction()
        {
            if (!_active)
                yield break;

            if (_event == null)
                throw new UnassignedReferenceException();

            _event.OnEventRaised -= CompleteAction;
            _event.OnEventRaised += CompleteAction;

            yield return new WaitUntil(() => IsCompleted);
            
            _event.OnEventRaised -= CompleteAction;
        }
    }
}
