using UnityEngine;
using System.Collections.Generic;

public class EntryPoint : MonoBehaviour
{
    private GameStateMachine _gameStateMachine;

    void Start()
    {
        var gameStates = new Dictionary<eGameState, GameState>
        {
            { eGameState.Load, new LoadGameState() },
            { eGameState.Game, new MainGameState() }
        };

        _gameStateMachine = new GameStateMachine(gameStates, eGameState.Load);
    }

    void Update()
    {
        _gameStateMachine.Update();
    }
}
