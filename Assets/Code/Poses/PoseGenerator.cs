using System;
using UnityEngine;
using System.Collections.Generic;

public class PoseGenerator
{
    private PoseLibrary _poseLibrary;
	private int _beatsPerPose = 12;
	private float _beatsSinceLastPose;

    public PoseGenerator (PoseLibrary poseLibrary)
    {
        _poseLibrary = poseLibrary;
	}

	public TargetPose AddPoseIfNeeded(List<TargetPose> poseList)
	{
		if (BeatManager.IsBeatFrame)
		{
			_beatsSinceLastPose++;
			if (_beatsSinceLastPose == _beatsPerPose)
			{
				_beatsSinceLastPose = 0;
				TargetPose newPose = GeneratePose ();
				poseList.Add (newPose);
				return newPose;
			}
		}
		return null;
	}

    public TargetPose GeneratePose()
    {
        return new TargetPose
        {
            Pose = _poseLibrary.GeneratePose(),
            HasExpired = false
        };
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

