using System;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Animations.AnimationStateEvents
{
    /// <summary>  
    /// StateMachineBehaviour that handles animation state events.  
    /// </summary>  
    public class AnimationStateEventBehaviour : StateMachineBehaviour
    {
        /// <summary>  
        /// List of animation events to be triggered.  
        /// </summary>  
        public List<AnimationEventBehaviourData> AnimationEvents = new();

        /// <summary>
        /// The current time of the animation state.
        /// </summary>
        private float _currentTime = 0f;

        /// <summary>
        /// The current loop count of the animation state.
        /// </summary>
        private int _currentLoop = 0;

        /// <summary>
        /// The previous loop count of the animation state.
        /// </summary>
        private int _previousLoop = 0;

        /// <inheritdoc />  
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            AnimationEvents.ForEach(e => e.HasTriggered = false);
            _currentLoop = 0;
            _previousLoop = 0;
        }

        /// <inheritdoc />  
        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _currentTime = stateInfo.normalizedTime % 1f;
            _currentLoop = (int)(stateInfo.normalizedTime / 1f);

            foreach (var animationEvent in AnimationEvents)
            {
                if (!animationEvent.HasTriggered && _currentTime >= animationEvent.TriggerTime)
                {
                    NotifyReceiver(animator, animationEvent);
                    animationEvent.HasTriggered = true;
                }

                if (animationEvent.ResetTriggerOnLoop && _currentLoop != _previousLoop)
                {
                    animationEvent.HasTriggered = false;
                }
            }
        }

        /// <summary>  
        /// Notifies the receiver that an animation event has been triggered.  
        /// </summary>  
        /// <param name="animator">The animator component.</param>  
        /// <param name="eventData">The data associated with the animation event.</param>  
        private void NotifyReceiver(Animator animator, AnimationEventBehaviourData eventData)
        {
            if (eventData.Event.EventName == null) return;

            if (animator.TryGetComponent(out IAnimationStateEventReceiver receiver))
            {
                receiver.OnTriggerAnimationStateEvent(eventData.Event);
            }
        }
    }

    /// <summary>  
    /// Data class for animation event behaviour.  
    /// </summary>  
    [Serializable]
    public class AnimationEventBehaviourData
    {
        /// <summary>  
        /// The event data associated with the animation event.  
        /// </summary>  
        [Tooltip("The event data associated with the animation event.")]
        public AnimationEventData Event;

        /// <summary>  
        /// The time at which the event should be triggered, normalized between 0 and 1.  
        /// </summary>
        [Tooltip("The time at which the event should be triggered, normalized between 0 and 1.")]
        [Range(0, 1)]
        public float TriggerTime = 0;

        /// <summary>  
        /// Whether the event trigger should be reset on each loop.  
        /// </summary>  
        [Tooltip("Whether the event trigger should be reset on each loop.")]
        public bool ResetTriggerOnLoop = true;

        /// <summary>  
        /// Whether the event has been triggered.  
        /// </summary>  
        [HideInInspector]
        [Tooltip("Whether the event has been triggered.")]
        public bool HasTriggered = false;
    }
}
