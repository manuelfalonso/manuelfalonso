using UnityEngine;

/// <summary>
/// Edited: Me
/// Source: Brackeys
/// </summary>
[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] private float _runSpeed = 40f;

    private CharacterController2D _controller;

    private float _horizontalMove = 0f;
    private bool _jump = false;
    private bool _crouch = false;

    void Start()
    {
        _controller = GetComponent<CharacterController2D>();
    }

    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _runSpeed;

        if (Input.GetButtonDown("Jump"))
            _jump = true;

        if (Input.GetButtonDown("Crouch"))
            _crouch = true;
        else if (Input.GetButtonUp("Crouch"))
            _crouch = false;
    }

    private void FixedUpdate()
    {
        _controller.Move(_horizontalMove * Time.fixedDeltaTime, _crouch, _jump);
        _jump = false;
    }
}
