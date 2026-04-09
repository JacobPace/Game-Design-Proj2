using UnityEngine;
using System.Collections.Generic;
using System;

public class CandlePuzzle : MonoBehaviour
{
    [Header("Puzzle Settings")]
    public List<Candle> correctOrder = new();
    private readonly List<Candle> playerInput = new();

    [Header("Puzzle Settings")]
    public GameManager gameManager;

    public DrawerAnimation drawerScript;

    public void CheckPuzzle()
    {
        for (int i = 0; i < correctOrder.Count; i++)
        {
            if (playerInput[i] != correctOrder[i])
            {
                ResetPuzzle();
                return;
            }
        }
        foreach (Candle candle in playerInput) {
            candle.ChangeState(CandleStates.DONE);
        }
        // remove debug in post
        Debug.Log("Puzzle Complete!!!!!!!!!!!!!!!");

        if (gameManager != null)
        {
            gameManager.RegisterPuzzleComplete();
        }

        // puzzle is complete, do stuff
        if (drawerScript != null) {
            drawerScript.OpenDrawer();
        }
    }

    public void ResetPuzzle()
    {
        // remove debug in post
        Debug.Log("Wrong order! Resetting candles...");
        foreach (Candle candle in playerInput)
        {
            candle.ChangeState(CandleStates.IDLE);
        }
        playerInput.Clear();
    }

    public void AddInupt(Candle candle)
    {
        if (playerInput.Contains(candle)) return; // error check just in incase
        playerInput.Add(candle);

        // check for puzzle completion once all candles have been lit
        if(playerInput.Count == correctOrder.Count)
        {
            CheckPuzzle();
        }
    }
}
