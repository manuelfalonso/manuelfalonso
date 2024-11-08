using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Animations.AnimationStateEvents
{
    /// <summary>
    /// Calls the 'OnAnimationStateBegan' function on any supported component when the target animation begins.
    /// </summary>
    /// <seealso cref="AnimationStateExitEventBehaviour"/>
    public class AnimationStateEnterEventBehaviour : StateMachineBehaviour
    {
        /// <summary>
        /// The list of event data associated with the animation state enter event.
        /// </summary>
        public List<AnimationEventData> Events = new();

        /// <inheritdoc />
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Events.ForEach(e => NotifyReceiver(animator, e));
        }

        /// <summary>
        /// Notifies the receiver that the animation state has begun.
        /// </summary>
        /// <param name="animator">The animator component.</param>
        private void NotifyReceiver(Animator animator, AnimationEventData eventData)
        {
            if (eventData.EventName == null) return;

            if (animator.TryGetComponent(out IAnimationStateEventEnterReceiver receiver))
            {
                receiver.OnEnterAnimationState(eventData);
            }
        }
    }
}
