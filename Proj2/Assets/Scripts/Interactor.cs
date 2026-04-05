using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


interface IInteractable
{
    public void Interact();
}
public class Interactor : MonoBehaviour
{
    // set in inspector
    public Transform InteractorSource;
    public float InteractRange;
    public LayerMask interactableLayer;
    private Sprite sprReticleOff;
    public Sprite sprReticleOn;
    public Image imgReticle;

    private Color clrDefault;
    private Color clrHover = Color.cyan;

    private Renderer lastRenderer;
    [ColorUsage(true, true)]
    public Color highlightColor = Color.white;

    private void Awake()
    {
        sprReticleOff = imgReticle.sprite;
        clrDefault = imgReticle.color;
    }

    void Update()
    {
        imgReticle.sprite = sprReticleOff;
        imgReticle.color = clrDefault;
        Ray ray = new(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange, interactableLayer))
        {
            imgReticle.sprite = sprReticleOn;
            imgReticle.color = clrHover;
            if (hitInfo.collider.TryGetComponent(out Renderer currentRenderer))
            {
                if (currentRenderer != lastRenderer)
                {
                    ResetOutline();
                    lastRenderer = currentRenderer;
                    lastRenderer.material.SetColor("_EmissionColor", highlightColor);
                }

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObject))
                    {
                        interactObject.Interact();
                    }

                }
            }
        }
        else
        {
            ResetOutline();
        }
    }

    void ResetOutline()
    {
        if (lastRenderer != null)
        {
            lastRenderer.material.SetColor("_EmissionColor", Color.black);
            lastRenderer = null;
        }
    }
}