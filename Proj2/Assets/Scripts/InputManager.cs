using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions inputActions;

    public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }
    public bool IsSprinting { get; private set; }
    public bool IsCrouching { get; private set; }
    public bool JumpTriggered { get; private set; }

    void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.Player.SetCallbacks(this);
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void OnDestroy()
    {
        if (inputActions != null)
            inputActions.Dispose();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Interact held — examine object / pick up item");
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Attack pressed");
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
            IsSprinting = true;

        if (context.canceled)
            IsSprinting = false;
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsCrouching = !IsCrouching;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            JumpTriggered = true;
        }
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Previous item");
        }
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Next item");
        }
    }

    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Menu pressed");
        }
    }

    void LateUpdate()
    {
        JumpTriggered = false;
    }
}