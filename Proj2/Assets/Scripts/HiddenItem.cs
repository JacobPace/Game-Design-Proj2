using UnityEngine;

public class HiddenItem : MonoBehaviour, IInteractable
{
    [SerializeField] private BedroomPuzzleManager puzzleManager;
    [SerializeField] private string itemName = "Hidden Item";

    private Renderer[] renderers;
    private Collider[] colliders;
    private bool _collected = false;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
        colliders = GetComponentsInChildren<Collider>(true);
        if (puzzleManager == null)
            puzzleManager = FindFirstObjectByType<BedroomPuzzleManager>();
    }

    void Start()
    {
        HideItem();
    }

    public void RevealItem()
    {
        foreach (Renderer r in renderers)
            r.enabled = true;

        foreach (Collider c in colliders)
            c.enabled = true;
    }

    public void HideItem()
    {
        foreach (Renderer r in renderers)
            r.enabled = false;

        foreach (Collider c in colliders)
            c.enabled = false;
    }

    public void Interact()
    {
        if (_collected) return;

        if (puzzleManager == null)
        {
            Debug.LogError("HiddenItem: puzzleManager not assigned on " + gameObject.name);
            return;
        }

        if (!puzzleManager.HasMagnifyingGlass)
        {
            Debug.Log("You need the magnifying glass first.");
            return;
        }

        _collected = true;
        puzzleManager.CollectHiddenItem(itemName);
        gameObject.SetActive(false);
    }
}