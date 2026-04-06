using UnityEngine;

public class Book : MonoBehaviour, IInteractable
{
    // Public vars
    public FlashlightController playerFlashlight;
    public bool used = false;

    public void Interact()
    {

        if (used) return;

        used = true;
        
        // give the player the flashlight
        if (playerFlashlight != null)
        {
            playerFlashlight.UnlockFlashlight();
            Debug.Log("Player grabbed book");
        }
    }
}
