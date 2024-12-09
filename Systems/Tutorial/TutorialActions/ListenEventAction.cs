using SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.TutorialSystem
{
    [CreateAssetMenu(
        fileName = "New Tutorial Action", 
        menuName = "Sombra Studios/Tutorial/Listen Event Action", 
        order = 6)]
    public class ListenEventAction : TutorialActionSO
    {
        [Header("Event Data")]
        [SerializeField] private VoidEventChannelSO _gameEvent;


        public override IEnumerator ExecuteAction()
        {
            if (!_active)
                yield break;

            if (_gameEvent == null)
                throw new UnassignedReferenceException();

            _gameEvent.OnEventRaised -= SetIsCompleted;
            _gameEvent.OnEventRaised += SetIsCompleted;

            yield return new WaitUntil(() => IsCompleted);
            
            _gameEvent.OnEventRaised -= SetIsCompleted;
        }

        private void SetIsCompleted() => IsCompleted = true;
    }
}
