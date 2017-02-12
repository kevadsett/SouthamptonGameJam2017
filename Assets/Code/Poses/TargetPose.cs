using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetPose
{
	private static int _instanceIndex;
    public Pose Pose;
    public int Beat;
	public bool HasBeenJudged;
    public GameObject Diagram;

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
