using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetPose
{
	private static int _instanceIndex;
    public Pose Pose;
	public bool HasExpired;

	private float _timeSinceLastBeat;
    private float _timeToMatchPose;
	public bool HasBeenJudged;

	public string ProgressKey;

	public float AgeProgress
	{
		get
		{
			return _ageInBeats / _beatsBeforeGuess;
		}
	}

	private float _beatsBeforeGuess = 16;
	private float _ageInBeats;

	public TargetPose()
	{
		_instanceIndex++;
		ProgressKey = "AgeProgress" + _instanceIndex;
	}

	public void Update()
	{
		_timeSinceLastBeat += Time.deltaTime;
		if (BeatManager.IsBeatFrame)
		{
			_ageInBeats++;
		}
		if (ViewBindings.Instance != null)
		{
			ViewBindings.Instance.BindValue<float> (ProgressKey, AgeProgress);
		}
	}

    public override string ToString()
    {
        string result = "PoseModel [ ";
        for(int i=0; i<Pose.LimbPoses.Length; i++)
        {
            result += Pose.LimbPoses[i].ToString();
        }
        result += " ]";
        return result;   
    }
}
