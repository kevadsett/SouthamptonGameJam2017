using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainGameState : IGameState
{
	private PoseGenerator _poseGenerator;
	private List<PoseModel> _poseTargets;
	private List<PoseModel> _posesToRemove;
	public void EnterState()
	{
		SceneManager.LoadScene ("Game", LoadSceneMode.Additive);
		_poseTargets = new List<PoseModel> ();
		_posesToRemove = new List<PoseModel> ();
		_poseGenerator = new PoseGenerator (2);
	}

	public void Update()
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

	public void ExitState()
	{
		SceneManager.UnloadScene ("Game");
		_poseGenerator = null;
	}
}

