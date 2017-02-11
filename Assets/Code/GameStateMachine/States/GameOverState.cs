using System;
using UnityEngine;

public enum eGameOverType
{
	Player1Victory,
	Player2Victory,
	Draw
}

public class GameOverState : GameState
{
	eGameOverType _type;
	public GameOverState (eGameOverType type)
	{
		_type = type;
	}

	public override void EnterState ()
	{
		base.EnterState ();
		Debug.Log (_type);
	}

	public override void Update ()
	{
		base.Update ();
	}

	public override void ExitState ()
	{
		base.ExitState ();
	}
}

