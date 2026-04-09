using StarterAssets;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;


public class Interactor : MonoBehaviour
{
    // set in inspector
    public Transform InteractorSource;
    public float InteractRange;
    public LayerMask interactableLayer;
    public GameObject reticle;
    private Sprite sprReticleOff;
    public Sprite sprReticleOn;
    public Image imgReticle;
    public GameObject pauseMenu;

    private Color clrDefault;
    private Color clrHover = Color.cyan;

    private StarterAssetsInputs _starterInputs;

    private Renderer lastRenderer;
    [ColorUsage(true, true)]
    public Color highlightColor = Color.white;

    private MaterialPropertyBlock _propBlock;
    private static readonly int EmissionColorID = Shader.PropertyToID("_EmissionColor");

    private void Awake()
    {
        if (imgReticle == null)
        {
            Debug.LogError("Interactor: imgReticle is not assigned in the Inspector.");
            return;
        }


        sprReticleOff = imgReticle.sprite;
        clrDefault = imgReticle.color;
        _propBlock = new MaterialPropertyBlock();


    }

    private void Start()
    {
        _starterInputs = UnityEngine.Object.FindFirstObjectByType<StarterAssetsInputs>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
                    SetEmission(lastRenderer, highlightColor);
                }

                if (PlayerInput.Instance.input.Interact.triggered)
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
        if (PlayerInput.Instance.input.Menu.triggered)
        {
            _starterInputs.cursorInputForLook = false;
            //_starterInputs.SetCursorState(false);
            PlayerInput.Instance.DisableInput();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            reticle.SetActive(false);
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void SetEmission(Renderer rend, Color color)
    {
        if (rend == null) return;

        rend.GetPropertyBlock(_propBlock);
        _propBlock.SetColor(EmissionColorID, color);
        rend.SetPropertyBlock(_propBlock);
    }

    void ResetOutline()
    {
        if (lastRenderer != null)
        {
            lastRenderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor(EmissionColorID, Color.black);
            lastRenderer.SetPropertyBlock(_propBlock);
            lastRenderer = null;
        }
    }
}