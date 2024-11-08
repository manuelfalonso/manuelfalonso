namespace SombraStudios.Shared.Animations.AnimationStateEvents
{
    /// <summary>
    /// Enables a component to react to the 'ActionFinished' animation event.
    /// </summary>
    /// <seealso cref="IAnimationStateEventEnterReceiver"/>
    public interface IAnimationStateEventExitReceiver
    {
        /// <summary>
        /// Called when the target animation exits.
        /// </summary>
        /// <param name="label">A label identifying the animation that has finished</param>
        void OnExitAnimationState(AnimationEventData eventData);
    }
}
