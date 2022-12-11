using UnityEngine;

/// <summary>
/// Supports: Crouch, Jump and Animations.
/// Edited: ManuelFAlonso
/// Source: Brackeys
/// </summary>
[RequireComponent(typeof(CharacterController2D), typeof(Animator))]
public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 40f;

    private CharacterController2D _controller;
    private Animator _animator;

    private float _horizontalMove = 0f;
    private bool _jump = false;
    private bool _crouch = false;

    private Camera _mainCamera;

    void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponent<Animator>();

        _mainCamera = Camera.main;
    }

    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;
        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            _crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            _crouch = false;
        }
    }

    private void FixedUpdate()
    {
        _controller.Move(_horizontalMove * Time.fixedDeltaTime, _crouch, _jump);
        _jump = false;
    }

    public void OnJumping(bool isJumping)
    {
        _animator.SetBool("Jump", isJumping);
    }

    public void OnCrouching(bool isCrouching)
    {
        _animator.SetBool("Crouch", isCrouching);
    }
}
