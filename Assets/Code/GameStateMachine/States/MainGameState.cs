using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState : GameState
{
	private int _poseTargetsPerWave = 12;
	private int _currentWaveIndex = 0;

	private float[] _bpms = new float[] { 120, 140, 160, 180 };
	private float _currentBpm;
	private int _currentRound;

	private PoseGenerator _poseGenerator;

	private List<TargetPose> _poseTargets;
	private List<TargetPose> _posesToRemove;

	private Poser _player1;
	private Poser _player2;

	private int _player1Score;
	private int _player2Score;

	private int _player1Lives = 3;
	private int _player2Lives = 3;

    private LimbAnimation _limbAnimation;
    private PoseLibrary _poseLibrary;
    private PoserParts _stickmanParts;
	private PoseRibbon _poseRibbon;

	private GameObject _backgroundCanvas;

    private JudgingScreen _judgingScreen;

    private Poser CreatePlayer(string name, float horizontalPosition, LimbAnimation limbAnimation, PoserParts poserParts, PoseLibrary poseLibrary, params KeyCode[] controls)
    {
        Poser poser = Poser.CreatePoser(true, limbAnimation, poserParts, poseLibrary, controls);
        poser.name = name;
        poser.transform.position = new Vector3(horizontalPosition, 0, 0);

        return poser;
    }

	public override void EnterState()
    {
        // Set up the pose system.
        _poseLibrary = Resources.Load<PoseLibrary>("PoseLibrary");

		_poseTargets = new List<TargetPose> ();
		_posesToRemove = new List<TargetPose> ();
		_poseGenerator = new PoseGenerator (_poseLibrary);

		// Load the audio.
		GameObject audioPrefab = Resources.Load<GameObject>("Audio");
		GameObject.Instantiate(audioPrefab);

        // Load the players.
        _limbAnimation = Resources.Load<LimbAnimation>("LimbAnimation");
        PoserParts playerParts = Resources.Load<PoserParts>("Parts/MrBaguetteParts");

        _player1 = CreatePlayer("Player1", -15f, _limbAnimation, playerParts, _poseLibrary, KeyCode.Q, KeyCode.W, KeyCode.A, KeyCode.S);
        _player2 = CreatePlayer("Player2", 15f, _limbAnimation, playerParts, _poseLibrary, KeyCode.I, KeyCode.O, KeyCode.K, KeyCode.L);

        // Load the stickman.
        _stickmanParts = Resources.Load<PoserParts>("Parts/StickmanParts");

        // Load the ribbon.
        GameObject poseRibbonPrefab = Resources.Load<GameObject> ("UI/PoseRibbon");
		GameObject poseRibbonForegroundPrefab = Resources.Load<GameObject> ("UI/PoseRibbonForeground");
		GameObject poseDiagramPrefab = Resources.Load<GameObject> ("UI/PoseRibbonDiagram");
		GameObject scoreLivesPrefab = Resources.Load<GameObject> ("UI/Score_Lives");
        GameObject judgingScreenPrefab = Resources.Load<GameObject>("UI/JudgingScreen");

        // Set up the UI.
		GameObject backgroundCanvasPrefab = Resources.Load<GameObject> ("UI/BackgroundCanvas");
        GameObject canvasObject = GameObject.Find("UICanvas");
        GameObject backgroundCanvas = GameObject.Find("ForegroundUICanvas");

		RectTransform canvasTransform = canvasObject.GetComponent<RectTransform> ();
        RectTransform foregroundCanvasTransform = backgroundCanvas.GetComponent<RectTransform>();

		GameObject scoreLivesUI = GameObject.Instantiate (scoreLivesPrefab);
		scoreLivesUI.transform.SetParent (canvasTransform, false);

        _poseRibbon = GameObject.Instantiate(poseRibbonPrefab).GetComponent<PoseRibbon>();
        _poseRibbon.transform.SetParent(canvasTransform, false);
        _poseRibbon.Setup(poseDiagramPrefab);

        GameObject poseRibbonForeground = GameObject.Instantiate(poseRibbonForegroundPrefab);
        poseRibbonForeground.transform.SetParent(foregroundCanvasTransform, false);

		_backgroundCanvas = GameObject.Instantiate (backgroundCanvasPrefab);
		GameObject backgroundCameraObject = GameObject.Find ("BackgroundCamera");
		_backgroundCanvas.GetComponent<Canvas> ().worldCamera =  backgroundCameraObject.GetComponent<Camera>();

        _judgingScreen = GameObject.Instantiate(judgingScreenPrefab).GetComponent<JudgingScreen>();
        _judgingScreen.transform.SetParent(canvasTransform, false);

		ViewBindings.Instance.BindValue ("Player1Score", "" + _player1Score);
		ViewBindings.Instance.BindValue ("Player1Lives", "" + _player1Lives);
		ViewBindings.Instance.BindValue ("Player2Score", "" + _player2Score);
		ViewBindings.Instance.BindValue ("Player2Lives", "" + _player2Lives);

		_currentBpm = _bpms [_currentRound];
		ViewBindings.Instance.BindValue ("bpm", _currentBpm);
	}

	public override void Update()
	{
		BeatManager.Update (Time.deltaTime);
		TargetPose newPose = _poseGenerator.AddPoseIfNeeded (_poseTargets);

		if (newPose != null)
		{
			_poseRibbon.AddNewPoseDiagram(newPose, _limbAnimation, _stickmanParts, _poseLibrary);
		}

		_posesToRemove.Clear ();

        for(int i=0; i<_poseTargets.Count; i++)
        {
            TargetPose pose = _poseTargets[i];

			if (pose.HasExpired)
			{
				if (pose.HasBeenJudged == false)
				{
					JudgePoses(pose);
				}
			}
		}

		if (_player1Lives < 1 && _player2Lives > 0)
		{
			StateMachine.ChangeState (eGameState.Player2Victory);
		}
		else if (_player2Lives < 1 && _player1Lives > 0)
		{
			StateMachine.ChangeState (eGameState.Player1Victory);
		}
		else if (_player1Lives < 1 && _player2Lives == 0)
		{
			StateMachine.ChangeState (eGameState.Draw);
		}

        float dt = Time.deltaTime;

		_player1.UpdatePose(dt);
		_player2.UpdatePose(dt);
	}

	public override void ExitState()
	{
		_poseGenerator = null;
		GameObject.Destroy (_backgroundCanvas);
	}

	private void JudgePoses(TargetPose pose)
	{
		pose.HasBeenJudged = true;
		bool someoneWasWrong = false;

        bool player1Success = _player1.GetCurrentPose().Matches(pose.Pose);
        bool player2Success = _player2.GetCurrentPose().Matches(pose.Pose);

        _judgingScreen.DisplayResults(player1Success, player2Success);

        if(player1Success)
		{
			_player1Score++;
			ViewBindings.Instance.BindValue ("Player1Score", "" + _player1Score);
		}
		else
		{
			_player1Lives--;
			ViewBindings.Instance.BindValue ("Player1Lives", "" + _player1Lives);
			someoneWasWrong = true;
		}

		if(player2Success)
		{
			_player2Score++;
			ViewBindings.Instance.BindValue ("Player2Score", "" + _player2Score);
		}
		else
		{
			_player2Lives--;
			ViewBindings.Instance.BindValue ("Player2Lives", "" + _player2Lives);
			someoneWasWrong = true;
		}

		if (someoneWasWrong)
		{
			StateMachine.PushState(eGameState.InterimScore);
		}
		else
		{
			_currentWaveIndex++;
			if (_currentWaveIndex == _poseTargetsPerWave)
			{
				StartNextRound();
			}
		}
	}

	private void StartNextRound()
	{
		_currentWaveIndex++;
		_currentBpm = _bpms [_currentWaveIndex];
		StateMachine.PushState (eGameState.InterimScore);
		ViewBindings.Instance.BindValue ("bpm", _currentBpm);
	}
}

