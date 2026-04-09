using UnityEngine;

public class MagnifyingGlassPick : MonoBehaviour, IInteractable
{
    [SerializeField] private BedroomPuzzleManager puzzleManager;

    public void Interact()
    {
        Debug.Log("Magnifying glass interacted!");
        if (puzzleManager == null)
        {
            Debug.LogError("MagnifyingGlassPickup: puzzleManager is not assigned.");
            return;
        }

        puzzleManager.CollectMagnifyingGlass();
        gameObject.SetActive(false);
    }
}

