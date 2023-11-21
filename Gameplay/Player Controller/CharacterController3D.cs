using UnityEngine;

namespace SombraStudios.Shared.Gameplay.PlayerController
{
    /// <summary>
    /// Third Person Character Controller Class
    /// Required: Cinemachine. Create a Free Look Camera with World Space orbit
    /// Optional: Animation Controller
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class CharacterController3D : MonoBehaviour
    {
        [SerializeField] private float playerSpeed = 4.0f;
        [SerializeField] private float jumpHeight = 2.0f;
        [SerializeField] private float fallSpeedLimit = -0.5f;
        [SerializeField] private float turnSmoothTime = 0.1f;
        private bool isGrounded;
        private float turnSmoothVelocity;

	    private Camera gameCamera;
        private CharacterController controller;
        private Vector3 verticalVelocity;
        //private Animator animator;

        const float GRAVITY_VALUE = 9.81f;

        private void Start()
        {
            gameCamera = Camera.main;
            controller = GetComponent<CharacterController>();
            //animator = GetComponent<Animator>();
        }

        private void Update()
        {
            MovePlayer();
        }

        private void MovePlayer()
        {
            isGrounded = controller.isGrounded;

            // Limit fall velocity
            if (isGrounded && verticalVelocity.y < 0)
                verticalVelocity.y = fallSpeedLimit;

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            // Input to move over the X and Z axis 
            // and normalized so we dont move faster diagonally
            Vector3 input = new Vector3(horizontal, 0f, vertical).normalized;

            // animator.SetFloat("MovementX", input.x);
            // animator.SetFloat("MovementZ", input.z);

            // Check if there is input
            if (input.magnitude >= 0.1f)
            {
                // Angle between inputs plus the camera Y axis
                float targetAngle = Mathf.Atan2(input.x, input.z) * Mathf.Rad2Deg + gameCamera.transform.eulerAngles.y;
                // Smooth transition between transform Y axis and targetAngle
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                // Multiply a Quaternion angle and a Vector results in a vector
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * playerSpeed * Time.deltaTime);
            }

            // Jump or apply gravity
            if (isGrounded && Input.GetButtonDown("Jump"))
			    // Jump Speed = sqrt(2Hg) whre H -> height and g -> gravity
                verticalVelocity.y += Mathf.Sqrt(2 * jumpHeight * GRAVITY_VALUE);
            else
                verticalVelocity.y -= GRAVITY_VALUE * Time.deltaTime;

            controller.Move(verticalVelocity * Time.deltaTime);   
        }
    }
}
