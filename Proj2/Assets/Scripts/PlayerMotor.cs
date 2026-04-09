using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour
{
    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float crouchSpeed = 1.5f;

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -15.0f;

    [Header("Crouch Settings")]
    [SerializeField] private float standingHeight = 2.0f;
    [SerializeField] private float crouchingHeight = 1.0f;
    [SerializeField] private float crouchTransitionSpeed = 8.0f;

    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float verticalLookLimit = 80.0f;
    [SerializeField] private Transform cameraHolder;

    private CharacterController controller;

    private Vector3 velocity;
    private float verticalRotation;
    private bool isGrounded;
    private float targetHeight;
    private bool isCrouching;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        targetHeight = standingHeight;
    }

    void Update()
    {
        CheckGrounded();
        HandleCrouchInput();
        HandleCrouch();
        HandleLook();
        HandleMovementAndJump();
    }

    private void CheckGrounded()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;
    }

    private void HandleCrouchInput()
    {
        if (PlayerInput.Instance != null && PlayerInput.Instance.input.Crouch.triggered)
        {
            isCrouching = !isCrouching;
        }
    }

    private void HandleCrouch()
    {
        targetHeight = isCrouching ? crouchingHeight : standingHeight;

        float newHeight = Mathf.Lerp(
            controller.height,
            targetHeight,
            Time.deltaTime * crouchTransitionSpeed
        );

        controller.height = newHeight;
        controller.center = new Vector3(0f, newHeight / 2f, 0f);

        if (cameraHolder != null)
        {
            Vector3 camPos = cameraHolder.localPosition;
            camPos.y = newHeight - 0.1f;
            cameraHolder.localPosition = camPos;
        }
    }

    private void HandleLook()
    {
        if (PlayerInput.Instance == null) return;

        Vector2 look = PlayerInput.Instance.input.Look.ReadValue<Vector2>();

        transform.Rotate(Vector3.up, look.x * mouseSensitivity);

        verticalRotation -= look.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalLookLimit, verticalLookLimit);

        if (cameraHolder != null)
            cameraHolder.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private void HandleMovementAndJump()
    {
        if (PlayerInput.Instance == null) return;

        Vector2 input = PlayerInput.Instance.input.Move.ReadValue<Vector2>();

        Vector3 move = transform.right * input.x + transform.forward * input.y;

        bool isSprinting = PlayerInput.Instance.input.Sprint.IsPressed();
        bool jumpPressed = PlayerInput.Instance.input.Jump.triggered;

        float speed = walkSpeed;
        if (isSprinting && !isCrouching)
            speed = sprintSpeed;
        else if (isCrouching)
            speed = crouchSpeed;

        if (jumpPressed && isGrounded && !isCrouching)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = move * speed;
        finalMove.y = velocity.y;

        controller.Move(finalMove * Time.deltaTime);
    }
}