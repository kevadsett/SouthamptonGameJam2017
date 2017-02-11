using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoseController {
	private Dictionary<eLimbType, KeyCode> _limbKeys;

	private PoseModel _poseModel;

	public PoseController(KeyCode leftArm, KeyCode rightArm, KeyCode leftLeg, KeyCode rightLeg)
	{
		_poseModel = new PoseModel();
		_limbKeys = new Dictionary<eLimbType, KeyCode> ()
		{
			{ eLimbType.LeftArm, leftArm },
			{ eLimbType.RightArm, rightArm },
			{ eLimbType.LeftLeg, leftLeg },
			{ eLimbType.RightLeg, rightLeg }
		};
	}

	public void Update () {
		foreach (KeyValuePair<eLimbType, KeyCode> kvp in _limbKeys)
		{
			if (Input.GetKeyDown(kvp.Value))
			{
				_poseModel.PoseDefinition[kvp.Key].Cycle();
				Debug.Log (kvp.Key + " now at pose : " + _poseModel.PoseDefinition [kvp.Key].CurrentPosition);
			}
		}
	}

	public bool IsPoseCorrect(PoseModel pose)
	{
		return _poseModel.IsEqualTo (pose);
	}
}
