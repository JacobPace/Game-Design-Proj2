using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void RestartGame() => SceneManager.LoadScene("Title");
    public void QuitGame() => Application.Quit();
}
