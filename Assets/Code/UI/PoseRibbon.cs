using System;
using UnityEngine;

public class PoseRibbon
{
	private GameObject _instance;
	private GameObject _poseDiagramPrefab;

	public PoseRibbon (GameObject prefab, GameObject poseDiagramPrefab, Transform parentCanvas)
	{
		_instance = GameObject.Instantiate (prefab) as GameObject;
		_instance.transform.SetParent (parentCanvas, false);
		_poseDiagramPrefab = poseDiagramPrefab;
	}

	public void AddNewPoseDiagram(TargetPose pose, LimbAnimation limbAnimation, PoserParts stickmanParts, PoseLibrary poseLibrary)
	{
		GameObject poseDiagram = GameObject.Instantiate (_poseDiagramPrefab) as GameObject;
        poseDiagram.GetComponent<PoseDiagramController>().Setup(pose, limbAnimation, stickmanParts, poseLibrary);
		poseDiagram.transform.SetParent (_instance.transform, false);
	}
}

