using UnityEngine;
using System.Collections.Generic;
using State = BookStates;
using Unity.VisualScripting;

public enum BookStates
{
    IDLE,
    PRESSED,
    DONE
}
public class ShelfBook : MonoBehaviour, IInteractable
{
    // FSM Stuff
    public State State { get; private set; }
    private HashSet<KeyValuePair<State, State>> allowedTransitions;

    public int bookNumber;
    public BookshelfPuzzle puzzle;

    private Vector3 startLocalPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startLocalPos = transform.localPosition;
        State = State.IDLE;

        allowedTransitions = new()
        {
            new(State.IDLE, State.PRESSED),
            new(State.PRESSED, State.IDLE),
            new(State.PRESSED, State.DONE),
        };
    }

    // Update is called once per frame
    void Update()
    {
        StateStay();
    }

    public void ChangeState(State newState)
    {
        Debug.Log($"{name}: trying to change from {State} to {newState}");

        if (allowedTransitions.Contains(new(State, newState)))
        {
            if (newState != State.DONE)
            {
                StateExit();
                State = newState;
                StateEnter();
            }
            else
            {
                State = newState;
                StateEnter();
            }
        }
        else
        {
            Debug.LogWarning($"{name}: invalid transition from {State} to {newState}");
        }
    }

    public void Interact()
    {
        Debug.Log($"{name}: Interact() called. Current state = {State}");

        if (State == State.IDLE)
        {
            ChangeState(State.PRESSED);
        }     
    }

    // STATE FUNCTIONS
    public void StateEnter()
    {
        switch (State)
        {
            case State.IDLE:
                transform.localPosition = startLocalPos;
                Debug.Log($"{name}: Entered IDLE");
                break;

            case State.PRESSED:
                transform.localPosition = startLocalPos + new Vector3(-0.05f, 0f, 0f);
                Debug.Log($"{name}: entered PRESSED, sending input to puzzle");
                puzzle.AddInput(this);
                break;

            case State.DONE:
                // keep pressed in when puzzle is solved
                Debug.Log($"{name}: entered DONE");
                break;
        }
    }

    public void StateStay()
    {
        switch (State)
        {
            case State.IDLE:
                break;

            case State.PRESSED:
                break;

            case State.DONE:
                break;
        }
    }

    public void StateExit()
    {
        switch (State)
        {
            case State.IDLE:
                break;

            case State.PRESSED:
                // when resetting, book will pop out in IDLE StateEnter
                break;

            case State.DONE:
                break;
        }
    }
}
