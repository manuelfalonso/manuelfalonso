using System;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Tutorial
{
    [CreateAssetMenu(fileName = "New Tutorial Action", menuName = "Sombra Studios/Tutorial/Checkpoint Action", order = 3)]
    public class CheckpointAction : TutorialActionSO
    {
        [NonSerialized] private bool _isCheckpointReachedInitialValue = false;

        private bool _isCheckpointReached;


        public override IEnumerator ExecuteAction()
        {
            if (!_active)
                yield break;

            yield return new WaitUntil(() => _isCheckpointReached);

            IsCompleted = true;
        }

        public override void OnAfterDeserialize()
        {
            base.OnAfterDeserialize();
            _isCheckpointReached = _isCheckpointReachedInitialValue;
        }


        public void SetCheckpointReached() => _isCheckpointReached = true;
    }
}
