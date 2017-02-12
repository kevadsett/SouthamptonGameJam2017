using UnityEngine;
using UnityEngine.SceneManagement;

public class InterimScoreState : GameState
{
	private float _pauseTime;
	private float _timeInState;
	private GameObject _screen;

	public InterimScoreState (float pauseTime)
	{
		_pauseTime = pauseTime;
	}
	public override void EnterState()
	{
		BeatManager.Reset();
		GameObject interimScorePrefab = Resources.Load<GameObject> ("UI/InterimScore");
		RectTransform UICanvasTransform = GameObject.Find ("UICanvas").GetComponent<RectTransform> ();
		_screen = GameObject.Instantiate (interimScorePrefab);
		_screen.transform.SetParent (UICanvasTransform, false);
	}

	public override void Update()
	{
		float dt = Time.deltaTime;
		GameData.Player1.UpdatePose(dt);
		GameData.Player2.UpdatePose(dt);

		Pose firstPose = GameData.PoseManager.FirstPose.Pose;
		Pose player1Pose = GameData.Player1.GetCurrentPose();
		Pose player2Pose = GameData.Player2.GetCurrentPose();

		if (player1Pose.Matches(firstPose) && player2Pose.Matches(firstPose))
		{
			StateMachine.PopState ();
		}
	}

	public override void ExitState ()
	{
		_timeInState = 0.0f;
		GameObject.Destroy (_screen);
	}
}

