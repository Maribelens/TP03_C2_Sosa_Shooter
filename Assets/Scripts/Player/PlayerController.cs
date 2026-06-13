using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement speeds:")] 
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float crounchSpeed = 2.5f;

    [Header("Jump:")] 
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -10f;

    [Header("Crounch:")] 
    [SerializeField] private float standHeight = 2f;
    [SerializeField] private float crounchHeight = 1f;

    [Header("Ground Check:")] 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;

    private CharacterController _controller;
    private Vector3 _velocity;
    private bool _isCrounching;
    private bool _isGrounded;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        HandleCrounch();
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        float currentSpeed = GetCurrentSpeed();
        _controller.Move(move * currentSpeed * Time.deltaTime);
    }

    private float GetCurrentSpeed()
    {
        if (_isCrounching) return crounchSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) return runSpeed;
        return walkSpeed;
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded && !_isCrounching)
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void HandleCrounch()
    {
        if (!Input.GetKeyDown(KeyCode.C)) return;

        _isCrounching = !_isCrounching;
        _controller.height = _isCrounching ? crounchHeight : standHeight;
    }

    private void ApplyGravity()
    {
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = -2f;

        _velocity.y += gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}    
