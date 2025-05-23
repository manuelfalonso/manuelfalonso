using System;

namespace SombraStudios.Shared.Systems.Resource
{
    /// <summary>
    /// Serializable class representing a float resource.
    /// </summary>
    [Serializable]
    public class FloatResource : Resource<float>
    {
        /// <summary>
        /// Gets the normalized amount of the resource (amount / maxAmount).
        /// </summary>
        public float AmountNormalized => Amount / MaxAmount;

        /// <summary>
        /// Creates a new instance of the Resource class.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <param name="description">The description of the resource.</param>
        /// <param name="minAmount">The minimum amount of the resource.</param>
        /// <param name="maxAmount">The maximum amount the resource can have.</param>
        /// <param name="initialAmount">The initial amount of the resource.</param>
        public FloatResource(string name, string description, float minAmount, float maxAmount, float initialAmount) :
            base(name, description, minAmount, maxAmount, initialAmount)
        {
            // Initial Resource Amount validation
            if (initialAmount > maxAmount || initialAmount < minAmount)
            {
                throw new ArgumentOutOfRangeException(nameof(initialAmount), InitialAmountExceptionMessage());
            }
        }

        #region Public Methods
        /// <summary>
        /// Increases the resource amount by the specified value.
        /// </summary>
        /// <param name="amountToIncrease">The amount to increase.</param>
        /// <returns>The new amount.</returns>
        public override float IncreaseResource(float amountToIncrease)
        {
            Amount += amountToIncrease;
            if (Amount > MaxAmount) { Amount = MaxAmount; }
            return Amount;
        }

        /// <summary>
        /// Decreases the resource amount by the specified value.
        /// </summary>
        /// <param name="amountToDecrease">The amount to decrease.</param>
        /// <returns>The new amount.</returns>
        public override float DecreaseResource(float amountToDecrease)
        {
            Amount -= amountToDecrease;
            if (Amount < 0f) { Amount = 0f; }
            return Amount;
        }

        /// <summary>
        /// Clears the resource amount, setting it to zero.
        /// </summary>
        /// <returns>The new amount.</returns>
        public override float ClearResource()
        {
            Amount = 0f;
            return Amount;
        }

        /// <summary>
        /// Resets the amount to the initial amount.
        /// </summary>
        /// <returns>The new amount.</returns>
        public override float ResetResource()
        {
            Amount = InitialAmount;
            return Amount;
        }
        #endregion

        /// <summary>
        /// Generates the exception message for the initial amount validation.
        /// </summary>
        /// <returns>The exception message.</returns>
        private string InitialAmountExceptionMessage()
        {
            return $"initialAmount can't be larger than maxAmount or less than minAmount";
        }
    }
}
