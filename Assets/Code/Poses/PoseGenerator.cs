using System;
using UnityEngine;
using System.Collections.Generic;

public class PoseGenerator
{
	private float _timeToMatchPose;
	private float _timeBetweenGenerations;
	private float _timeSinceLastPoseGenerated;

	public PoseGenerator (float timeToMatchPose)
	{
		_timeToMatchPose = timeToMatchPose;
		_timeBetweenGenerations = _timeSinceLastPoseGenerated = _timeToMatchPose / 3;
	}

	public void Update(List<PoseModel> poseList)
	{
		
		_timeSinceLastPoseGenerated += Time.deltaTime;
		if (_timeSinceLastPoseGenerated >= _timeBetweenGenerations)
		{
			poseList.Add(GeneratePose ());
			_timeSinceLastPoseGenerated -= _timeBetweenGenerations;
			DebugPrint (poseList);
		}
	}

	public PoseModel GeneratePose()
	{
		PoseModel pose = new PoseModel (_timeToMatchPose);
		pose.Randomise ();
		return pose;
	}

	private void DebugPrint(List<PoseModel> poseList)
	{
		string result = "[ - ";
		foreach (PoseModel pose in poseList)
		{
			result += pose + " - ";
		}
		result += "]";
		Debug.Log (result);
	}
}

