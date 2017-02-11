using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameState : GameState
{
	public override void EnterState()
	{
		SceneManager.LoadScene("Load", LoadSceneMode.Single);
	}

	public override void Update()
    {
        StateMachine.ChangeState(eGameState.Game);
    }
}

