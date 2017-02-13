using UnityEngine;
using UnityEngine.SceneManagement;

public class InterimScoreState : GameState
{
	public override void EnterState()
	{
		BeatManager.Reset();
		RectTransform UICanvasTransform = GameObject.Find ("UICanvas").GetComponent<RectTransform> ();

        GameData.FlagScreen.SetVisible(true);
	}

    public override void ExitState()
    {
        GameData.FlagScreen.SetVisible(false);
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
}

