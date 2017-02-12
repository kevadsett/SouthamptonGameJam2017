using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState : GameState
{
    private const int _maxRounds = 4;
	private int _currentWaveIndex = 0;

	private float[] _bpms = new float[] { 120, 140, 160, 180 };
	private float _currentBpm;
	private int _currentRound;

	private int _player1Score;
	private int _player2Score;

    private JudgingScreen _judgingScreen;

	private int _beatsPassed;
	private int _barsPassed;

	public override void EnterState()
    {
        _currentWaveIndex = 0;
        _currentRound = 0;
        _currentBpm = _bpms [_currentRound];
        _player1Score = 0;
        _player2Score = 0;
        _beatsPassed = 0;
        _barsPassed = 0;

        _judgingScreen = GameObject.Instantiate(GameData.JudgingScreenPrefab).GetComponent<JudgingScreen>();
        _judgingScreen.transform.SetParent(GameData.CanvasTransform, false);

		ViewBindings.Instance.BindValue ("Player1Score", "" + _player1Score);
		ViewBindings.Instance.BindValue ("Player2Score", "" + _player2Score);
		ViewBindings.Instance.BindValue ("bpm", _currentBpm);
	}

	public override void Update()
	{
        float dt = Time.deltaTime;

        if(BeatManager.IsBeatFrame && BeatManager.CurrentBeat % GameData.BeatsPerPose == 0)
        {
            JudgePoses(GameData.PoseManager.GetTargetPose(BeatManager.CurrentBeat / GameData.BeatsPerPose));
        }

        UpdateMusic ();

        BeatManager.Update (dt);

		GameData.Player1.UpdatePose(dt);
		GameData.Player2.UpdatePose(dt);

		DetermineNextStep ();
	}

	public override void ExitState()
	{
        GameObject.Destroy(_judgingScreen.gameObject);

        BeatManager.Reset();
	}

	private void JudgePoses(TargetPose pose)
	{
		pose.HasBeenJudged = true;

		bool player1Success = GameData.Player1.GetCurrentPose().Matches(pose.Pose);
		bool player2Success = GameData.Player2.GetCurrentPose().Matches(pose.Pose);

        _judgingScreen.DisplayResults(player1Success, player2Success);

        if(player1Success)
		{
			_player1Score++;
			ViewBindings.Instance.BindValue ("Player1Score", "" + _player1Score);
		}

		if(player2Success)
		{
			_player2Score++;
			ViewBindings.Instance.BindValue ("Player2Score", "" + _player2Score);
		}
	}

	private void UpdateMusic()
	{
		if (BeatManager.IsBeatFrame)
		{
			string trackName = _currentBpm + "bpm";
			if (AudioPlayer.IsPlaying (trackName) == false)
			{
				AudioPlayer.PlaySound (trackName, Vector3.zero);
			}
		}
	}

	private void DetermineNextStep()
	{
		if (BeatManager.IsBeatFrame)
		{
			_beatsPassed++;

			if (_beatsPassed == GameData.BeatsPerBar)
			{
				_barsPassed++;
				_beatsPassed = 0;

				if (_barsPassed == GameData.BarCount)
				{
					_currentRound++;

                    ViewBindings.Instance.BindValue("CurrentRound", _currentRound);

					_barsPassed = 0;
					if (_currentRound < _maxRounds)
                    {
						GameData.PoseManager.GeneratePosesForRound(GameData.WaveCount, _currentRound);
						StartNextRound ();
					}
					else
					{
                        if(_player1Score > _player2Score)
                        {
                            StateMachine.PushState (eGameState.Player1Victory);
                        }
                        else if(_player1Score < _player2Score)
                        {
                            StateMachine.PushState (eGameState.Player2Victory);
                        }
                        else
                        {
                            StateMachine.PushState (eGameState.Draw);
                        }
					}
				}
			}
		}
	}

	private void StartNextRound()
    {
        _currentBpm = _bpms [++_currentWaveIndex];
		StateMachine.PushState (eGameState.InterimScore);
        ViewBindings.Instance.BindValue ("bpm", 0f);
	}

    public override void OnChildPop()
    {
        if(_currentRound >= _maxRounds)
        {
            StateMachine.PopState();
        }
        else
        {
            ViewBindings.Instance.BindValue ("bpm", _currentBpm);
        }
    }
}

