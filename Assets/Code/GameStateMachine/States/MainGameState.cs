using System;
using UnityEngine.SceneManagement;

public class MainGameState : IGameState
{
	private PoseGenerator _poseGenerator;
	public void EnterState()
	{
		SceneManager.LoadScene ("Game", LoadSceneMode.Additive);
		_poseGenerator = new PoseGenerator (2);
	}

	public void Update()
	{
		_poseGenerator.Update ();
	}

	public void ExitState()
	{
		SceneManager.UnloadScene ("Game");
		_poseGenerator = null;
	}
}

