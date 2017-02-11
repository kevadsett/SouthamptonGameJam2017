using System;
using UnityEngine;

public class PoseDiagramController : MonoBehaviour
{
	private TargetPose _pose;
	private bool _maximised;
	private RectTransform _rect;
	public void SetPose(TargetPose pose)
	{
		_pose = pose;
		_rect = GetComponent<RectTransform> ();
		PoseDiagramPivotBinder pivotBinder = GetComponent<PoseDiagramPivotBinder> ();
		pivotBinder.Key = pose.ProgressKey;
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
