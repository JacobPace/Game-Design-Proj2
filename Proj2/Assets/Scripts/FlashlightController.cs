using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightController : MonoBehaviour
{
    public Light flashlightLight;
    public GameObject flashlightObject;
    public Key toggleKey = Key.F;

    public bool HasFlashlight { get; private set; } = false;
    public bool IsOn { get; private set; } = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (flashlightLight != null)
        {
            flashlightLight.enabled = false;
        }
        if (flashlightObject != null)
        {
            flashlightObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!HasFlashlight) return;

        if (Keyboard.current[toggleKey].wasPressedThisFrame)
        {
            ToggleFlashlight();
        }
    }

    public void UnlockFlashlight()
    {
        HasFlashlight = true;

        if (flashlightObject != null)
        {
            flashlightObject.SetActive(true);
        }
        Debug.Log("Flashlight acquired");
    }

    public void ToggleFlashlight()
    {
        IsOn = !IsOn;

        if (flashlightLight != null)
        {
            flashlightLight.enabled = IsOn;
        }
        Debug.Log(IsOn ? "Flashlight: ON" : "Flashlight: OFF");
    }
}
