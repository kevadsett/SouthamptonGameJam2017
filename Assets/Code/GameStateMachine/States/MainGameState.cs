using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState : GameState
{
	private float _timeToMatchPose = 20.0f;
	private PoseGenerator _poseGenerator;
	private List<PoseModel> _poseTargets;
	private List<PoseModel> _posesToRemove;

	private PoseController _player1;
	private PoseController _player2;

	private int _player1Score;
	private int _player2Score;

	private int _player1Lives = 3;
	private int _player2Lives = 3;

	public override void EnterState()
	{
		SceneManager.LoadScene ("Game", LoadSceneMode.Single);
		_poseTargets = new List<PoseModel> ();
		_posesToRemove = new List<PoseModel> ();
		_poseGenerator = new PoseGenerator (_timeToMatchPose);
		_player1 = new PoseController (_timeToMatchPose,
			UnityEngine.KeyCode.Q,
			UnityEngine.KeyCode.W,
			UnityEngine.KeyCode.A,
			UnityEngine.KeyCode.S
		);
		_player2 = new PoseController (_timeToMatchPose,
			UnityEngine.KeyCode.I,
			UnityEngine.KeyCode.O,
			UnityEngine.KeyCode.K,
			UnityEngine.KeyCode.L
		);
	}

	public override void Update()
	{
		_poseGenerator.Update (_poseTargets);
		_posesToRemove.Clear ();
		foreach (PoseModel pose in _poseTargets)
		{
			pose.Update ();
			if (pose.HasExpired)
			{
				JudgePoses (pose);
				_posesToRemove.Add (pose);
			}
		}
		foreach (PoseModel pose in _posesToRemove)
		{
			_poseTargets.Remove (pose);
		}
		_player1.Update ();
		_player2.Update ();

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

	private void JudgePoses(PoseModel pose)
	{
		if (_player1.IsPoseCorrect (pose))
		{
			Debug.Log ("Player 1 got it right");
			_player1Score++;
		}
		else
		{
			Debug.Log ("Player 1 got it wrong");
			_player1Lives--;
		}
		if (_player2.IsPoseCorrect (pose))
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

