using System;
using UnityEngine;

public class PoseDiagramController : MonoBehaviour
{
    private const int _visibleBars = 7;

    private TargetPose _targetPose;
    private RectTransform _rect;
    private RectTransform _parentRect;
    private Poser _stickman;

    private float _currentProgress;
    private float _targetProgress;

	public void Setup(RectTransform parentRect, TargetPose targetPose, LimbAnimation limbAnimation, PoserParts stickmanParts, PoseLibrary poseLibrary)
	{
		_rect = GetComponent<RectTransform> ();
        _parentRect = parentRect;
        _targetPose = targetPose;

        KeyCode[] keycodes = new KeyCode[] { KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None };

        _stickman = Poser.CreatePoser(false, limbAnimation, stickmanParts, poseLibrary, keycodes);
        _stickman.transform.SetParent(_rect, false);
        _stickman.transform.localPosition = new Vector3(0, 15f, 0);
        _stickman.transform.localScale = 120f * Vector3.one;
        _stickman.name = "Stickman";
        _stickman.SetPose(targetPose.Pose);

        _currentProgress = GetTargetProgress();
        _targetProgress = _currentProgress;

        SetPosition();
    }

    private float GetTargetProgress()
    {
        int currentBeat = BeatManager.CurrentBeat;
        int targetBeat = _targetPose.Beat + 1;
        int barIndex = - 1 -Mathf.FloorToInt((currentBeat - targetBeat) / (float)GameData.BeatsPerPose);

        return 0.5f + barIndex / (float)_visibleBars;
    }

    private void SetPosition()
    {
        float width = _parentRect.rect.width;

        float x = _parentRect.rect.width * (_currentProgress - 0.5f);
        float y = _rect.localPosition.y;

        _rect.localPosition = new Vector3(x, y, 0f);
    }

    private void Update()
    {
        _targetProgress = GetTargetProgress();
        _currentProgress = Mathf.MoveTowards(_currentProgress, _targetProgress, Time.deltaTime);

        SetPosition();
    }
}
