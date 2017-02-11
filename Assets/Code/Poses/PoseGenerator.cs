using System;
using UnityEngine;
using System.Collections.Generic;

public class PoseGenerator
{
    private PoseLibrary _poseLibrary;
	private float _secondsBetweenPoses;
	private float _timeSinceLastPoseGenerated;

	public PoseGenerator (PoseLibrary poseLibrary, float secondsBetweenPoses)
	{
        _poseLibrary = poseLibrary;
		_secondsBetweenPoses = secondsBetweenPoses;
	}

	public void Update(List<TargetPose> poseList)
	{
		_timeSinceLastPoseGenerated += Time.deltaTime;
		if (_timeSinceLastPoseGenerated >= _secondsBetweenPoses)
		{
			poseList.Add(GeneratePose ());
			_timeSinceLastPoseGenerated -= _secondsBetweenPoses;
			DebugPrint (poseList);
		}
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

