namespace SombraStudios.Shared.Animations.AnimationStateEvents
{
    /// <summary>
    /// Interface for receiving animation state events.
    /// </summary>
    public interface IAnimationStateEventReceiver
    {
        /// <summary>
        /// Method to be called when an animation state event is triggered.
        /// </summary>
        /// <param name="eventData">The data associated with the animation event.</param>
        void OnTriggerAnimationStateEvent(AnimationEventData eventData);
    }
}
