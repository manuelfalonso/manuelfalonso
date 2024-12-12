using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Animations.StateEvents
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

        /// <summary>
        /// Whether to show debug logs.
        /// </summary>
        [Header("Debug")]
        [SerializeField]
        private bool _showLogs;

        /// <summary>
        /// Called when an animation state begins.
        /// </summary>
        /// <param name="eventData">The data associated with the animation event.</param>
        public void OnEnterAnimationState(AnimationEventData eventData)
        {
            TriggerEvent(_animationStateEnterEvents, eventData);

            if (_showLogs)
                Utility.Loggers.Logger.Log($"Event Enter triggered: {eventData.EventName}", this);
        }

        /// <summary>
        /// Called when an animation state event is triggered.
        /// </summary>
        /// <param name="eventData">The data associated with the animation event.</param>
        public void OnTriggerAnimationStateEvent(AnimationEventData eventData)
        {
            TriggerEvent(_animationStateEvents, eventData);

            if (_showLogs)
                Utility.Loggers.Logger.Log($"Event triggered: {eventData.EventName}", this);
        }

        /// <summary>
        /// Called when an animation state exits.
        /// </summary>
        /// <param name="eventData">The data associated with the animation event.</param>
        public void OnExitAnimationState(AnimationEventData eventData)
        {
            TriggerEvent(_animationStateExitEvents, eventData);

            if (_showLogs)
                Utility.Loggers.Logger.Log($"Event Exit triggered: {eventData.EventName}", this);
        }

        /// <summary>
        /// Triggers the matching event from the provided list.
        /// </summary>
        /// <param name="eventsList">The list of animation events.</param>
        /// <param name="eventData">The data associated with the animation event.</param>
        private void TriggerEvent(List<AnimationEvent> eventsList, AnimationEventData eventData)
        {
            AnimationEvent matchingEvent = FindMatchingEvent(eventsList, eventData);
            matchingEvent.EventAction?.Invoke(eventData);
        }

        /// <summary>
        /// Finds the matching event from the provided list.
        /// </summary>
        /// <param name="eventsList">The list of animation events.</param>
        /// <param name="eventData">The data associated with the animation event.</param>
        /// <returns>The matching animation event.</returns>
        private AnimationEvent FindMatchingEvent(List<AnimationEvent> eventsList, AnimationEventData eventData)
        {
            return eventsList.Find(e => e.EventName.Value == eventData.EventName.Value);
        }
    }
}
