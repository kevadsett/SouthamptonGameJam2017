using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eGameState
{
    Load,
    Game
}

public class GameStateMachine
{
    private GameState _currentState;
    private Dictionary<eGameState, GameState> _states;

    public GameStateMachine(Dictionary<eGameState, GameState> states, eGameState defaultState)
    {
        _states = states;

        foreach(var state in states.Values)
        {
            state.StateMachine = this;
        }

        ChangeState(defaultState);
    }

    public void Update()
    {
        _currentState.Update();
    }

    public void ChangeState(eGameState newState)
    {
        
        if(_currentState != null)
        {
            _currentState.ExitState();
            Debug.Log("FROM " + _currentState.GetType().Name);
        }
        Debug.Log("ENTERING " + newState);
        _currentState = _states[newState];
        _currentState.EnterState();
    }
}
