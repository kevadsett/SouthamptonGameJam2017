using System;
using UnityEngine;
public static class GameData
{
	public static PoseLibrary PoseLibrary;
	public static LimbAnimation LimbAnimation;
	public static PoserParts Player1Parts;
    public static PoserParts Player2Parts;
	public static PoserParts StickmanParts;

	public static GameObject PoseRibbonPrefab;
	public static GameObject PoseRibbonForegroundPrefab;
	public static GameObject PoseDiagramPrefab;
	public static GameObject ScoreLivesPrefab;
	public static GameObject JudgingScreenPrefab;

	public static GameObject BackgroundCanvasPrefab;

	public static GameObject TitleScreenPrefab;
    public static GameObject GameOverScreenPrefab;

	public static Poser Player1;
	public static Poser Player2;

	public static PoseRibbon PoseRibbon;

	public static GameObject AudioPrefab;

	public static RectTransform CanvasTransform;

    public static PoseManager PoseManager;
    public const int BeatsPerPose = 8;
    public const int BarCount = 12;
    public const int WaveCount = 7;
    public const int BeatsPerBar = 4;
}
