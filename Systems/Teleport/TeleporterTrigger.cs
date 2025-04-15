using UnityEngine;

namespace SombraStudios.Shared.Systems.Teleport
{
    /// <summary>
    /// Uses the Teleport behaviour with a trigger to create a teleport zone.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TeleporterTrigger : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Teleporter _teleporter;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out ITeleportable teleportable))
            {
                _teleporter.Teleport(teleportable);
            }
        }
    }
}
