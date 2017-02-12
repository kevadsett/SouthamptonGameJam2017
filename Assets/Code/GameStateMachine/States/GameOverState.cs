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

	public GameOverState (eGameOverType type)
	{
		_type = type;
	}

	public override void EnterState ()
	{
        GameObject foregroundCanvas = GameObject.Find("ForegroundUICanvas");
        RectTransform foregroundCanvasTransform = foregroundCanvas.GetComponent<RectTransform>();

        GameOverScreen gameOverScreen = GameObject.Instantiate(GameData.GameOverScreenPrefab).GetComponent<GameOverScreen>();
        gameOverScreen.transform.SetParent(foregroundCanvas.transform, false);

        gameOverScreen.Setup(_type);
	}
}

