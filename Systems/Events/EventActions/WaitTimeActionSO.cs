using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Events
{
    /// <summary>
    /// Action for waiting time in seconds
    /// </summary>
    [CreateAssetMenu(
        fileName = "WaitTimeAction", 
        menuName = "Sombra Studios/Systems/Events/Wait Time Action", 
        order = 11)]
    public class WaitTimeActionSO : EventActionSO
    {
        [Header(EventSequencer.SETTINGS_TITLE)]
        [Tooltip("In seconds, how long to wait before completing the action")]
        [SerializeField] private float _waitTime;

        public override void PauseAction(bool pause) { }

        public override IEnumerator StartAction()
        {
            if (!_active)
                yield break;

            yield return new WaitForSeconds(_waitTime);

            IsCompleted = true;
        }
    }
}
