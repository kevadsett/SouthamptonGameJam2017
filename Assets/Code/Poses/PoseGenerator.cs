using System;
using UnityEngine;

public class PoseGenerator
{
	private float _secondsBetweenPoses;
	private float _timeSinceLastPoseGenerated;
	public PoseGenerator (float secondsBetweenPoses)
	{
		_secondsBetweenPoses = secondsBetweenPoses;
		GeneratePose ();
	}

	public void Update()
	{
		_timeSinceLastPoseGenerated += Time.deltaTime;
		if (_timeSinceLastPoseGenerated >= _secondsBetweenPoses)
		{
			GeneratePose ();
			_timeSinceLastPoseGenerated -= _secondsBetweenPoses;
		}
	}

	public PoseModel GeneratePose()
	{
		PoseModel pose = new PoseModel ();
		pose.Randomise ();
		Debug.Log ("Generated " + pose);
		return pose;
	}
}

