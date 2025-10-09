using System;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State initialState;
    public State currentState;

    private void Start()
    {
        changeState(initialState);
    }

    public void changeState(State state)
    {
        if(currentState == state || state == null)
            return;
        if (currentState != null)
        {
            currentState.ExitState(this);
        }
        
        currentState = state;
        currentState.EnterState(this);
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
            currentState.CheckTransitions(this);
        }
    }
}
