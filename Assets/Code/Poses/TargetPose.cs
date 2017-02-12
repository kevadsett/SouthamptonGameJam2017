using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetPose
{
	private static int _instanceIndex;
    public Pose Pose;
	public bool HasExpired;

	public bool HasBeenJudged;

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
