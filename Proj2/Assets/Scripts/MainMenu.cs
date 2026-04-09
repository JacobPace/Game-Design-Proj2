using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject infoScreen;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("LibraryTest");
    }

    public void InfoScreen()
    {
        infoScreen.SetActive(true);
    }
    public void CloseInfoScreen()
    {
        infoScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
