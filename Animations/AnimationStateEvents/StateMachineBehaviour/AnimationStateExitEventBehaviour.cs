using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Animations.AnimationStateEvents
{
    /// <summary>
    /// Calls the 'OnAnimationStateExited' function on any supported component when the target animation exits.
    /// </summary>
    /// <seealso cref="AnimationStateEnterEventBehaviour"/>
    public class AnimationStateExitEventBehaviour : StateMachineBehaviour
    {
        /// <summary>
        /// The list of event data associated with the animation state exit event.
        /// </summary>
        public List<AnimationEventData> Events = new();

        /// <inheritdoc />
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Events.ForEach(e => NotifyReceiver(animator, e));
        }

        /// <summary>
        /// Notifies the receiver that the animation state has exited.
        /// </summary>
        /// <param name="animator">The animator component.</param>
        private void NotifyReceiver(Animator animator, AnimationEventData eventData)
        {
            if (eventData.EventName == null) return;

            if (animator.TryGetComponent(out IAnimationStateEventExitReceiver receiver))
            {
                receiver.OnAnimationStateExited(eventData);
            }
        }
    }
}
