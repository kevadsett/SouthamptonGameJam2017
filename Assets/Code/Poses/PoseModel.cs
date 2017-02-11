using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoseModel
{


	public bool HasExpired;
	private float _expiryTime;
	private float _age = 0.0f;
	private Dictionary<eLimbType, LimbModel> _definition;
	public Dictionary<eLimbType, LimbModel> PoseDefinition
	{
		get
		{
			return _definition;
		}
	}

	private Dictionary<int, int> _limbToPoseCount;

	public PoseModel (float expiryTime)
	{
		_expiryTime = expiryTime;
		_definition = new Dictionary<eLimbType, LimbModel>
		{
			{ eLimbType.LeftArm, new LimbModel (4) },
			{ eLimbType.RightArm, new LimbModel (4) },
			{ eLimbType.LeftLeg, new LimbModel (2) },
			{ eLimbType.RightLeg, new LimbModel (2) }
		};
	}

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

	public void Randomise()
	{
		foreach (LimbModel limb in _definition.Values)
		{
			limb.Randomise ();
		}
	}

	public bool IsEqualTo(PoseModel other)
	{
		if (other.PoseDefinition.Count != _definition.Count)
		{
			return false;
		}

		foreach (KeyValuePair<eLimbType, LimbModel> kvp in _definition)
		{
			LimbModel myLimb = kvp.Value;
			LimbModel otherLimb;
			other.PoseDefinition.TryGetValue (kvp.Key, out otherLimb);
			if (otherLimb == null)
			{
				return false;
			}
			if (otherLimb.CurrentPosition != myLimb.CurrentPosition)
			{
				return false;
			}
		}

		return true;
	}

	public override string ToString()
	{
		string result = "PoseModel [ ";
		foreach (LimbModel limb in _definition.Values)
		{
			result += limb.CurrentPosition + " ";
		}
		result += "]";
		return result;
	}
}
