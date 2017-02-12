using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState : GameState
{
	private int _currentWaveIndex = 0;


	private float[] _bpms = new float[] { 120, 140, 160, 180 };
	private float _currentBpm;
	private int _currentRound;

	private int _player1Score;
	private int _player2Score;

	private int _player1Lives = 3;
	private int _player2Lives = 3;

    private JudgingScreen _judgingScreen;

	private int _beatsPassed;
	private int _barsPassed;

	public override void EnterState()
    {
        _judgingScreen = GameObject.Instantiate(GameData.JudgingScreenPrefab).GetComponent<JudgingScreen>();
        _judgingScreen.transform.SetParent(GameData.CanvasTransform, false);

		ViewBindings.Instance.BindValue ("Player1Score", "" + _player1Score);
		ViewBindings.Instance.BindValue ("Player1Lives", "" + _player1Lives);
		ViewBindings.Instance.BindValue ("Player2Score", "" + _player2Score);
		ViewBindings.Instance.BindValue ("Player2Lives", "" + _player2Lives);

		_currentBpm = _bpms [_currentRound];
		ViewBindings.Instance.BindValue ("bpm", _currentBpm);

		GameObject.Instantiate(GameData.AudioPrefab);
	}

	public override void Update()
	{
		float dt = Time.deltaTime;
		BeatManager.Update (dt);

		UpdateMusic ();

		GameData.Player1.UpdatePose(dt);
		GameData.Player2.UpdatePose(dt);

		DetermineNextStep ();
	}

	public override void ExitState()
	{
		GameObject.Destroy (GameData.PoseRibbon);
		GameObject.Destroy (GameData.Player1);
		GameObject.Destroy (GameData.Player2);
	}

	private void JudgePoses(TargetPose pose)
	{
		pose.HasBeenJudged = true;
		bool someoneWasWrong = false;

		bool player1Success = GameData.Player1.GetCurrentPose().Matches(pose.Pose);
		bool player2Success = GameData.Player2.GetCurrentPose().Matches(pose.Pose);

        _judgingScreen.DisplayResults(player1Success, player2Success);

        if(player1Success)
		{
			_player1Score++;
			ViewBindings.Instance.BindValue ("Player1Score", "" + _player1Score);
		}
		else
		{
			_player1Lives--;
			ViewBindings.Instance.BindValue ("Player1Lives", "" + _player1Lives);
			someoneWasWrong = true;
		}

		if(player2Success)
		{
			_player2Score++;
			ViewBindings.Instance.BindValue ("Player2Score", "" + _player2Score);
		}
		else
		{
			_player2Lives--;
			ViewBindings.Instance.BindValue ("Player2Lives", "" + _player2Lives);
			someoneWasWrong = true;
		}

		if (someoneWasWrong)
		{
			StateMachine.PushState(eGameState.InterimScore);
		}
		else
		{
			_currentWaveIndex++;
			if (_currentWaveIndex == GameData.WaveCount)
			{
				StartNextRound();
			}
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
		if (_player1Lives < 1 && _player2Lives > 0)
		{
			StateMachine.ChangeState (eGameState.Player2Victory);
		}
		else if (_player2Lives < 1 && _player1Lives > 0)
		{
			StateMachine.ChangeState (eGameState.Player1Victory);
		}
		else if (_player1Lives < 1 && _player2Lives == 0)
		{
			StateMachine.ChangeState (eGameState.Draw);
		}

		if (BeatManager.IsBeatFrame)
		{
			_beatsPassed++;
			if (_beatsPassed == 4)
			{
				_barsPassed++;
				_beatsPassed = 0;
				if (_barsPassed == 12)
				{
					_currentRound++;
					_barsPassed = 0;
					if (_currentRound < 4)
                    {
                        GameData.PoseManager.GeneratePosesForRound(GameData.WaveCount);
						StartNextRound ();
					}
					else
					{
						// TODO: Have an "everybody wins" state or reset or something.
						StateMachine.ChangeState (eGameState.Draw);
					}
				}
			}
		}
	}

	private void StartNextRound()
    {
        _currentBpm = _bpms [_currentWaveIndex++];
		StateMachine.PushState (eGameState.InterimScore);
		ViewBindings.Instance.BindValue ("bpm", _currentBpm);
	}
}

