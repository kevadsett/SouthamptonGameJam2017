using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameState : GameState
{
	public override void EnterState()
	{
		// Set up the pose system.
		GameData.PoseLibrary = Resources.Load<PoseLibrary>("PoseLibrary");
		// Load the players.
		GameData.LimbAnimation = Resources.Load<LimbAnimation>("LimbAnimation");
        GameData.Player1Parts = Resources.Load<PoserParts>("Parts/MrBaguetteParts");
		GameData.Player2Parts = Resources.Load<PoserParts>("Parts/MrFrancaisParts");
		// Load the stickman.
		GameData.StickmanParts = Resources.Load<PoserParts>("Parts/StickmanParts");

		// Load the ribbon.
		GameData.PoseRibbonPrefab = Resources.Load<GameObject> ("UI/PoseRibbon");
		GameData.PoseRibbonForegroundPrefab = Resources.Load<GameObject> ("UI/PoseRibbonForeground");
		GameData.PoseDiagramPrefab = Resources.Load<GameObject> ("UI/PoseRibbonDiagram");
		GameData.ScoreLivesPrefab = Resources.Load<GameObject> ("UI/Score_Lives");
		GameData.JudgingScreenPrefab = Resources.Load<GameObject>("UI/JudgingScreen");

		// Set up the UI.
		GameData.BackgroundCanvasPrefab = Resources.Load<GameObject> ("UI/BackgroundCanvas");

		GameData.TitleScreenPrefab = Resources.Load<GameObject> ("UI/TitleScreen");
        GameData.GameOverScreenPrefab = Resources.Load<GameObject>("UI/GameOverScreen");
        GameData.FlagScreenPrefab = Resources.Load<GameObject>("UI/FlagScreen");

		// Load the audio.
		GameData.AudioPrefab = Resources.Load<GameObject>("Audio");

        GameData.PoseManager = new PoseManager (GameData.PoseLibrary);

        GameObject.Instantiate(GameData.AudioPrefab);
	}

	public override void Update()
    {
        StateMachine.ChangeState(eGameState.Title);
    }
}

