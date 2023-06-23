using UnityEngine;

namespace SombraStudios.Systems.Teleport
{
    /// <summary>
    /// Uses the Teleport behaviour with a trigger to create a teleport zone
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class TeleporterTrigger : MonoBehaviour
    {
        [SerializeField] private Teleporter _teleporter;


        void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out ITeleportable teleportable))
            {
                _teleporter.Teleport(teleportable);
            }
        }
    }
}
