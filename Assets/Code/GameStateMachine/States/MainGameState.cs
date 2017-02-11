using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState : GameState
{
	private float _timeToMatchPose = 20.0f;
	private int _poseTargetsPerWave = 12;
	private int _currentWaveIndex = 0;

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
		_poseGenerator = new PoseGenerator (_poseLibrary, _timeToMatchPose);

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

        // Set up the UI.
		GameObject backgroundCanvasPrefab = Resources.Load<GameObject> ("UI/BackgroundCanvas");
		GameObject canvasObject = GameObject.Find ("UICanvas") as GameObject;
        GameObject backgroundCanvas = GameObject.Find("ForegroundUICanvas") as GameObject;

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

		ViewBindings.Instance.BindValue ("Player1Score", "" + _player1Score);
		ViewBindings.Instance.BindValue ("Player1Lives", "" + _player1Lives);
		ViewBindings.Instance.BindValue ("Player2Score", "" + _player2Score);
		ViewBindings.Instance.BindValue ("Player2Lives", "" + _player2Lives);
	}

	public override void Update()
	{
		TargetPose newPose = _poseGenerator.AddPoseIfNeeded (_poseTargets);

		if (newPose != null)
		{
			_poseRibbon.AddNewPoseDiagram(newPose, _limbAnimation, _stickmanParts, _poseLibrary);
		}

		_posesToRemove.Clear ();

        for(int i=0; i<_poseTargets.Count; i++)
        {
            TargetPose pose = _poseTargets[i];
			pose.Update ();
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
        if (_player1.GetCurrentPose().Matches(pose.Pose))
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
		if (_player2.GetCurrentPose().Matches(pose.Pose))
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
			StateMachine.PushState (eGameState.InterimScore);
		}
		else
		{
			_currentWaveIndex++;
			if (_currentWaveIndex == _poseTargetsPerWave)
			{
				StateMachine.PushState (eGameState.InterimScore);
			}
		}
	}
}

