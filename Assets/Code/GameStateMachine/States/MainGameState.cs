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

	private PoseRibbon _poseRibbon;

    private Poser CreatePoser(string name, float horizontalPosition, LimbAnimation limbAnimation, PoserParts poserParts, PoseLibrary poseLibrary, params KeyCode[] controls)
    {
        Poser poser = Poser.CreatePoser(limbAnimation, poserParts, poseLibrary, controls);
        poser.name = name;
        poser.transform.position = new Vector3(horizontalPosition, 0, 0);

        return poser;
    }

	public override void EnterState()
    {
        // Set up the pose system.
        PoseLibrary poseLibrary = Resources.Load<PoseLibrary>("PoseLibrary");

		_poseTargets = new List<TargetPose> ();
		_posesToRemove = new List<TargetPose> ();
		_poseGenerator = new PoseGenerator (poseLibrary, _timeToMatchPose);

        // Load the background.
        GameObject backgroundPrefab = Resources.Load<GameObject>("Background");
        GameObject.Instantiate(backgroundPrefab);

        // Load the players.
        LimbAnimation limbAnimation = Resources.Load<LimbAnimation>("LimbAnimation");
        PoserParts poserParts = Resources.Load<PoserParts>("MrBaguetteParts");
        GameObject poserPrefab = Resources.Load<GameObject>("Poser");

        _player1 = CreatePoser("Player1", -10f, limbAnimation, poserParts, poseLibrary, KeyCode.Q, KeyCode.W, KeyCode.A, KeyCode.S);
        _player2 = CreatePoser("Player2", 10f, limbAnimation, poserParts, poseLibrary, KeyCode.I, KeyCode.O, KeyCode.K, KeyCode.L);

		GameObject poseRibbonPrefab = Resources.Load<GameObject> ("UI/PoseRibbonContainer");
		GameObject poseDiagramPrefab = Resources.Load<GameObject> ("UI/PoseDiagram");
		_poseRibbon = new PoseRibbon(poseRibbonPrefab, poseDiagramPrefab, GameObject.Find("UICanvas").transform);

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
			_poseRibbon.AddNewPoseDiagram (newPose);
		}

		_posesToRemove.Clear ();

		foreach (TargetPose pose in _poseTargets)
		{
			pose.Update ();
			if (pose.HasExpired)
			{
				if (pose.HasBeenJudged == false)
				{
					JudgePoses (pose);
				}
//				_posesToRemove.Add (pose);
			}
		}

//		foreach (TargetPose pose in _posesToRemove)
//		{
//			_poseTargets.Remove (pose);
//		}

		if (_player1Lives == 0 && _player2Lives > 0)
		{
			StateMachine.ChangeState (eGameState.Player1Victory);
		}
		else if (_player2Lives == 0 && _player1Lives > 0)
		{
			StateMachine.ChangeState (eGameState.Player2Victory);
		}
		else if (_player1Lives == 0 && _player2Lives == 0)
		{
			StateMachine.ChangeState (eGameState.Draw);
		}
		_player1.UpdatePose ();
		_player2.UpdatePose ();
	}

	public override void ExitState()
	{
		_poseGenerator = null;
	}

	private void JudgePoses(TargetPose pose)
	{
		pose.HasBeenJudged = true;
		bool someoneWasWrong = false;
        if (_player1.GetCurrentPose().Equals(pose))
		{
			Debug.Log ("Player 1 got it right");
			_player1Score++;
			ViewBindings.Instance.BindValue ("Player1Score", "" + _player1Score);
		}
		else
		{
			Debug.Log ("Player 1 got it wrong");
			_player1Lives--;
			ViewBindings.Instance.BindValue ("Player1Lives", "" + _player1Lives);
			someoneWasWrong = true;
		}
		if (_player2.GetCurrentPose().Equals(pose))
		{
			Debug.Log ("Player 2 got it right");
			_player2Score++;
			ViewBindings.Instance.BindValue ("Player2Score", "" + _player2Score);
		}
		else
		{
			Debug.Log ("Player 2 got it wrong");
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

