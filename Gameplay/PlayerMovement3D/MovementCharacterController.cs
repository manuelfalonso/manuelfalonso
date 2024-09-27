using UnityEngine;

namespace SombraStudios.Shared.Gameplay.PlayerMovement3D
{
    /// <summary>
    /// Apply movement to a game object with keyboard input and Character Controller
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(CharacterController))]
    public class MovementCharacterController : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        private Rigidbody _rb;
        private CharacterController _cc;
        private Vector3 _movementInput;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            // The object is not be driven by the physics engine
            _rb.isKinematic = true;
            _cc = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            MovementInput();
        }

        private void MovementInput()
        {
            _movementInput = Vector3.zero;

            // Lateral movement
            _movementInput.x = Input.GetAxisRaw("Horizontal");
            // Forward and backward movement
            _movementInput.z = Input.GetAxisRaw("Vertical");

            // Vertical movement
            if (Input.GetKey(KeyCode.E))
            {
                _movementInput.y = 1;
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                _movementInput.y = -1;
            }

            Move();
        }

        private void Move()
        {
            // Apply movement with Character Controller
            _cc.SimpleMove(_movementInput.normalized * _speed);
        }
    }
}
