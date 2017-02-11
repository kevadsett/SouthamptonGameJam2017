using System;
using UnityEngine;

public class PoseDiagramController : MonoBehaviour
{
    private TargetPose _pose;
    private RectTransform _rect;
    private RectTransform _parentRect;

	public void Setup(RectTransform parentRect, TargetPose pose, LimbAnimation limbAnimation, PoserParts stickmanParts, PoseLibrary poseLibrary)
	{
		_pose = pose;
		_rect = GetComponent<RectTransform> ();
        _parentRect = parentRect;

        KeyCode[] keycodes = new KeyCode[] { KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None };

        Poser stickman = Poser.CreatePoser(false, limbAnimation, stickmanParts, poseLibrary, keycodes);
        stickman.transform.parent = _rect;
        stickman.transform.localPosition = new Vector3(0, -20, 0);
        stickman.transform.localScale = 120f * Vector3.one;
        stickman.name = "Stickman";
        stickman.SetPose(pose.Pose);
	
        SetPosition(0);
    }

    private void SetPosition(float progress)
    {
        float width = _parentRect.rect.width;

        _rect.localPosition = new Vector3(0.5f * (width + _rect.rect.width) * (1f - progress), 0, 0);   
    }

    private void Update()
    {
        float progress;
        if(ViewBindings.Instance.TryGetBoundValue(_pose.ProgressKey, out progress))
        {
            SetPosition(progress);
        }
    }
}
