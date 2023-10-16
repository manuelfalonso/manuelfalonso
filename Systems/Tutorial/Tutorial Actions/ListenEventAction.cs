using SombraStudios.Utility.Events;
using System.Collections;
using UnityEngine;

namespace SombraStudios.TutorialSystem
{
    [CreateAssetMenu(fileName = "New Tutorial Action", menuName = "Sombra Studios/Tutorial/Listen Event Action", order = 6)]
    public class ListenEventAction : TutorialAction
    {
        [Header("Event Data")]
        [SerializeField] private GameEvent _gameEvent;
        [SerializeField] private GameEventScriptableObjectListener _gameEventListener;


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
