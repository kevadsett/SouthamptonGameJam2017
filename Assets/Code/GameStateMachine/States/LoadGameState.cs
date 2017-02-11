using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameState : GameState
{
	public override void EnterState()
	{
        
	}

	public override void Update()
    {
        StateMachine.ChangeState(eGameState.Game);
    }
}

