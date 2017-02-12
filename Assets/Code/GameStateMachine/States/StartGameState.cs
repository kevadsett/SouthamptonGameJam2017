using System;
using UnityEngine;

public class StartGameState : GameState
{
    private GameObject _poseRibbonForeground;
    private GameObject _backgroundCanvas;
    private GameObject _scoreLives;
    private GameObject _interimScreen;  

	public override void EnterState ()
	{
		GameData.Player1 = CreatePlayer("Player1", -15f, GameData.LimbAnimation, GameData.Player1Parts, GameData.PoseLibrary, KeyCode.Q, KeyCode.W, KeyCode.A, KeyCode.S);
		GameData.Player2 = CreatePlayer("Player2", 15f, GameData.LimbAnimation, GameData.Player2Parts, GameData.PoseLibrary, KeyCode.I, KeyCode.O, KeyCode.K, KeyCode.L);

		GameObject foregroundCanvas = GameObject.Find("ForegroundUICanvas");
		RectTransform foregroundCanvasTransform = foregroundCanvas.GetComponent<RectTransform>();

		GameData.PoseRibbon = GameObject.Instantiate(GameData.PoseRibbonPrefab).GetComponent<PoseRibbon>();
		GameData.PoseRibbon.transform.SetParent(GameData.CanvasTransform, false);
		GameData.PoseRibbon.Setup(GameData.PoseDiagramPrefab);

		_poseRibbonForeground = GameObject.Instantiate(GameData.PoseRibbonForegroundPrefab);
		_poseRibbonForeground.transform.SetParent(foregroundCanvasTransform, false);

		_backgroundCanvas = GameObject.Instantiate (GameData.BackgroundCanvasPrefab);
		GameObject backgroundCameraObject = GameObject.Find ("BackgroundCamera");
		_backgroundCanvas.GetComponent<Canvas> ().worldCamera =  backgroundCameraObject.GetComponent<Camera>();
		
		_scoreLives = GameObject.Instantiate (GameData.ScoreLivesPrefab);
		_scoreLives.transform.SetParent (foregroundCanvas.transform, false);

        ViewBindings.Instance.BindValue("CurrentRound", 0);

        _interimScreen = GameObject.Instantiate (GameData.InterimScreen);
        _interimScreen.transform.SetParent (foregroundCanvas.transform, false);

        GameData.PoseManager.GeneratePosesForRound(GameData.WaveCount, 0);

        ViewBindings.Instance.BindValue ("Player1Score", "0");
        ViewBindings.Instance.BindValue ("Player2Score", "0");
	}

    public override void ExitState()
    {
        GameObject.Destroy(GameData.Player1.gameObject);
        GameObject.Destroy(GameData.Player2.gameObject);
        GameObject.Destroy(GameData.PoseRibbon.gameObject);
        GameObject.Destroy(_poseRibbonForeground);
        GameObject.Destroy(_backgroundCanvas);
        GameObject.Destroy(_scoreLives);

        GameData.PoseManager.Reset();
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
            GameObject.Destroy(_interimScreen);
			StateMachine.PushState (eGameState.Game);
		}
	}

    public override void OnChildPop()
    {
        StateMachine.ChangeState(eGameState.Title);
    }

	private Poser CreatePlayer(string name, float horizontalPosition, LimbAnimation limbAnimation, PoserParts poserParts, PoseLibrary poseLibrary, params KeyCode[] controls)
	{
		Poser poser = Poser.CreatePoser(true, limbAnimation, poserParts, poseLibrary, controls);
		poser.name = name;
		poser.transform.position = new Vector3(horizontalPosition, 0, 0);

		return poser;
	}
}

