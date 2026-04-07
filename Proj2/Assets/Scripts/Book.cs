using UnityEngine;

public class Book : MonoBehaviour, IInteractable
{
    // Public vars
    public GameObject playerFlashlight;
    public NewFlashlight flashlightScript;

    private bool used = false;
    public void Interact()
    {

        if (used) return;

        used = true;
        
        // give the player the flashlight
        if (playerFlashlight != null)
        {
            playerFlashlight.SetActive(true);
        }
        
        if (flashlightScript != null)
        {
            flashlightScript.UnlockFlashlight();
        }

        Debug.Log("You picked up the flashlight.");
    }
}
