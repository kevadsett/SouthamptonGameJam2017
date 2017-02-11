using UnityEngine;
using System.Collections;

public class EntryPoint : MonoBehaviour {

	void Start () {
		GameStateMachine.ChangeState (eGameState.Load);
	}
	
	void Update () {
		GameStateMachine.Update ();
	}
}
