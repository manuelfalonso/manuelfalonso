namespace SombraStudios.Shared.Systems.Resource
{
    /// <summary>
    /// Represents data related to the state of a resource system, including
    /// current amount, maximum amount, percentage, and various flags indicating
    /// specific conditions such as being at maximum amount or in cooldown.
    /// </summary>
    public class ResourceSystemData
    {
        /// <summary>
        /// Gets or sets the current amount of the resource.
        /// </summary>
        public float Amount { get; set; }

        /// <summary>
        /// Gets or sets the maximum amount the resource can reach.
        /// </summary>
        public float MaxAmount { get; set; }

        /// <summary>
        /// Gets or sets the percentage of the resource's current amount relative to its maximum amount.
        /// </summary>
        public float Percent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the resource is at its maximum amount.
        /// </summary>
        public bool IsMaxAmount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the resource is empty.
        /// </summary>
        public bool IsEmptyAmount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the resource is immutable.
        /// </summary>
        public bool Immutable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the resource is currently in a cooldown state.
        /// </summary>
        public bool IsInCooldown { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="ResourceSystemData"/> that is a copy of the current instance.
        /// </summary>
        /// <returns>A new <see cref="ResourceSystemData"/> instance that is a copy of this instance.</returns>
        public ResourceSystemData Clone()
        {
            return new ResourceSystemData
            {
                Amount = Amount,
                MaxAmount = MaxAmount,
                Percent = Percent,
                IsMaxAmount = IsMaxAmount,
                IsEmptyAmount = IsEmptyAmount,
                Immutable = Immutable,
                IsInCooldown = IsInCooldown
            };
        }
    }
}
