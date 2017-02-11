using UnityEngine;
using System.Collections;

public interface IGameState 
{
	void EnterState ();
	void Update ();
	void ExitState();
}
