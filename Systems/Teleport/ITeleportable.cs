using UnityEngine;

namespace SombraStudios.Shared.Systems.Teleport
{
    /// <summary>
    /// Implent this to create a teleportable object.
    /// For example a physical object that need to considers velocity when teleporting
    /// </summary>
    public interface ITeleportable
    {
        public bool CanTeleport { get; set; }
        public GameObject GameObject { get; set; }

        public abstract void TeleportTo(Transform destination);
    }
}
