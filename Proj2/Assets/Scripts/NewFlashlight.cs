using UnityEngine;

public class NewFlashlight : MonoBehaviour
{

    public GameObject ON;
    public GameObject OFF;
    public bool isON;
    public bool hasFlashlight = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ON.SetActive(false);
        OFF.SetActive(true);
        isON = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasFlashlight) return;

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isON)
            {
                ON.SetActive(false);
                OFF.SetActive(true);
            }

            else
            {
                ON.SetActive(true);
                OFF.SetActive(false);
            }

            isON = !isON;
        }
    }

    public void UnlockFlashlight()
    {
        hasFlashlight = true;
    }
}
