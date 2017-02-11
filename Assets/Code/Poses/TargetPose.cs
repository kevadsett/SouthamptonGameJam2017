using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetPose
{
    public Pose Pose;
	public bool HasExpired;
	private float _expiryTime = 6.0f;
	private float _age = 0.0f;

	public void Update()
	{
		if (HasExpired == false)
		{
			_age += Time.deltaTime;
			if (_age > _expiryTime)
			{
				HasExpired = true;
			}
		}
	}

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
