using UnityEngine;
using System.Collections;

public class ResourceLoader : MonoBehaviour {

	void Start ()
	{
		// obviously these aren't going anywhere yet
		Resources.LoadAll ("");
		GameStateMachine.ChangeState (eGameState.Game);
	}
}
