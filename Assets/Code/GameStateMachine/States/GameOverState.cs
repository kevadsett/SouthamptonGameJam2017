using System;
using UnityEngine;

public enum eGameOverType
{
	Player1Victory,
	Player2Victory,
	Draw
}

public class GameOverState : GameState
{
    eGameOverType _type;
	GameOverScreen _gameOverScreen;

    public GameOverState (eGameOverType type)
	{
		_type = type;
	}

	public override void EnterState ()
	{
        GameObject foregroundCanvas = GameObject.Find("ForegroundUICanvas");
        RectTransform foregroundCanvasTransform = foregroundCanvas.GetComponent<RectTransform>();

        _gameOverScreen = GameObject.Instantiate(GameData.GameOverScreenPrefab).GetComponent<GameOverScreen>();
        _gameOverScreen.transform.SetParent(foregroundCanvas.transform, false);
        _gameOverScreen.Setup(_type);
	}

    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StateMachine.PopState();
        }
    }

    public override void ExitState()
    {
        GameObject.Destroy(_gameOverScreen.gameObject);
    }
}

