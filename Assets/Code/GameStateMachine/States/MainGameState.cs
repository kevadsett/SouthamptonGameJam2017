using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainGameState : GameState
{
	private PoseGenerator _poseGenerator;
	private List<PoseModel> _poseTargets;
	private List<PoseModel> _posesToRemove;
	public override void EnterState()
	{
		SceneManager.LoadScene ("Game", LoadSceneMode.Single);
		_poseTargets = new List<PoseModel> ();
		_posesToRemove = new List<PoseModel> ();
		_poseGenerator = new PoseGenerator (2);
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
	}

	public override void ExitState()
	{
		_poseGenerator = null;
	}
}

