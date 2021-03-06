using UnityEngine;

public class Limb : MonoBehaviour
{
    private LimbAnimation _limbAnimation;

    private LimbPart _upperLimb;
    private LimbPart _lowerLimb;
    private Transform _collisionPoint;

    private LimbPose[] _poses;
    private KeyCode _keycode;

    private int _previousPoseIndex;
    private int _currentPoseIndex;
    private float _transitionTime;

    public int CurrentPose
    {
        get { return _currentPoseIndex; }
    }

    public Transform CollisionPoint
    {
        get { return _collisionPoint; }
    }

    public static Limb CreateLimb(LimbAnimation limbAnimation, LimbPose[] poses, LimbPart upperLimbPrefab, LimbPart lowerLimbPrefab, KeyCode keycode, bool flipX)
    {
        GameObject gameObject = new GameObject("limb", typeof(Limb));
        Limb limb = gameObject.GetComponent<Limb>();

        LimbPart upperLimb = GameObject.Instantiate(upperLimbPrefab).GetComponent<LimbPart>();
        LimbPart lowerLimb = GameObject.Instantiate(lowerLimbPrefab).GetComponent<LimbPart>();

        upperLimb.transform.SetParent(limb.transform, false);
        upperLimb.transform.localPosition = Vector3.zero;
        upperLimb.transform.localRotation = Quaternion.identity;

        lowerLimb.transform.SetParent(upperLimb.Pivot, false);
        lowerLimb.transform.localPosition = Vector3.zero;
        lowerLimb.transform.localRotation = Quaternion.identity;

        if(flipX)
        {
            Vector3 upperPivotPosition = upperLimb.Pivot.transform.localPosition;
            upperPivotPosition.x *= -1;
            upperLimb.Pivot.transform.localPosition = upperPivotPosition;

            upperLimb.GetComponent<SpriteRenderer>().flipX = true;
            lowerLimb.GetComponent<SpriteRenderer>().flipX = true;
        }

        limb.Setup(limbAnimation, poses, upperLimb, lowerLimb, lowerLimb.Pivot, keycode);

        return limb;
    }

    private void Setup(LimbAnimation limbAnimation, LimbPose[] poses, LimbPart upperLimb, LimbPart lowerLimb, Transform collisionPoint, KeyCode keycode)
    {
        _limbAnimation = limbAnimation;

        _upperLimb = upperLimb;
        _lowerLimb = lowerLimb;
        _collisionPoint = collisionPoint;

        _poses = poses;
        _keycode = keycode;

        _previousPoseIndex = 0;
        _currentPoseIndex = 0;
        _transitionTime = _limbAnimation.Duration;

        ApplyPose();
    }

    private void RotateBone(Transform bone, float previousRotation, float currentRotation, float t)
    {
        float rotation = Mathf.LerpAngle(previousRotation, currentRotation, t);
        bone.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    private void ApplyPose()
    {
        float currentLerp = _limbAnimation.Curve.Evaluate(_transitionTime / _limbAnimation.Duration);

        LimbPose previousPose = _poses[_previousPoseIndex];
        LimbPose currentPose = _poses[_currentPoseIndex];

        RotateBone(_upperLimb.transform, previousPose.UpperRotation, currentPose.UpperRotation, currentLerp);
        RotateBone(_lowerLimb.transform, previousPose.LowerRotation, currentPose.LowerRotation, currentLerp);
    }

	public void Update()
    {
        _transitionTime = Mathf.MoveTowards(_transitionTime, _limbAnimation.Duration, Time.deltaTime);

        if(Input.GetKeyDown(_keycode))
        {
            _transitionTime = 0f;
            NextPose ();
        }

        ApplyPose();
    }

	private bool goingUp;

	private void NextPose()
	{
		_previousPoseIndex = _currentPoseIndex;

		if (goingUp)
		{
			_currentPoseIndex++;
			if (_currentPoseIndex >= _poses.Length)
			{
				_currentPoseIndex = _poses.Length - 2;
				goingUp = false;
			}
		}
		else
		{
			_currentPoseIndex--;
			if (_currentPoseIndex < 0)
			{
				_currentPoseIndex = 1;
				goingUp = true;
			}
		}
	}

    public void SetPose(int poseIndex)
    {
        _previousPoseIndex = poseIndex;
        _currentPoseIndex = poseIndex;
        _transitionTime = _limbAnimation.Duration;
    }
}
