using System;
using UnityEngine;

namespace SombraStudios.Shared.Systems.Resource
{
    /// <summary>
    /// Base class for all resources.
    /// A Resource has a Name and description and it can be
    /// increased, decreased, cleared or resetted.
    /// </summary>
    [Serializable]
    public abstract class Resource<T> where T : struct
    {
        /// <summary>
        /// Gets the name of the resource.
        /// </summary>
        [field: SerializeField]
        public string Name { get; protected set; }

        /// <summary>
        /// Gets the description of the resource.
        /// </summary>
        [field: SerializeField, Multiline]
        public string Description { get; protected set; }

        /// <summary>
        /// Gets the current amount of the resource.
        /// </summary>
        [field: SerializeField]
        public T Amount { get; protected set; }

        /// <summary>
        /// Gets the minimum amount that the resource can have.
        /// </summary>
        [field: SerializeField]
        public T MinAmount { get; protected set; }

        /// <summary>
        /// Gets the maximum amount that the resource can have.
        /// </summary>
        [field: SerializeField]
        public T MaxAmount { get; protected set; }

        /// <summary>
        /// Gets the initial amount of the resource.
        /// </summary>
        [field: SerializeField]
        public T InitialAmount { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the Resource class.
        /// </summary>
        /// <param name="name">The name of the resource.</param>
        /// <param name="description">The description of the resource.</param>
        /// <param name="minAmount">The minimum amount of the resource.</param>
        /// <param name="maxAmount">The maximum amount of the resource.</param>
        /// <param name="initialAmount">The initial amount of the resource.</param>
        protected Resource(string name, string description, T minAmount, T maxAmount, T initialAmount)
        {
            Name = name;
            Description = description;
            Amount = initialAmount;
            MinAmount = minAmount;
            MaxAmount = maxAmount;
            InitialAmount = initialAmount;
        }

        /// <summary>
        /// Returns a string representation of the resource.
        /// </summary>
        /// <returns>A string representing the resource.</returns>
        public override string ToString()
        {
            return $"{this} => {Name}. {Description}";
        }

        /// <summary>
        /// Increases the resource by the specified amount.
        /// </summary>
        /// <param name="amountToIncrease">The amount to increase the resource by.</param>
        /// <returns>The new amount of the resource.</returns>
        public abstract T IncreaseResource(T amountToIncrease);

        /// <summary>
        /// Decreases the resource by the specified amount.
        /// </summary>
        /// <param name="amountToDecrease">The amount to decrease the resource by.</param>
        /// <returns>The new amount of the resource.</returns>
        public abstract T DecreaseResource(T amountToDecrease);

        /// <summary>
        /// Clears the resource amount, setting it to zero.
        /// </summary>
        /// <returns>The new amount of the resource.</returns>
        public abstract T ClearResource();

        /// <summary>
        /// Resets the amount to the initial amount.
        /// </summary>
        /// <returns>The new amount of the resource.</returns>
        public abstract T ResetResource();
    }
}
