using SombraStudios.Shared.ScriptableObjects.Patterns.Behavioural.Strategy;
using UnityEngine;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// Abstract base class for VFX properties that can be applied to a VFX controller.
    /// </summary>
    public abstract class VFXPropertySO : StrategySO<BaseVFXController>
    {
        [Header(PROPERTIES_TITLE)]
        [Tooltip("Determines whether the VFX property should be reverted on animation end." +
            " with the RevertVFXOnStateExit component attached to the Animator state.")]
        [SerializeField] private bool _revertOnAnimationEnd;

        /// <summary>
        /// Gets or sets a value indicating whether the VFX property should be reverted on animation end with the
        /// <seealso cref= "RevertVFXOnStateExit"/> component attached to the Animator state.
        /// </summary>
        public bool RevertOnAnimationEnd { get => _revertOnAnimationEnd; internal set => _revertOnAnimationEnd = value; }

        /// <summary>
        /// Determines whether the VFX property can be executed on the specified VFX controller.
        /// </summary>
        /// <param name="vFXController">The VFX controller to check.</param>
        /// <returns>True if the VFX property can be executed; otherwise, false.</returns>
        public override bool CanExecute(BaseVFXController vFXController)
        {
            return vFXController != null;
        }

        public override void Execute(BaseVFXController vFXController)
        {
            vFXController.AddToCurrentVFXProperties(this);
        }

        /// <summary>
        /// Tries to revert the VFX property on the specified VFX controller.
        /// </summary>
        /// <param name="vFXController">The VFX controller to revert the VFX property on.</param>
        /// <returns>True if the VFX property was successfully reverted; otherwise, false.</returns>
        public virtual bool TryRevertVFX(BaseVFXController vFXController)
        {
            if (!CanExecute(vFXController)) return false;
            RevertVFX(vFXController);
            return true;
        }

        /// <summary>
        /// Reverts the VFX property on the specified VFX controller.
        /// </summary>
        /// <param name="vFXController">The VFX controller to revert the VFX property on.</param>
        public virtual void RevertVFX(BaseVFXController vFXController)
        {
            vFXController.RemoveFromCurrentVFXProperties(this);
        }
    }
}
