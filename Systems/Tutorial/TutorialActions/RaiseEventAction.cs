using SombraStudios.Shared.Patterns.Behavioural.Observer.ScriptableObjects;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.TutorialSystem
{
    [CreateAssetMenu(
        fileName = "New Tutorial Action", 
        menuName = "Sombra Studios/Tutorial/Raise Event Action", 
        order = 5)]
    public class RaiseEventAction : TutorialActionSO
    {
        [Header("Event Data")]
        [SerializeField] protected VoidEventChannelSO _gameEvent;


        public override IEnumerator ExecuteAction()
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
