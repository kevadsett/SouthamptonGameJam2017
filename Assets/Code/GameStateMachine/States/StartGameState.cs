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

		GameObject scoreLivesPrefab = GameObject.Instantiate (GameData.ScoreLivesPrefab);
		scoreLivesPrefab.transform.SetParent (foregroundCanvas.transform, false);

        GameData.PoseManager.GeneratePosesForRound(GameData.WaveCount);
	}

	public override void Update ()
	{
        float dt = Time.deltaTime;
        GameData.Player1.UpdatePose(dt);
        GameData.Player2.UpdatePose(dt);

        Pose firstPose = GameData.PoseManager.FirstPose.Pose;
        Pose player1Pose = GameData.Player1.GetCurrentPose();
        Pose player2Pose = GameData.Player2.GetCurrentPose();

        if (player1Pose.Matches(firstPose) && player2Pose.Matches(firstPose))
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

