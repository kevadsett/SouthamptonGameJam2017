using System;
using UnityEngine;

public class StartGameState : GameState
{
	public override void EnterState ()
	{
		GameData.Player1 = CreatePlayer("Player1", -15f, GameData.LimbAnimation, GameData.PlayerParts, GameData.PoseLibrary, KeyCode.Q, KeyCode.W, KeyCode.A, KeyCode.S);
		GameData.Player2 = CreatePlayer("Player2", 15f, GameData.LimbAnimation, GameData.PlayerParts, GameData.PoseLibrary, KeyCode.I, KeyCode.O, KeyCode.K, KeyCode.L);

		GameObject foregroundCanvas = GameObject.Find("ForegroundUICanvas");
		RectTransform foregroundCanvasTransform = foregroundCanvas.GetComponent<RectTransform>();

		GameData.PoseRibbon = GameObject.Instantiate(GameData.PoseRibbonPrefab).GetComponent<PoseRibbon>();
		GameData.PoseRibbon.transform.SetParent(GameData.CanvasTransform, false);
		GameData.PoseRibbon.Setup(GameData.PoseDiagramPrefab);

		GameObject poseRibbonForeground = GameObject.Instantiate(GameData.PoseRibbonForegroundPrefab);
		poseRibbonForeground.transform.SetParent(foregroundCanvasTransform, false);

		GameObject backgroundCanvas = GameObject.Instantiate (GameData.BackgroundCanvasPrefab);
		GameObject backgroundCameraObject = GameObject.Find ("BackgroundCamera");
		backgroundCanvas.GetComponent<Canvas> ().worldCamera =  backgroundCameraObject.GetComponent<Camera>();
	}

	public override void Update ()
	{
		if (GameData.Player1.GetCurrentPose().Matches(GameData.Player2.GetCurrentPose()))
		{
			StateMachine.ChangeState(eGameState.Game);
		}
	}

	public override void ExitState ()
	{
		
	}


	private Poser CreatePlayer(string name, float horizontalPosition, LimbAnimation limbAnimation, PoserParts poserParts, PoseLibrary poseLibrary, params KeyCode[] controls)
	{
		Poser poser = Poser.CreatePoser(true, limbAnimation, poserParts, poseLibrary, controls);
		poser.name = name;
		poser.transform.position = new Vector3(horizontalPosition, 0, 0);

		return poser;
	}
}

