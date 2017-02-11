using System;
using UnityEngine;
using System.Collections.Generic;

public class PoseGenerator
{
	private float _secondsBetweenPoses;
	private float _timeSinceLastPoseGenerated;

	public PoseGenerator (float secondsBetweenPoses)
	{
		_secondsBetweenPoses = secondsBetweenPoses;
	}

	public void Update(List<PoseModel> poseList)
	{
		
		_timeSinceLastPoseGenerated += Time.deltaTime;
		if (_timeSinceLastPoseGenerated >= _secondsBetweenPoses)
		{
			poseList.Add(GeneratePose ());
			_timeSinceLastPoseGenerated -= _secondsBetweenPoses;
			DebugPrint (poseList);
		}
	}

	public PoseModel GeneratePose()
	{
		PoseModel pose = new PoseModel ();
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

