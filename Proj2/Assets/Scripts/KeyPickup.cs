using Unity.VisualScripting;
using UnityEngine;

public class KeyPickup : MonoBehaviour, IInteractable
{
    // References
    public GameManager gameManager;
    public GameObject heldKeyObject;

    private bool pickedUp = false;

    public void Interact()
    {
        if (pickedUp) return;

        pickedUp = true;

        Debug.Log("Key picked up!");

        // Show the key in front of camera
        if (heldKeyObject != null)
        {
            heldKeyObject.SetActive(true);
        }

        if (gameManager != null)
        {
            gameManager.RegisterKeyCollected();
        }

        // hide the world key from the drawer
        gameObject.SetActive(false);
    }
}
