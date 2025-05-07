using SombraStudios.Shared.Extensions;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace SombraStudios.Shared.Gameplay.PlayerMovement2D
{
    /// <summary>  
    /// Handles top-down 2D player movement, supporting both legacy and new input systems.  
    /// Includes smooth movement and optional rotation towards movement direction.  
    /// </summary>  
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement2DTopDown : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [Header("Settings")]
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _acceleration = 5f;
        [SerializeField] private float _deceleration = 5f;
        [SerializeField] private bool _applyRotation = true;
        [SerializeField] private float _rotationSpeed = 720f; // Degrees per second  

        private Vector2 _inputDirection;
        private Vector2 _currentVelocity;

        /// <summary>  
        /// Initializes the Rigidbody2D reference.  
        /// </summary>  
        void Start()
        {
            this.SafeInit(ref _rigidbody2D);
        }

        /// <summary>  
        /// Reads player input every frame.  
        /// </summary>  
        private void Update()
        {
            ReadInput();
        }

        /// <summary>  
        /// Handles movement and rotation in FixedUpdate for consistent physics updates.  
        /// </summary>  
        void FixedUpdate()
        {
            Move();
        }

        /// <summary>  
        /// Reads input from either the legacy or new input system and calculates the movement direction.  
        /// </summary>  
        private void ReadInput()
        {
#if ENABLE_INPUT_SYSTEM
            // New Input System  
            if (Keyboard.current != null)
            {
                float deltaX = Keyboard.current.aKey.isPressed ? -1 : Keyboard.current.dKey.isPressed ? 1 : 0;
                float deltaY = Keyboard.current.sKey.isPressed ? -1 : Keyboard.current.wKey.isPressed ? 1 : 0;
                _inputDirection = new Vector2(deltaX, deltaY).normalized;
            }
#else
               // Legacy Input System  
               float deltaX = Input.GetAxis("Horizontal");  
               float deltaY = Input.GetAxis("Vertical");  
               _inputDirection = new Vector2(deltaX, deltaY).normalized;  
#endif
        }

        /// <summary>  
        /// Smoothly moves the player based on input direction and applies optional rotation.  
        /// </summary>  
        private void Move()
        {
            // Smoothly interpolate the velocity based on input direction  
            if (_inputDirection.sqrMagnitude > 0.01f)
            {
                _currentVelocity = Vector2.Lerp(_currentVelocity, _inputDirection * _moveSpeed, _acceleration * Time.fixedDeltaTime);
            }
            else
            {
                _currentVelocity = Vector2.Lerp(_currentVelocity, Vector2.zero, _deceleration * Time.fixedDeltaTime);
            }

            if (_applyRotation && _currentVelocity.sqrMagnitude > 0.001f)
            {
                Rotate(_currentVelocity);
            }

            _rigidbody2D.MovePosition(_rigidbody2D.position + _currentVelocity * Time.fixedDeltaTime);
        }

        /// <summary>  
        /// Rotates the player smoothly towards the movement direction.  
        /// </summary>  
        /// <param name="movement">The current movement vector.</param>  
        private void Rotate(Vector2 movement)
        {
            float targetAngle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            // Apply rotation with Lerp for smoothness with rotation speed  
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
        }
    }
}
