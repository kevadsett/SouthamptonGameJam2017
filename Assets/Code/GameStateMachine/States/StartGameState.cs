using System;
using UnityEngine;

public class StartGameState : GameState
{

	private GameObject _startScreen;
	public override void EnterState ()
	{
		GameObject startScreenPrefab = Resources.Load<GameObject> ("UI/StartScreen");
		GameObject canvasObject = GameObject.Find ("UICanvas") as GameObject;
		RectTransform canvasTransform = canvasObject.GetComponent<RectTransform> ();

		_startScreen = GameObject.Instantiate (startScreenPrefab);
		_startScreen.transform.SetParent (canvasTransform, false);
	}

	public override void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			StateMachine.ChangeState (eGameState.Game);
		}
	}

	public override void ExitState ()
	{
		GameObject.Destroy (_startScreen);
	}
}

