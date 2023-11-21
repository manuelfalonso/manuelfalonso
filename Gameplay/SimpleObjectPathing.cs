using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Shared.Gameplay
{
    /// <summary>
    /// Move the transform of an object through a list of waypoints
    /// </summary>
    public class SimpleObjectPathing : MonoBehaviour
    {
        private List<Transform> waypoints;
        private int waypointIndex = 0;
        private float _speed = 10f;


        void Start()
        {
            transform.position = waypoints[waypointIndex].transform.position;
        }

        void Update()
        {
            Move();
        }


        public void SetWaypoints(List<Transform> waypoints)
        {
            this.waypoints = waypoints;
        }


        private void Move()
        {
            if (waypointIndex <= waypoints.Count - 1)
            {
                var targetPosition = waypoints[waypointIndex].transform.position;
                var movementThisFrame = _speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards
                    (transform.position, targetPosition, movementThisFrame);

                if (transform.position == targetPosition)
                {
                    waypointIndex++;
                }
            }
            else
            {
                // Beahviour when reaching last waypoint
            }
        }
    }
}
