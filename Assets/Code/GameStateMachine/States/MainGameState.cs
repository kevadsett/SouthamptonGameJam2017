using System;
using UnityEngine.SceneManagement;

public class MainGameState : IGameState
{
	public void EnterState()
	{
		SceneManager.LoadScene ("Game", LoadSceneMode.Additive);
	}

	public void Update() {}

	public void ExitState()
	{
		SceneManager.UnloadScene ("Game");
	}
}

