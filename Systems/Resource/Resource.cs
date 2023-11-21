using System;

namespace SombraStudios.Shared.Systems.Resource
{
    [Serializable]
    public class Resource : ResourceBase
    {
        public float Amount { get; private set; }
        public float MaxAmount { get; private set; }
        public float InitialAmount { get; private set; }


        public Resource(string name, string description, float initialAmount, float maxAmount) : base(name, description)
        {
            // Initial Resource Amount validation
            if (initialAmount > maxAmount || initialAmount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(initialAmount), InitialAmountExceptionMessage());
            }

            Amount = initialAmount;
            InitialAmount = initialAmount;
            MaxAmount = maxAmount;
        }


        public void IncreaseAmount(float amountToIncrease)
        {
            IncreaseResource(amountToIncrease);
        }

        public void DecreaseAmount(float amountToDecrease)
        {
            DecreaseResource(amountToDecrease);
        }

        public void ClearAmount()
        {
            ClearResource();
        }

        /// <summary>
        /// Reset the amount to the initial amount
        /// </summary>
        public void ResetAmount()
        {
            ResetResource();
        }


        protected override void IncreaseResource(float amountToIncrease)
        {
            Amount += amountToIncrease;
            if (Amount > MaxAmount) { Amount = MaxAmount; }
        }

        protected override void DecreaseResource(float amountToDecrease)
        {
            Amount -= amountToDecrease;
            if (Amount < 0) { Amount = 0; }
        }

        protected virtual void ResetResource()
        {
            Amount = InitialAmount;
        }

        protected virtual void ClearResource()
        {
            Amount = 0;
        }


        private string InitialAmountExceptionMessage()
        {
            return $"initialAmount can't be larger than maxAmount or less than minAmount";
        }
    }
}
