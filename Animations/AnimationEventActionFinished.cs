using UnityEngine;

namespace SombraStudios.Shared.Animations
{
    /// <summary>
    /// Enables a component to react to the 'ActionFinished' animation event.
    /// </summary>
    /// <seealso cref="IAnimationEventActionBegin"/>
    public interface IAnimationEventActionFinished
    {
        /// <summary>
        /// Called when the target animation exits.
        /// </summary>
        /// <param name="label">A label identifying the animation that has finished</param>
        void ActionFinished(string label);
    }

    /// <summary>
    /// Calls the 'ActionFinished' function on any supported component when the target animation exits.
    /// </summary>
    /// <seealso cref="AnimationEventActionBegin"/>
    public class AnimationEventActionFinished : StateMachineBehaviour
    {
        [SerializeField]
        [Tooltip("A label identifying the animation that has finished.")]
        private string _label;

        /// <inheritdoc />
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var eventReceiver = animator.GetComponentInParent<IAnimationEventActionFinished>();
            eventReceiver?.ActionFinished(_label);
        }
    }
}
