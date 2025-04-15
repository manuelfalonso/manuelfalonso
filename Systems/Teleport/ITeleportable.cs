using UnityEngine;

namespace SombraStudios.Shared.Systems.Teleport
{
    /// <summary>
    /// Interface for creating a teleportable object.
    /// For example, a physical object that needs to consider velocity when teleporting.
    /// </summary>
    public interface ITeleportable
    {
        /// <summary>
        /// Gets or sets a value indicating whether the object can teleport.
        /// </summary>
        bool CanTeleport { get; set; }

        /// <summary>
        /// Gets the GameObject associated with the teleportable object.
        /// </summary>
        GameObject GameObject { get; }

        /// <summary>
        /// Teleports the object to the specified destination.
        /// </summary>
        /// <param name="destination">The destination transform to teleport to.</param>
        void TeleportTo(Transform destination);
    }
}
