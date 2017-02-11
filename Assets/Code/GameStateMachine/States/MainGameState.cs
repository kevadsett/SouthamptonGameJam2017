using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainGameState : GameState
{
	private PoseGenerator _poseGenerator;
	private List<PoseModel> _poseTargets;
	private List<PoseModel> _posesToRemove;

	private PoseController _player1;
	private PoseController _player2;

	public override void EnterState()
	{
		SceneManager.LoadScene ("Game", LoadSceneMode.Single);
		_poseTargets = new List<PoseModel> ();
		_posesToRemove = new List<PoseModel> ();
		_poseGenerator = new PoseGenerator (2);
		_player1 = new PoseController (
			UnityEngine.KeyCode.Q,
			UnityEngine.KeyCode.W,
			UnityEngine.KeyCode.A,
			UnityEngine.KeyCode.S
		);
		_player2 = new PoseController (
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
				_posesToRemove.Add (pose);
			}
		}
		foreach (PoseModel pose in _posesToRemove)
		{
			_poseTargets.Remove (pose);
		}
		_player1.Update ();
		_player2.Update ();
	}

	public override void ExitState()
	{
		_poseGenerator = null;
	}
}

