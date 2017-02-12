using System;

public static class BeatManager
{
	private static float _timeSinceLastBeat;

	public static bool IsBeatFrame;

	public static int CurrentBeat;

	public static void Update(float dt)
	{
		float bpm;
		ViewBindings.Instance.TryGetBoundValue ("bpm", out bpm);
		float beatsPerSecond = bpm / 60f;
		float beatTime = 1.0f / beatsPerSecond;
		_timeSinceLastBeat += dt;
		if (_timeSinceLastBeat >= beatTime)
		{
			_timeSinceLastBeat -= beatTime;
			IsBeatFrame = true;
            CurrentBeat = CurrentBeat + 1;
		}
		else
		{
			IsBeatFrame = false;
		}
	}

	public static void Reset()
	{
		_timeSinceLastBeat = 0;
		IsBeatFrame = false;
		CurrentBeat = 0;
	}
}
