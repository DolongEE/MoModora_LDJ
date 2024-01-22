using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set; }
    string CurrentStateName { get; set; }

    public void Initialize(State startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(State newState)
    {
        Debug.Log($"{newState.GetType().ToString()}: Enter");
        CurrentState.Exit();
        CurrentState = newState;
        newState.Enter();        
    }
    
}
