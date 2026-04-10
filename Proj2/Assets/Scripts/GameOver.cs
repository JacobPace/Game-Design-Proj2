using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        
    }
    public void RestartGame() => SceneManager.LoadScene("Title");
    public void QuitGame() => Application.Quit();
}
