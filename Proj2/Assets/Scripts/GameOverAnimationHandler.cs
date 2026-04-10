using UnityEngine;

public class GameOverAnimationHandler : MonoBehaviour
{
    public GameObject canvas;
    public AudioSource audioSource;
    public AudioClip clip;

    private void Start()
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    public void EnableCanvas()
    {
        canvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
