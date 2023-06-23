using SombraStudios.Utility;
using System.Collections;
using UnityEngine;

namespace SombraStudios.TutorialSystem
{
    [CreateAssetMenu(fileName = "New Tutorial Action", menuName = "SombraStudios/Tutorial/Raise Event Action", order = 5)]
    public class RaiseEventAction : TutorialAction
    {
        [Header("Event Data")]
        [SerializeField] protected GameEventSO _gameEvent;


        public override IEnumerator ExecuteAction()
        {
            if (!_active)
                yield break;

            if (_gameEvent == null)
                throw new MissingReferenceException();

            _gameEvent.Raise();

            IsCompleted = true;
        }
    }
}
