using UnityEngine;

namespace SombraStudios.Shared.Gameplay
{
    /// <summary>
    /// Move a position towards another one methods
    /// </summary>
    public class MoveTowards : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Transform _target;

        void Update()
        {
            //Vector3MoveTowards();
            //PositionMoveTowards();
        }

        private void Vector3MoveTowards()
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);
        }

        private void PositionMoveTowards()
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            transform.position = transform.position + direction * speed * Time.deltaTime;
        }
    }
}
