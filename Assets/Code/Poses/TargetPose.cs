using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetPose
{
	private static int _instanceIndex;
    public Pose Pose;
	public bool HasExpired;
    public float TimeToMatchPose;
	public bool HasBeenJudged;

	public string ProgressKey;

	public float AgeProgress
	{
		get
		{
			return _age / _expiryTime;
		}
	}

	private float _expiryTime = 6.0f;
	private float _age = 0.0f;

	public TargetPose()
	{
		_instanceIndex++;
		ProgressKey = "AgeProgress" + _instanceIndex;
	}

	public void Update()
	{
		_age += Time.deltaTime;
		if (_age > _expiryTime)
		{
			HasExpired = true;
		}
		if (ViewBindings.Instance != null)
		{
			ViewBindings.Instance.BindValue<float> (ProgressKey, AgeProgress);
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
