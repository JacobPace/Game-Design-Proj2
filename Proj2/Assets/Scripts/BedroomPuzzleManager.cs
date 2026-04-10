using UnityEngine;
using TMPro;

public class BedroomPuzzleManager : MonoBehaviour
{
    [Header("Hidden Items")]
    [SerializeField] private HiddenItem[] hiddenItems;

    [Header("Door")]
    [SerializeField] private DoorOpener bedroomDoor;

    [Header("Game Manager")]
    [SerializeField] private GameManager gameManager;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI itemCountText;
    [SerializeField] private TextMeshProUGUI statusText;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip itemPickupSound;
    [SerializeField] private AudioClip magnifyingGlassSound;
    [SerializeField] private AudioClip puzzleCompleteSound;
    [SerializeField] private AudioClip doorOpenSound;

    public bool HasMagnifyingGlass { get; private set; }

    private int collectedCount = 0;
    private bool puzzleComplete = false;

    void Awake()
    {
        if (gameManager == null)
            gameManager = FindFirstObjectByType<GameManager>();
    }

    void Start()
    {
        UpdateUI();

        if (statusText != null)
            statusText.text = "Find the magnifying glass!";
    }

    public void CollectMagnifyingGlass()
    {
        if (HasMagnifyingGlass) return;

        HasMagnifyingGlass = true;
        PlaySound(magnifyingGlassSound);

        if (statusText != null)
            statusText.text = "Now search the room for hidden items!";

        foreach (HiddenItem item in hiddenItems)
        {
            if (item != null)
                item.RevealItem();
        }

        Debug.Log("Magnifying glass collected. Hidden items revealed.");
    }

    public void CollectHiddenItem(string itemName)
    {
        if (puzzleComplete) return;

        collectedCount++;
        collectedCount = Mathf.Clamp(collectedCount, 0, hiddenItems.Length);

        PlaySound(itemPickupSound);
        UpdateUI();

        Debug.Log($"Collected: {itemName} | Progress: {collectedCount}/{hiddenItems.Length}");

        if (collectedCount >= hiddenItems.Length)
            CompletePuzzle();
    }

    private void CompletePuzzle()
    {
        if (puzzleComplete) return;
        puzzleComplete = true;

        PlaySound(puzzleCompleteSound);

        if (statusText != null)
            statusText.text = "All items found! The door is open!";

        Debug.Log("Bedroom puzzle complete!");

        // Open the bedroom door
        if (bedroomDoor != null)
        {
            bedroomDoor.OpenDoor();
            PlaySound(doorOpenSound);
        }
        else
        {
            Debug.LogWarning("BedroomPuzzleManager: bedroomDoor not assigned.");
        }

        // Tell the GameManager a puzzle is done
        if (gameManager != null)
        {
            gameManager.RegisterPuzzleComplete();
        }
        else
        {
            Debug.LogWarning("BedroomPuzzleManager: gameManager not assigned.");
        }
    }

    private void UpdateUI()
    {
        if (itemCountText != null)
            itemCountText.text = $"Items: {collectedCount} / {hiddenItems.Length}";
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
            audioSource.PlayOneShot(clip);
    }
}