using UnityEngine;

public class FlashlightReveal : MonoBehaviour
{
    public GameObject hiddenNumber;
    public NewFlashlight flashlight;
    public Transform flashlightTransform;
    public float revealDistance = 5f;
    public LayerMask revealLayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (hiddenNumber != null)
        {
            hiddenNumber.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (hiddenNumber == null || flashlight == null || flashlightTransform == null)
            return;

        bool shouldShow = false;

        if (flashlight.hasFlashlight && flashlight.isON)
        {
            Ray ray = new Ray(flashlightTransform.position, flashlightTransform.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, revealDistance))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    shouldShow = true;
                }
            }
        }

        hiddenNumber.SetActive(shouldShow);
    }
}
