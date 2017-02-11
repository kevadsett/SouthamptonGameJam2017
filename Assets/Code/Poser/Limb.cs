using UnityEngine;

public class Limb : MonoBehaviour
{
    public LimbAnimation LimbAnimation;

    [Space]
    public SpriteRenderer UpperBone;
    public SpriteRenderer LowerBone;
    public Transform CollisionPoint;

    private LimbPose[] _poses;
    private KeyCode _keycode;
    private int _previousPoseIndex;
    private int _currentPoseIndex;
    private float _transitionTime;

    public int CurrentPose
    {
        get { return _currentPoseIndex; }
    }

    public void Setup(LimbPose[] poses, KeyCode keycode)
    {
        _poses = poses;
        _keycode = keycode;
        _transitionTime = LimbAnimation.Duration;
        _previousPoseIndex = Random.Range(0, poses.Length);
        _currentPoseIndex = (_previousPoseIndex + 1) % poses.Length;
    }

    private void RotateBone(Transform bone, float previousRotation, float currentRotation, float t)
    {
        float rotation = Mathf.LerpAngle(previousRotation, currentRotation, t);
        bone.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    private void Update()
    {
        _transitionTime = Mathf.MoveTowards(_transitionTime, LimbAnimation.Duration, Time.deltaTime);

        if(Input.GetKeyDown(_keycode))
        {
            _transitionTime = 0f;
            _previousPoseIndex = _currentPoseIndex;
            _currentPoseIndex = (_previousPoseIndex + 1) % _poses.Length;
        }

        float currentLerp = LimbAnimation.Curve.Evaluate(_transitionTime / LimbAnimation.Duration);

        LimbPose previousPose = _poses[_previousPoseIndex];
        LimbPose currentPose = _poses[_currentPoseIndex];

        RotateBone(UpperBone.transform, previousPose.UpperRotation, currentPose.UpperRotation, currentLerp);
        RotateBone(LowerBone.transform, previousPose.LowerRotation, currentPose.LowerRotation, currentLerp);
    }
}
