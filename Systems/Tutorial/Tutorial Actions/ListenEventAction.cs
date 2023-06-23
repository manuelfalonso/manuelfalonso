using SombraStudios.Utility;
using System.Collections;
using UnityEngine;

namespace SombraStudios.TutorialSystem
{
    [CreateAssetMenu(fileName = "New Tutorial Action", menuName = "SombraStudios/Tutorial/Listen Event Action", order = 6)]
    public class ListenEventAction : TutorialAction
    {
        [Header("Event Data")]
        [SerializeField] private GameEventSO _gameEvent;
        [SerializeField] private GameEventSOListener _gameEventListener;


        public override IEnumerator ExecuteAction()
        {
            if (!_active)
                yield break;

            if (_gameEvent == null || _gameEventListener == null)
                throw new UnassignedReferenceException();

            _gameEventListener.Response.AddListener(SetIsCompleted);

            yield return new WaitUntil(() => IsCompleted);

            _gameEventListener.Response.RemoveListener(SetIsCompleted);
        }

        private void SetIsCompleted() => IsCompleted = true;
    }
}
