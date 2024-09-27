using UnityEngine;

namespace SombraStudios.Shared.Gameplay.PlayerMovement3D
{
    /// <summary>
    /// Apply movement to a game object with keyboard input and Rigidbody
    /// Support Forces and MovePosition methods.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class MovementRigidbody : MonoBehaviour
    {
        [SerializeField] private bool _useForces = false;
        public bool UseForces { 
            get { return _useForces; }
            set
            {
                if (value)
                {
                    _speed = 15f;
                    _useForces = value;
                }
                else
                {
                    _speed = 5f;
                    _useForces = value;
                }            
            } 
        }

        [SerializeField] private float _speed = 5f;

        private Rigidbody _rb;
        private Vector3 _movementInput;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
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
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            UseForces = _useForces;

            if (UseForces)
            {
                // Apply Movement with Forces
                _rb.AddForce(_movementInput.normalized * _speed, ForceMode.Acceleration);
            }
            else
            {
                // Apply Movement with RigidBody
                _rb.MovePosition(_rb.position + _movementInput.normalized * _speed * Time.fixedDeltaTime);
            }
        }
    }
}
