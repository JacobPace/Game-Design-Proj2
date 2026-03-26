using UnityEngine;
using UnityEngine.InputSystem;


interface IInteractable
{
    public void Interact();
}
public class Interactor : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Transform InteractorSource;
    public float InteractRange;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.eKey.wasPressedThisFrame)
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
            if(Physics.Raycast(r, out RaycastHit hitInfo, InteractRange))
            {
                if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObject))
                {
                    interactObject.Interact();
                }
            }
        }
    }
}
