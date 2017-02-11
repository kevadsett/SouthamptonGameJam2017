using System;
using UnityEngine;

public class PoseDiagramController : MonoBehaviour
{
	private TargetPose _pose;
	private bool _maximised;
	public void SetPose(TargetPose pose)
	{
		_pose = pose;
	}

	void Update()
	{
		if (_pose == null)
		{
			return;
		}
		float newXPivotPoint = ((1.0f - _pose.AgeProgress) * 0.5f) + 0.5f;
		RectTransform rectTransform = GetComponent<RectTransform>();
		rectTransform.pivot = new Vector2 (newXPivotPoint, rectTransform.pivot.y);
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
				newScale = rectTransform.localScale.x - 0.3f * Time.deltaTime;
			}
			newScale = Mathf.Max (newScale, 0);
			rectTransform.localScale = new Vector3 (newScale, newScale, newScale);
		}
	}
}
