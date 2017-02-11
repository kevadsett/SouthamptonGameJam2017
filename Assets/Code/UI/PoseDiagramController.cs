using System;
using UnityEngine;

public class PoseDiagramController : MonoBehaviour
{
	private TargetPose _pose;
	private bool _maximised;
	private RectTransform _rect;
    private Poser _stickman;

	public void Setup(TargetPose pose, LimbAnimation limbAnimation, PoserParts stickmanParts, PoseLibrary poseLibrary)
	{
		_pose = pose;
		_rect = GetComponent<RectTransform> ();

		PoseDiagramPivotBinder pivotBinder = GetComponent<PoseDiagramPivotBinder> ();
		pivotBinder.Key = pose.ProgressKey;

        KeyCode[] keycodes = new KeyCode[] { KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None };

        Poser stickman = Poser.CreatePoser(false, limbAnimation, stickmanParts, poseLibrary, keycodes);
        stickman.transform.localScale = 40f * Vector3.one;
        stickman.transform.parent = _rect;
        stickman.name = "Stickman";
        stickman.SetPose(pose.Pose);
	}

	void Update()
	{
		if (_pose == null)
		{
			return;
		}

		if (_pose.HasBeenJudged)
		{
			float newScale;
			if (_maximised == false)
			{
				newScale = 1.5f;
				_maximised = true;
			}
			else
			{
				newScale = _rect.localScale.x - 0.3f * Time.deltaTime;
			}
			newScale = Mathf.Max (newScale, 0);
			_rect.localScale = new Vector3 (newScale, newScale, newScale);
		}
	}
}
