using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum eGameState
{
	Load,
	Game
}

static class GameStateMachine {
	private static Dictionary<eGameState, IGameState> _states = new Dictionary<eGameState, IGameState> ()
	{
		{ eGameState.Load, new LoadGameState() },
		{ eGameState.Game, new MainGameState() }
	};
	private static IGameState _currentState;
	
	public static void Update () {
		if (_currentState != null)
		{
			_currentState.Update ();
		}
	}

	public static void ChangeState(eGameState newState)
	{
		if (_currentState != null)
		{
			_currentState.ExitState ();
		}
		_currentState = _states[newState];
		_currentState.EnterState ();
	}
}
