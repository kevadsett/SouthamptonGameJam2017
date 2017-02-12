using System;
using UnityEngine;
using System.Collections.Generic;

public class PoseManager
{
    private PoseLibrary _poseLibrary;
    private List<TargetPose> _targetPoses;

    public PoseManager(PoseLibrary poseLibrary)
    {
        _poseLibrary = poseLibrary;
        _targetPoses = new List<TargetPose>();
	}

	public void GeneratePosesForRound(int count, int waveIndex)
    {
		int mutation = 2;
		if (mutation == 0)
		{
			mutation = 1;
		}
        for(int i=0; i<_targetPoses.Count; i++)
        {
            GameObject.Destroy(_targetPoses[i].Diagram);
        }

        _targetPoses.Clear();

        for(int i=0; i<count; i++)
        {
            TargetPose newPose = new TargetPose
            {
                Pose = _poseLibrary.GeneratePose(mutation),
                Beat = GameData.BeatsPerPose * i
            };

            newPose.Diagram = GameData.PoseRibbon.AddNewPoseDiagram(newPose, GameData.LimbAnimation, GameData.StickmanParts, GameData.PoseLibrary);

            _targetPoses.Add(newPose);
        }
    }

    public TargetPose FirstPose
    {
        get { return _targetPoses[0]; }
    }

    public TargetPose GetTargetPose(int index)
    {
        return _targetPoses[index];
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

