using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CandleTimerManager : MonoBehaviour
{
    // Timer Settings
    public List<TimerCandle> timerCandles = new();
    public float secondsPerCandle = 120f; // 2 minutes

    // Game Manager 
    public GameManager gameManager;

    // Lose Condition
    //public GameObject loseScreen;
    public bool gameOver = false;

    private float elapsedTime = 0f;
    private int extinguishedCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        elapsedTime = 0f;
        extinguishedCount = 0;

        // make sure all the candles start lit
        foreach (TimerCandle candle in timerCandles)
        {
            if (candle != null)
            {
                candle.LightCandle();
            }
        }
        StartCoroutine(TimerFunc());
    }


    void TriggerLoseCondition()
    {
        gameOver = true;
    }

    IEnumerator TimerFunc()
    {
        foreach (TimerCandle candle in timerCandles)
        {
            yield return new WaitForSeconds(60f);
            if (candle != null)
            {
                candle.ExtinguishCandle();
            }
        }
        gameManager.ChangeState(GameStates.LOST);
    }
}
