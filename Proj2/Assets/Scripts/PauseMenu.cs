using UnityEngine;
using StarterAssets;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject reticle;
    private StarterAssetsInputs _starterInputs;

    private void Start()
    {
        _starterInputs = Object.FindFirstObjectByType<StarterAssetsInputs>();
    }
    public void UnPauseGame()
    {
        pauseMenu.SetActive(false);
        reticle.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        PlayerInput.Instance.EnableInput();
        _starterInputs.cursorInputForLook = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
