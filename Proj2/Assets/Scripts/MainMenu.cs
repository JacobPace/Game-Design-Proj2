using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject infoScreen;
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
