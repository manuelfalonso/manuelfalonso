using UnityEngine;
using UnityEngine.AI;

namespace SombraStudios.Shared.Systems.Teleport
{
    /// <summary>
    /// Example NavMeshAgent component available to teleport.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class TeleportableNavMeshAgent : MonoBehaviour, ITeleportable
    {
        [Header("References")]
        [SerializeField] private NavMeshAgent _agent;

        [Header("Settings")]
        [SerializeField] private bool _canTeleport;
        [SerializeField] private bool _preserveRotation;

        public bool CanTeleport
        {
            get => _canTeleport;
            set => _canTeleport = value;
        }

        public GameObject GameObject => gameObject;

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void Awake()
        {
            if (_agent == null && !TryGetComponent(out _agent))
            {
                Debug.LogError("Missing NavMeshAgent", this);
                enabled = false;
            }
        }

        public void TeleportTo(Transform destination)
        {
            StopAgent();
            transform.SetPositionAndRotation(
                destination.position,
                _preserveRotation ? transform.rotation : destination.rotation);
            EnableAgent();
        }

        /// <summary>
        /// Stops the NavMeshAgent.
        /// </summary>
        private void StopAgent()
        {
            _agent.isStopped = true;
            _agent.ResetPath();
            _agent.enabled = false;
        }

        /// <summary>
        /// Enables the NavMeshAgent.
        /// </summary>
        private void EnableAgent()
        {
            _agent.enabled = true;
        }
    }
}
