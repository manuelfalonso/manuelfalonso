using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// StateMachineBehaviour that reverts VFX properties on state exit.
    /// </summary>
    public class RevertVFXOnStateExit : StateMachineBehaviour
    {
        /// <summary>
        /// Called when a transition ends and the state machine finishes evaluating this state.
        /// </summary>
        /// <param name="animator">The Animator that is playing the animation.</param>
        /// <param name="stateInfo">Information about the current animator state.</param>
        /// <param name="layerIndex">Index of the layer where the state is played.</param>
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (animator.TryGetComponent(out BaseVFXController vfxController))
            {
                vfxController.RevertVFXOnAnimationEnd();
            }
        }
    }
}
