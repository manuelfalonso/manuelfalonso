using SombraStudios.Shared.ScriptableObjects.Patterns.Behavioural.Strategy;

namespace SombraStudios.Shared.VFX.PropertySO
{
    /// <summary>
    /// Abstract base class for VFX properties that can be applied to a VFX controller.
    /// </summary>
    public abstract class VFXPropertySO : StrategySO<BaseVFXController>
    {
        /// <summary>
        /// Determines whether the VFX property can be executed on the specified VFX controller.
        /// </summary>
        /// <param name="vFXController">The VFX controller to check.</param>
        /// <returns>True if the VFX property can be executed; otherwise, false.</returns>
        public override bool CanExecute(BaseVFXController vFXController)
        {
            return vFXController != null;
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
        public abstract void RevertVFX(BaseVFXController vFXController);
    }
}
