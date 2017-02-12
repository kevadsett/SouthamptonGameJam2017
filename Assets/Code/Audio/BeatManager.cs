using System;

public static class BeatManager
{
	private static float _timeSinceLastBeat;

	public static bool IsBeatFrame;

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
		}
		else
		{
			IsBeatFrame = false;
		}
	}
}
