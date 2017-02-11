using System;
using UnityEngine;
using System.Collections.Generic;

public class PoseGenerator
{
    private PoseLibrary _poseLibrary;
	private float _timeToMatchPose;
	private float _timeBetweenGenerations;
	private float _timeSinceLastPoseGenerated;

    public PoseGenerator (PoseLibrary poseLibrary, float timeToMatchPose)
    {
        _poseLibrary = poseLibrary;
		_timeToMatchPose = timeToMatchPose;
		_timeBetweenGenerations = _timeSinceLastPoseGenerated = _timeToMatchPose / 3;
	}

	public TargetPose AddPoseIfNeeded(List<TargetPose> poseList)
	{
		_timeSinceLastPoseGenerated += Time.deltaTime;
		if (_timeSinceLastPoseGenerated >= _timeBetweenGenerations)
		{
			TargetPose newPose = GeneratePose ();
			poseList.Add(newPose);
			_timeSinceLastPoseGenerated -= _timeBetweenGenerations;
			return newPose;
		}
		return null;
	}

    public TargetPose GeneratePose()
    {
        return new TargetPose
        {
            Pose = _poseLibrary.GeneratePose(),
            TimeToMatchPose = _timeToMatchPose,
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

