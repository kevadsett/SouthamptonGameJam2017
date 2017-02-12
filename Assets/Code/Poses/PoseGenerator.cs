using System;
using UnityEngine;
using System.Collections.Generic;

public class PoseGenerator
{
    private PoseLibrary _poseLibrary;
	private int _beatsPerPose = 4;
	private float _beatsSinceLastPose;

    public PoseGenerator (PoseLibrary poseLibrary)
    {
        _poseLibrary = poseLibrary;
	}

	public TargetPose AddPoseIfNeeded(List<TargetPose> poseList)
	{
		TargetPose newPose = null;
		if (BeatManager.IsBeatFrame)
		{
			if (_beatsSinceLastPose == 0)
			{
				newPose = GeneratePose ();
				poseList.Add (newPose);
			}
			_beatsSinceLastPose = (_beatsSinceLastPose + 1) % _beatsPerPose;
		}
		return newPose;
	}

    public TargetPose GeneratePose()
    {
        return new TargetPose
        {
            Pose = _poseLibrary.GeneratePose(),
            HasExpired = false
        };
	}

	public void Reset()
	{
		_beatsSinceLastPose = 0;
	}

	private void DebugPrint(List<TargetPose> poseList)
	{
		string result = "[ - ";
		foreach (TargetPose pose in poseList)
		{
			result += pose + " - ";
		}
		result += "]";
		Debug.Log (result);
	}
}

