using UnityEngine;

public class Book : MonoBehaviour, IInteractable
{
    // Public vars
    public GameObject playerFlashlight;
    public NewFlashlight flashlightScript;

    private bool used = false;
    public void Interact()
    {
        // if flashlight has been taken, book won't do anything anymore
        if (used) return;

        used = true;
        
        // give the player the flashlight
        if (playerFlashlight != null)
        {
            playerFlashlight.SetActive(true);
        }
        // allow the flashlight to work
        if (flashlightScript != null)
        {
            flashlightScript.UnlockFlashlight();
        }

    }
}
