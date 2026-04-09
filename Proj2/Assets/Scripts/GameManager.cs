using UnityEngine;
using System.Collections.Generic;
using State = GameStates;
using UnityEngine.SceneManagement;

public enum GameStates
{
    PLAYING,
    ONE_PUZZLE_DONE,
    ALL_PUZZLES_DONE,
    HAS_KEY,
    WON,
    LOST
}
public class GameManager : MonoBehaviour
{

    public State State { get; private set; }
    private HashSet<KeyValuePair<State, State>> allowedTransitions;

    // Puzzle Tracking
    public int puzzlesCompleted = 0;
    public int totalPuzzles = 2;

    // Reward Objects
    public DrawerAnimation rewardDrawer;
    public GameObject keyObject;
    public DoorOpener finalDoor;

    // End Screens
    public string winSceneName = "WinGame";
    public string loseSceneName = "LoseGame";

    public bool HasKey { get; private set; } = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        State = State.PLAYING;

        allowedTransitions = new()
        {
            new(State.PLAYING, State.ONE_PUZZLE_DONE),
            new(State.ONE_PUZZLE_DONE, State.ALL_PUZZLES_DONE),
            new(State.ALL_PUZZLES_DONE, State.HAS_KEY),
            new(State.HAS_KEY, State.WON),
            new(State.PLAYING, State.LOST),
            new(State.ONE_PUZZLE_DONE, State.LOST),
            new(State.ALL_PUZZLES_DONE, State.LOST),
            new(State.HAS_KEY, State.LOST),
        };

        if (keyObject != null)
        {
            keyObject.SetActive(false);
        }
    }

    public void RegisterPuzzleComplete()
    {
        if (State == State.WON || State == State.LOST) return;

        puzzlesCompleted++;

        Debug.Log("Puzzle completed. Total completed: " + puzzlesCompleted);

        if (puzzlesCompleted == 1)
        {
            ChangeState(State.ONE_PUZZLE_DONE);
        }
        else if (puzzlesCompleted >= totalPuzzles)
        {
            ChangeState(State.ALL_PUZZLES_DONE);
        }
    }

    public void RegisterKeyCollected()
    {
        if (State == State.ALL_PUZZLES_DONE)
        {
            HasKey = true;
            ChangeState(State.HAS_KEY);
        }
    }
    public void TryEscape()
    {
       if (State == State.HAS_KEY)
        {
            ChangeState(State.WON);
        }
       else
        {
            Debug.Log("Player tried to escape without the key");
        }
    }

    public void TimeRanOut()
    {
        if (State != State.WON && State != State.LOST)
        {
            ChangeState(State.LOST);
        }
    }

    public void ChangeState(State newState)
    {
        if (allowedTransitions.Contains(new(State, newState)))
        {
            StateExit();
            State = newState;
            StateEnter();
        }
        else
        {
            Debug.LogWarning($"Invalid transition from {State} to {newState}");
        }
    }

    void StateEnter()
    {
        switch (State)
        {
            case State.PLAYING:
                Debug.Log("Game started.");
                break;

            case State.ONE_PUZZLE_DONE:
                Debug.Log("One puzzle completed.");
                break;

            case State.ALL_PUZZLES_DONE:
                Debug.Log("All puzzles completed. Escape is now available");

                if (rewardDrawer != null)
                {
                    rewardDrawer.OpenDrawer();
                }

                if (keyObject != null)
                {
                    keyObject.SetActive(true);
                }
                break;

            case State.HAS_KEY:
                Debug.Log("Game State: HAS_KEY");

                if (finalDoor != null)
                {
                    finalDoor.OpenDoor();
                }
                break;

            case State.WON:
                Debug.Log("Player escaped");
                SceneManager.LoadScene(winSceneName);
                break;

            case State.LOST:
                Debug.Log("Player lost. Time ran out.");
                SceneManager.LoadScene(loseSceneName);
                break;
        }
    }

    void StateExit()
    {
        switch(State)
        {
            case State.PLAYING:
            case State.ONE_PUZZLE_DONE:
            case State.ALL_PUZZLES_DONE:
            case State.HAS_KEY:
            case State.WON:
            case State.LOST:
                break;
        }
    }
}
