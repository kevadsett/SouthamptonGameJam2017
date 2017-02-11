using System;
using UnityEngine.SceneManagement;

public class LoadGameState : IGameState
{
	public void EnterState()
	{
		SceneManager.LoadScene ("Load", LoadSceneMode.Additive);
	}

	public void Update() {}

	public void ExitState()
	{
		SceneManager.UnloadScene ("Load");
	}
}

