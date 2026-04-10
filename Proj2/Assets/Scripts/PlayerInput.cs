using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }
    private InputSystem_Actions controls;
    public InputSystem_Actions.PlayerActions input;
    private void Awake()
    {
        Instance = this;
        controls = new InputSystem_Actions();
        controls.Enable();
        input = controls.Player;
        input.Enable();
    }

    private void OnDestroy()
    {
        controls?.Dispose();
        if (Instance == this) Instance = null; 
    }

    public void DisableInput()
    {
        input.Disable();
    }
    public void EnableInput()
    {
        input.Enable();
    }
}
