using UnityEngine;

namespace SombraStudios.Shared.Systems.Resource
{
    /// <summary>
    /// Abstract base class for resource systems.
    /// </summary>
    /// <typeparam name="T">The type of the resource amount.</typeparam>
    public abstract class ResourceSystem<T> : MonoBehaviour where T : struct
    {
        #region Try Methods
        /// <summary>
        /// Tries to increase the resource amount.
        /// </summary>
        /// <param name="amountToIncrease">The amount to increase.</param>
        /// <param name="result">The result of the operation.</param>
        /// <param name="considerCooldowns">Whether to consider cooldowns.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        protected abstract bool TryIncreaseAmount(T amountToIncrease, out (ResourceSystemData previous, ResourceSystemData current) result,
            bool considerCooldowns = true);

        /// <summary>
        /// Tries to decrease the resource amount.
        /// </summary>
        /// <param name="amountToDecrease">The amount to decrease.</param>
        /// <param name="result">The result of the operation.</param>
        /// <param name="considerCooldowns">Whether to consider cooldowns.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        protected abstract bool TryDecreaseAmount(T amountToDecrease, out (ResourceSystemData previous, ResourceSystemData current) result,
            bool considerCooldowns = true);

        /// <summary>
        /// If emptied, tries to restore the resource amount to a certain percentage.
        /// </summary>
        /// <param name="result">The result of the operation.</param>
        /// <param name="restorePercentage">The percentage to restore.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        protected abstract bool TryRestoreAmount(out (ResourceSystemData previous, ResourceSystemData current) result, float restorePercentage = 1f);

        /// <summary>
        /// Tries to reset the resource amount to it's initial amount.
        /// </summary>
        /// <param name="result">The result of the operation.</param>
        /// <param name="validateEmpty">Whether to validate if the resource is empty.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        protected abstract bool TryResetAmount(out (ResourceSystemData previous, ResourceSystemData current) result, bool validateEmpty = false);

        /// <summary>
        /// Tries to clear the resource amount.
        /// </summary>
        /// <param name="result">The result of the operation.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        protected abstract bool TryClearAmount(out (ResourceSystemData previous, ResourceSystemData current) result);
        #endregion

        #region Can Methods
        /// <summary>
        /// Determines whether the resource amount can be increased.
        /// </summary>
        /// <param name="amountToIncrease">The amount to increase.</param>
        /// <param name="considerCooldowns">Whether to consider cooldowns.</param>
        /// <returns>True if the amount can be increased, otherwise false.</returns>
        protected abstract bool CanIncreaseAmount(T amountToIncrease, bool considerCooldowns);

        /// <summary>
        /// Determines whether the resource amount can be decreased.
        /// </summary>
        /// <param name="amountToDecrease">The amount to decrease.</param>
        /// <param name="considerCooldowns">Whether to consider cooldowns.</param>
        /// <returns>True if the amount can be decreased, otherwise false.</returns>
        protected abstract bool CanDecreaseAmount(T amountToDecrease, bool considerCooldowns);

        /// <summary>
        /// Determines whether the resource amount can be restored.
        /// </summary>
        /// <param name="restorePercentage">The percentage to restore.</param>
        /// <returns>True if the amount can be restored, otherwise false.</returns>
        protected abstract bool CanRestoreAmount(float restorePercentage);

        /// <summary>
        /// Determines whether the resource amount can be reset.
        /// </summary>
        /// <param name="validateEmpty">Whether to validate if the resource is empty.</param>
        /// <returns>True if the amount can be reset, otherwise false.</returns>
        protected abstract bool CanResetAmount(bool validateEmpty);

        /// <summary>
        /// Determines whether the resource amount can be cleared.
        /// </summary>
        /// <returns>True if the amount can be cleared, otherwise false.</returns>
        protected abstract bool CanClearAmount();
        #endregion

        #region Do Methods
        /// <summary>
        /// Increases the resource amount.
        /// </summary>
        /// <param name="amountToIncrease">The amount to increase.</param>
        protected abstract void DoIncreaseAmount(T amountToIncrease);

        /// <summary>
        /// Decreases the resource amount.
        /// </summary>
        /// <param name="amountToDecrease">The amount to decrease.</param>
        protected abstract void DoDecreaseAmount(T amountToDecrease);

        /// <summary>
        /// Restores the resource amount.
        /// </summary>
        /// <param name="restorePercentage">The percentage to restore.</param>
        protected abstract void DoRestoreAmount(float restorePercentage);

        /// <summary>
        /// Resets the resource amount.
        /// </summary>
        protected abstract void DoResetAmount();

        /// <summary>
        /// Clears the resource amount.
        /// </summary>
        protected abstract void DoClearAmount();
        #endregion

        /// <summary>
        /// Starts the regeneration process.
        /// </summary>
        /// <param name="data">The regeneration data.</param>
        protected abstract void StartRegeneration(RegenerationData<T> data);

        /// <summary>
        /// Starts the degeneration process.
        /// </summary>
        /// <param name="data">The degeneration data.</param>
        protected abstract void StartDegeneration(RegenerationData<T> data);

        /// <summary>
        /// Data structure for resource regeneration.
        /// </summary>
        /// <typeparam name="T">The type of the resource amount.</typeparam>
        public struct RegenerationData<T> where T : struct
        {
            /// <summary>
            /// Gets or sets the regeneration time.
            /// </summary>
            public float RegenerationTime { get; set; }

            /// <summary>
            /// Time between regeneration ticks, in seconds.
            /// </summary>
            [Tooltip("Time in seconds between regeneration ticks.")]
            public float RegenerationTick { get; set; }

            /// <summary>
            /// Rate of resource regeneration per tick.
            /// </summary>
            [Tooltip("Amount per regeneration tick.")]
            public T RegenerationAmountRate { get; set; }
        }
    }
}
