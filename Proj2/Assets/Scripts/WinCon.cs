using UnityEngine;

public class WinCon : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameManager.State == GameStates.HAS_KEY)
        {
            //Debug.Log("wtf");
            gameManager.ChangeState(GameStates.WON);
        }
    }
}
