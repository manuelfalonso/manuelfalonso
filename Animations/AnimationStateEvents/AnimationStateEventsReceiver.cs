using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Animations.AnimationStateEvents
{
    /// <summary>
    /// Receives and handles animation state events.
    /// </summary>
    public class AnimationStateEventsReceiver : MonoBehaviour, IAnimationStateEventEnterReceiver, IAnimationStateEventReceiver,
        IAnimationStateEventExitReceiver
    {
        /// <summary>
        /// List of animation state enter events.
        /// </summary>
        [SerializeField]
        private List<AnimationEvent> _animationStateEnterEvents = new();

        /// <summary>
        /// List of animation state events.
        /// </summary>
        [SerializeField]
        private List<AnimationEvent> _animationStateEvents = new();

        /// <summary>
        /// List of animation state exit events.
        /// </summary>
        [SerializeField]
        private List<AnimationEvent> _animationStateExitEvents = new();

        [Header("Debug")]
        [SerializeField]
        private bool _showLogs;

        /// <summary>
        /// Called when an animation state begins.
        /// </summary>
        /// <param name="eventData">The data associated with the animation event.</param>
        public void OnAnimationStateBegan(AnimationEventData eventData)
        {
            AnimationEvent matchingEvent = _animationStateEnterEvents.Find(e => e.EventName.Value == eventData.EventName.Value);

            AnimationEventData test = CreateAnimationEventData(eventData);

            matchingEvent.EventAction?.Invoke(test);

            if (_showLogs) Debug.Log($"Event Enter triggered: {eventData.EventName}");
        }

        /// <summary>
        /// Called when an animation state event is triggered.
        /// </summary>
        /// <param name="eventData">The data associated with the animation event.</param>
        public void OnAnimationStateEventTriggered(AnimationEventData eventData)
        {
            AnimationEvent matchingEvent = _animationStateEvents.Find(e => e.EventName.Value == eventData.EventName.Value);

            AnimationEventData test = CreateAnimationEventData(eventData);

            matchingEvent.EventAction?.Invoke(test);

            if (_showLogs) Debug.Log($"Event triggered: {eventData.EventName}");
        }

        /// <summary>
        /// Called when an animation state exits.
        /// </summary>
        /// <param name="eventData">The data associated with the animation event.</param>
        public void OnAnimationStateExited(AnimationEventData eventData)
        {
            AnimationEvent matchingEvent = _animationStateExitEvents.Find(e => e.EventName.Value == eventData.EventName.Value);

            AnimationEventData test = CreateAnimationEventData(eventData);

            matchingEvent.EventAction?.Invoke(test);

            if (_showLogs) Debug.Log($"Event Exit triggered: {eventData.EventName}");
        }

        /// <summary>
        /// Creates a new instance of AnimationEventData based on the provided event data.
        /// </summary>
        /// <param name="eventData">The data associated with the animation event.</param>
        /// <returns>A new instance of AnimationEventData.</returns>
        private static AnimationEventData CreateAnimationEventData(AnimationEventData eventData)
        {
            return new AnimationEventData
            {
                EventName = eventData.EventName,
                FloatParameter = eventData.FloatParameter,
                IntParameter = eventData.IntParameter,
                StringParameter = eventData.StringParameter,
                ObjectReferenceParameter = eventData.ObjectReferenceParameter,
            };
        }
    }
}
