using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemName = "Item";
    [SerializeField] private string pickupMessage = "You picked something up.";
    [SerializeField] private BedroomPuzzleManager puzzleManager; // add this

    private bool _pickedUp = false;

    public void Interact()
    {
        if (_pickedUp) return;
        _pickedUp = true;

        if (Inventory.Instance == null)
        {
            Debug.LogError("PickupItem: No Inventory found in scene!");
            return;
        }

        Inventory.Instance.AddItem(itemName);
        Debug.Log(pickupMessage);

        // notify puzzle manager if this is the magnifying glass
        if (puzzleManager != null)
        {
            puzzleManager.CollectMagnifyingGlass();
        }

        gameObject.SetActive(false);
    }
}