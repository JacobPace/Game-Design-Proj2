using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour
{
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
        // only check imgReticle here — other refs not available yet in Awake
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
        if (_starterInputs == null)
            Debug.LogError("Interactor: StarterAssetsInputs not found in scene.");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // safety guard — exit if critical refs are missing
        if (imgReticle == null || PlayerInput.Instance == null || InteractorSource == null) return;

        imgReticle.sprite = sprReticleOff;
        imgReticle.color = clrDefault;

        Ray ray = new(InteractorSource.position, InteractorSource.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange, interactableLayer))
        {
            imgReticle.sprite = sprReticleOn;
            imgReticle.color = clrHover;

            // HIGHLIGHT — separate from interaction
            Renderer currentRenderer = hitInfo.collider.GetComponentInChildren<Renderer>();
            if (currentRenderer != null)
            {
                if (currentRenderer != lastRenderer)
                {
                    ResetOutline();
                    lastRenderer = currentRenderer;
                    SetEmission(lastRenderer, highlightColor);
                }
            }

            // INTERACT — runs regardless of renderer
            if (PlayerInput.Instance.input.Interact.triggered)
            {
                IInteractable interactObject = hitInfo.collider.GetComponentInParent<IInteractable>();
                if (interactObject != null)
                {
                    interactObject.Interact();
                }
                else
                {
                    Debug.Log("No IInteractable found on: " + hitInfo.collider.gameObject.name);
                }
            }
        }
        else
        {
            ResetOutline();
        }

        // PAUSE MENU — completely outside the raycast block
        if (PlayerInput.Instance.input.Menu.triggered)
        {
            if (_starterInputs != null)
                _starterInputs.cursorInputForLook = false;
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