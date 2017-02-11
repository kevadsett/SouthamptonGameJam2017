using System;
using UnityEngine;

public class PoseRibbon : MonoBehaviour
{
    public RectTransform BeltTransform;

	private GameObject _poseDiagramPrefab;

    public void Setup(GameObject poseDiagramPrefab)
	{
		_poseDiagramPrefab = poseDiagramPrefab;
	}

	public void AddNewPoseDiagram(TargetPose pose, LimbAnimation limbAnimation, PoserParts stickmanParts, PoseLibrary poseLibrary)
	{
		GameObject poseDiagram = GameObject.Instantiate(_poseDiagramPrefab);
        poseDiagram.GetComponent<PoseDiagramController>().Setup(GetComponent<RectTransform>(), pose, limbAnimation, stickmanParts, poseLibrary);
		poseDiagram.transform.SetParent(BeltTransform, false);
	}
}

