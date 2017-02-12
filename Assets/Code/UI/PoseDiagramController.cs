using System;
using UnityEngine;

public class PoseDiagramController : MonoBehaviour
{
    private RectTransform _rect;
    private RectTransform _parentRect;
	private float _moveSpeed = 100f;
    private Poser _stickman;

	public void Setup(RectTransform parentRect, TargetPose pose, LimbAnimation limbAnimation, PoserParts stickmanParts, PoseLibrary poseLibrary)
	{
		_rect = GetComponent<RectTransform> ();
        _parentRect = parentRect;

        KeyCode[] keycodes = new KeyCode[] { KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None };

        _stickman = Poser.CreatePoser(false, limbAnimation, stickmanParts, poseLibrary, keycodes);
        _stickman.transform.SetParent(_rect, false);
        _stickman.transform.localPosition = new Vector3(0, 15f, 0);
        _stickman.transform.localScale = 120f * Vector3.one;
        _stickman.name = "Stickman";
        _stickman.SetPose(pose.Pose);
	
        SetPosition(0);
    }

    private void SetPosition(float dt)
    {
        _rect.localPosition = new Vector3 (_rect.localPosition.x - (_moveSpeed * Time.deltaTime), _rect.localPosition.y, _rect.localPosition.z);
    }

    private void Update()
    {
        SetPosition(Time.deltaTime);
    }
}
