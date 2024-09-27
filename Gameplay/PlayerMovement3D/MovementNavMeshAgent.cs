using UnityEngine;
using UnityEngine.AI;

namespace SombraStudios.Shared.Gameplay.PlayerMovement3D
{
    /// <summary>
    /// Apply movement to a game object with mouse input and NavMeshAgent
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class MovementNavMeshAgent : MonoBehaviour
    {
        private NavMeshAgent _nma;

        // Start is called before the first frame update
        void Start()
        {
            _nma = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            ClickCheck();
        }

        private void ClickCheck()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Move(hit.point);
                }
            }
        }

        private void Move(Vector3 position)
        {
            _nma.SetDestination(position);
        }
    }
}
