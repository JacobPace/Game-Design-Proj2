using UnityEngine;

public class BedroomPuzzleManager : MonoBehaviour
{
    [SerializeField] private HiddenItem[] hiddenItems;

    public bool HasMagnifyingGlass { get; private set; }

    private int collectedCount = 0;

    public void CollectMagnifyingGlass()
    {
        if (HasMagnifyingGlass) return;

        HasMagnifyingGlass = true;
        Debug.Log("Magnifying glass collected.");

        foreach (HiddenItem item in hiddenItems)
        {
            if (item != null)
                item.RevealItem();
        }
    }

    public void CollectHiddenItem(string itemName)
    {
        collectedCount++;
        collectedCount = Mathf.Clamp(collectedCount, 0, hiddenItems.Length);
        Debug.Log("Collected: " + itemName);
        Debug.Log("Progress: " + collectedCount + "/" + hiddenItems.Length);

        if (collectedCount >= hiddenItems.Length)
        {
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        Debug.Log("Bedroom puzzle complete!");
        Debug.Log("A door unlocked somewhere...");
    }
}