using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eGameState
{
	Load,
	Game,
	Player1Victory,
	Player2Victory,
	Draw,
	InterimScore
}

public class GameStateMachine
{
    private GameState _currentState;
    private Dictionary<eGameState, GameState> _states;
	private Stack<GameState> _stateStack = new Stack<GameState>();

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
		_currentState = _stateStack.Peek ();
        _currentState.Update();
    }

    public void ChangeState(eGameState newState)
    {
		if (_stateStack.Count > 0)
		{
			PopState ();
		}
		PushState (newState);
    }

	public void PushState(eGameState newState)
	{
		GameState pushedState = _states [newState];
		_stateStack.Push (pushedState);
		pushedState.EnterState ();
	}

	public void PopState()
	{
		GameState poppedState = _stateStack.Pop ();
		poppedState.ExitState ();
	}
}
