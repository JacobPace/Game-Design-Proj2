using System.Collections.Generic;
using UnityEngine;
using State = CandleStates;

public enum CandleStates {
    IDLE,
    LIT,
    DONE
}

public class Candle : MonoBehaviour, IInteractable
{
    // FSM stuff
    public State State {  get; private set; }
    private HashSet<KeyValuePair<State, State>> allowedTransitions; 

    // set in inspector
    public ParticleSystem flame;
    public Light candleLight;
    public CandlePuzzle puzzle;

    void Start()
    {
        this.flame.Stop();
        this.State = State.IDLE;
        allowedTransitions = new()
        {
            new(State.IDLE, State.LIT),
            new(State.LIT, State.IDLE),
            new(State.LIT, State.DONE),
        };
    }

    
    void Update()
    {
        this.StateStay(); // nothing really happens, state machine logix is pretty simple
    }

    public void ChangeState(State newState)
    {
        if (allowedTransitions.Contains(new(State, newState)))
        {
            if (newState != State.DONE)
            {
                this.StateExit();
                this.State = newState;
                this.StateEnter();
            }
            else // dont want candles to turn off when puzzle is done
            {
                State = newState;
                StateEnter();
            }
        }
    }
    
    // Function when player interacts with candle
    public void Interact()
    {
        if (this.State == State.IDLE)
        {
            this.ChangeState(State.LIT);
        }
        // add a lighter item, inverntory, and invertory checker?

    }

    // STATE FUNCTIONS
    public void StateEnter()
    {
        switch (State)
        {
            case State.IDLE:
                // code
                break;
            case State.LIT:
                this.flame.Play();
                this.candleLight.enabled = true;
                // play flame audio?
                this.puzzle.AddInupt(this); // add current candle to player input check
                SoundManager.Play(SoundType.CANDLE_ON);
                break;
            case State.DONE:
                //code
                break;
        }
    }
    public void StateStay()
    {
        switch (State)
        {
            case State.IDLE:
                // code
                break;
            case State.LIT:
                //code
                break;
            case State.DONE:
                //code
                break;
        }
    }
    public void StateExit()
    {
        switch (State)
        {
            case State.IDLE:
                // code
                break;
            case State.LIT:
                this.flame.Stop();
                this.candleLight.enabled = false;
                // extinguish audio?
                break;
            case State.DONE:
                //code
                break;
        }
    }
}
