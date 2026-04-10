using UnityEngine;

public class MagnifyingGlassPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private BedroomPuzzleManager puzzleManager;
    [SerializeField] private ParticleSystem pickupEffect;

    void Awake()
    {
        if (puzzleManager == null)
            puzzleManager = FindFirstObjectByType<BedroomPuzzleManager>();
    }

    public void Interact()
    {
        if (puzzleManager == null)
        {
            Debug.LogError("MagnifyingGlassPickup: puzzleManager not assigned.");
            return;
        }

        if (puzzleManager.HasMagnifyingGlass) return;

        if (pickupEffect != null)
            pickupEffect.Play();

        puzzleManager.CollectMagnifyingGlass();

        GetComponent<Renderer>()?.gameObject.SetActive(false);
        Destroy(gameObject, 0.5f);
    }
}