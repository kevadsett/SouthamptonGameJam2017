using System;
using UnityEngine.SceneManagement;

public class MainGameState : GameState
{
	private PoseGenerator _poseGenerator;

	public override void EnterState()
	{
		SceneManager.LoadScene ("Game", LoadSceneMode.Single);
		_poseGenerator = new PoseGenerator (2);
	}

	public override void Update()
	{
		_poseGenerator.Update ();
	}

	public override void ExitState()
	{
		_poseGenerator = null;
	}
}

