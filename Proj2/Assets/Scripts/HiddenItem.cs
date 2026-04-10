using UnityEngine;

public class HiddenItem : MonoBehaviour, IInteractable
{
    [Header("Setup")]
    [SerializeField] private BedroomPuzzleManager puzzleManager;
    [SerializeField] private string itemName = "Hidden Item";

    [Header("Effects")]
    [SerializeField] private ParticleSystem revealEffect;
    [SerializeField] private ParticleSystem collectEffect;

    private Renderer[] renderers;
    private Collider[] colliders;
    private bool _collected = false;
    private bool _revealed = false;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
        colliders = GetComponentsInChildren<Collider>(true);

        if (puzzleManager == null)
            puzzleManager = FindFirstObjectByType<BedroomPuzzleManager>();
    }
    public void RefreshComponents()
    {
        renderers = GetComponentsInChildren<Renderer>(true);
        colliders = GetComponentsInChildren<Collider>(true);
    }

    void Start()
    {
        HideItem();
    }

    public void RevealItem()
    {
        if (_revealed) return;
        _revealed = true;

        foreach (Renderer r in renderers)
            r.enabled = true;

        foreach (Collider c in colliders)
            c.enabled = true;

        if (revealEffect != null)
            revealEffect.Play();

        Debug.Log($"{itemName} revealed!");
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
        if (!Inventory.Instance.HasItem("MagnifyingGlass"))
        {
            Debug.Log("You need the magnifying glass to see this.");
            return;
        }

        _collected = true;
        puzzleManager.CollectHiddenItem(itemName);
        gameObject.SetActive(false);
    }

    public string GetItemName() => itemName;
}