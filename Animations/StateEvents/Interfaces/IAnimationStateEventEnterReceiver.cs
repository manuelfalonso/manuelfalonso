namespace SombraStudios.Shared.Animations.StateEvents
{
    /// <summary>
    /// Enables a component to react to the 'ActionBegin' animation event.
    /// </summary>
    /// <seealso cref="IAnimationStateEventExitReceiver"/>
    public interface IAnimationStateEventEnterReceiver
    {
        /// <summary>
        /// Called when the target animation begins.
        /// </summary>
        /// <param name="label">A label identifying the animation that has began</param>
        void OnEnterAnimationState(AnimationEventData eventData);
    }
}
