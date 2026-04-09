using UnityEngine;
using System.Collections.Generic;

public class CandleTimerManager : MonoBehaviour
{
    // Timer Settings
    public List<TimerCandle> timerCandles = new();
    public float secondsPerCandle = 180f; // 3 minutes

    // Game Manager 
    public GameManager gameManager;

    // Lose Condition
    public GameObject loseScreen;
    public bool gameOver = false;

    private float elapsedTime = 0f;
    private int extinguishedCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        elapsedTime = 0f;
        extinguishedCount = 0;

        if (loseScreen != null)
            loseScreen.SetActive(false);

        // make sure all the candles start lit
        foreach (TimerCandle candle in timerCandles)
        {
            if (candle != null)
            {
                candle.LightCandle();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return;

        elapsedTime += Time.deltaTime;

        if (extinguishedCount < timerCandles.Count && elapsedTime >= secondsPerCandle * (extinguishedCount + 1))
        {
            ExtinguishNextCandle();
        }
    }

    void ExtinguishNextCandle()
    {
        if (extinguishedCount >= timerCandles.Count) return;

        Debug.Log("Extinguishing candle index: " + extinguishedCount + " at time: " + elapsedTime);

        TimerCandle candle = timerCandles[extinguishedCount];

        if (candle != null && candle.IsLit)
        {
            candle.ExtinguishCandle();
        }

        extinguishedCount++;

        if (extinguishedCount >= timerCandles.Count)
        {
            TriggerLoseCondition();
        }
    }

    void TriggerLoseCondition()
    {
        gameOver = true;
        Debug.Log("Time ran out. Player loses.");

        if (loseScreen != null)
        {
            loseScreen.SetActive(true);
        }

        if (gameManager != null)
        {
            gameManager.TimeRanOut();
        }

        Time.timeScale = 0f;
    }
}
