using UnityEngine;
using UnityEngine.InputSystem;


interface IInteractable
{
    public void Interact();
}
public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    public LayerMask interactableLayer;

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Ray r = new(InteractorSource.position, InteractorSource.forward);
            if(Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, interactableLayer))
            {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObject))
                {
                    interactObject.Interact();
                }
            }
        }
    }
}
