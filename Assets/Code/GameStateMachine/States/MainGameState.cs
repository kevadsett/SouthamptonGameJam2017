using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState : GameState
{
	private float _timeToMatchPose = 20.0f;
	private PoseGenerator _poseGenerator;

	private List<TargetPose> _poseTargets;
	private List<TargetPose> _posesToRemove;

	private Poser _player1;
	private Poser _player2;

	private int _player1Score;
	private int _player2Score;

	private int _player1Lives = 3;
	private int _player2Lives = 3;

    private Poser CreatePoser(string name, float horizontalPosition, GameObject prefab, PoserTextures poserTextures, PoseLibrary poseLibrary, params KeyCode[] controls)
    {
        GameObject instance = GameObject.Instantiate(prefab);
        instance.name = name;
        instance.transform.position = new Vector3(horizontalPosition, 0, 0);

        Poser poser = instance.GetComponent<Poser>();

        poser.Setup(poserTextures, poseLibrary, controls);

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
        PoserTextures poserTextures = Resources.Load<PoserTextures>("TestPoserTextures");
        GameObject poserPrefab = Resources.Load<GameObject>("Poser");

        _player1 = CreatePoser("Player1", -4f, poserPrefab, poserTextures, poseLibrary, KeyCode.Q, KeyCode.W, KeyCode.A, KeyCode.S);
        _player2 = CreatePoser("Player2", 4f, poserPrefab, poserTextures, poseLibrary, KeyCode.I, KeyCode.O, KeyCode.K, KeyCode.L);
	}

	public override void Update()
	{
		_poseGenerator.Update (_poseTargets);
		_posesToRemove.Clear ();

		foreach (TargetPose pose in _poseTargets)
		{
			pose.Update ();
			if (pose.HasExpired)
			{
				JudgePoses (pose);
				_posesToRemove.Add (pose);
			}
		}

		foreach (TargetPose pose in _posesToRemove)
		{
			_poseTargets.Remove (pose);
		}

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
	}

	public override void ExitState()
	{
		_poseGenerator = null;
	}

	private void JudgePoses(TargetPose pose)
	{
        if (_player1.GetCurrentPose().Equals(pose))
		{
			Debug.Log ("Player 1 got it right");
			_player1Score++;
		}
		else
		{
			Debug.Log ("Player 1 got it wrong");
			_player1Lives--;
		}
		if (_player2.GetCurrentPose().Equals(pose))
		{
			Debug.Log ("Player 2 got it right");
			_player2Score++;
		}
		else
		{
			Debug.Log ("Player 2 got it wrong");
			_player2Lives--;
		}
	}
}

