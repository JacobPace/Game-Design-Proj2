using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool HasFlashlight { get; private set; } = false;
    public void AddFlashlight()
    {
        HasFlashlight = true;
        Debug.Log("Flashlight added to inventory.");
    }
    
}
