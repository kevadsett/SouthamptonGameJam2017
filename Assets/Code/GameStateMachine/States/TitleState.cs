using System;
using UnityEngine;

public class TitleState : GameState
{
	private GameObject _titleScreen;

	public override void EnterState ()
	{
		GameObject canvasObject = GameObject.Find ("UICanvas") as GameObject;
		GameData.CanvasTransform = canvasObject.GetComponent<RectTransform> ();

		_titleScreen = GameObject.Instantiate (GameData.TitleScreenPrefab);
		_titleScreen.transform.SetParent (GameData.CanvasTransform, false);

		GameObject.Instantiate(GameData.AudioPrefab);
	}

	public override void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			StateMachine.ChangeState (eGameState.Start);
		}
		else
		{
			MusicPlayer.Play (0);
		}
	}

	public override void ExitState ()
	{
		GameObject.Destroy (_titleScreen);
		MusicPlayer.FadeOut (0);
	}
}

