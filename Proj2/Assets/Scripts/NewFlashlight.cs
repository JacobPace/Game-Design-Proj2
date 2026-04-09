using UnityEngine;
using UnityEngine.InputSystem;

public class NewFlashlight : MonoBehaviour
{

    public GameObject ON;
    public GameObject OFF;
    public bool isON;
    public bool hasFlashlight = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
        isON = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFlashlight) return;

        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (isON)
            {
                ON.SetActive(false);
                OFF.SetActive(true);
                SoundManager.Play(SoundType.FLASHLIGHT_ON);
            }

            else
            {
                ON.SetActive(true);
                OFF.SetActive(false);
                SoundManager.Play(SoundType.FLASHLIGHT_OFF);
            }

            isON = !isON;
        }
    }

    public void UnlockFlashlight()
    {
        hasFlashlight = true;
    }
}
