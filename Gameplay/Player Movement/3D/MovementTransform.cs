using UnityEngine;

namespace SombraStudios.Shared.Gameplay.PlayerMovement
{
    /// <summary>
    /// Apply movement to a game object with keyboard input and Transform position
    /// </summary>
    public class MovementTransform : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        // Update is called once per frame
        void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector3 movementInput = Vector3.zero;

            // Lateral movement
            movementInput.x = Input.GetAxisRaw("Horizontal");
            // Forward and backward movement
            movementInput.z = Input.GetAxisRaw("Vertical");

            // Vertical movement
            if (Input.GetKey(KeyCode.E))
            {
                movementInput.y = 1;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                movementInput.y = -1;
            }

            // Apply movement with Trasnform
            transform.Translate(movementInput.normalized * _speed * Time.deltaTime);
        }
    }
}
