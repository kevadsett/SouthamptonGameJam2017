using UnityEngine;
using System.Collections.Generic;

public class EntryPoint : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;

    void Start()
    {
        Object.DontDestroyOnLoad(this);

        var gameStates = new Dictionary<eGameState, GameState>
        {
            { eGameState.Load, new LoadGameState() },
			{ eGameState.Game, new MainGameState() },
			{ eGameState.Player1Victory, new GameOverState(eGameOverType.Player1Victory) },
			{ eGameState.Player2Victory, new GameOverState(eGameOverType.Player2Victory) },
			{ eGameState.Draw, new GameOverState(eGameOverType.Draw) }
        };

        _gameStateMachine = new GameStateMachine(gameStates, eGameState.Load);
    }

    void Update()
    {
        _gameStateMachine.Update();
    }
}
