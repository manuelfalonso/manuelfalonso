using System;
using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Events
{
    [CreateAssetMenu(
        fileName = "CheckpointAction", 
        menuName = "Sombra Studios/Systems/Events/Checkpoint Action", 
        order = 13)]
    public class CheckpointActionSO : EventActionSO
    {
        private bool _isCheckpointReached;

        public void SetCheckpointReached() => _isCheckpointReached = true;

        public override IEnumerator StartAction()
        {
            if (!_active)
                yield break;

            yield return new WaitUntil(() => _isCheckpointReached);

            IsCompleted = true;
        }

        public override void OnAfterDeserialize()
        {
            base.OnAfterDeserialize();
            Initialize();
        }

        public override void PauseAction(bool pause) { }

        internal override void Initialize()
        {
            base.Initialize();
            _isCheckpointReached = false;
        }
    }
}
