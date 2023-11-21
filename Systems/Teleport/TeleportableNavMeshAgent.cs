using UnityEngine;
using UnityEngine.AI;

namespace SombraStudios.Shared.Systems.Teleport
{
    /// <summary>
    /// Example NavMeshAgent component availbale to teleport
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class TeleportableNavMeshAgent : MonoBehaviour, ITeleportable
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private bool _canTeleport;
        [SerializeField] private bool _preserveRotation;

        public bool CanTeleport
        {
            get
            {
                return _canTeleport;
            }
            set
            {
                _canTeleport = value;
            }
        }
        public GameObject GameObject { get; set; }


        private void Start()
        {
            GameObject = gameObject;

            if (_agent == null)
            {
                if (TryGetComponent(out NavMeshAgent agent))
                {
                    _agent = agent;
                }
                else
                {
                    Debug.LogError($"Missing NavMeshAgent", this);
                    enabled = false;
                }
            }
        }


        // ITeleportable
        public void TeleportTo(Transform destination)
        {
            StopAgent();
            transform.position = destination.position;
            if (!_preserveRotation) 
            {
                transform.rotation = destination.rotation;
            }
            EnableAgent();
        }


        private void StopAgent()
        {
            _agent.isStopped = true;
            _agent.ResetPath();
            _agent.enabled = false;
        }

        private void EnableAgent()
        {
            _agent.enabled = true;
        }
    }
}
