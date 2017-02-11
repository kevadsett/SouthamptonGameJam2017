using UnityEngine;

public class Limb : MonoBehaviour
{
    public LimbAnimation LimbAnimation;

    [Space]
    public LimbPose[] Poses;
    public SpriteRenderer UpperBone;
    public SpriteRenderer LowerBone;
    public Transform CollisionPoint;

    private KeyCode keycode;
    private int previousPoseIndex;
    private int currentPoseIndex;
    private float transitionTime;

    public int Current
    {
        get { return currentPoseIndex; }
    }

    public int RandomPose
    {
        get { return Random.Range(0, Poses.Length); }
    }

    public void Setup(KeyCode keycode)
    {
        this.keycode = keycode;
    }

    private void Awake()
    {
        transitionTime = LimbAnimation.Duration;
        previousPoseIndex = Random.Range(0, Poses.Length);
        currentPoseIndex = (previousPoseIndex + 1) % Poses.Length;
    }

    private void RotateBone(Transform bone, float previousRotation, float currentRotation, float t)
    {
        float rotation = Mathf.LerpAngle(previousRotation, currentRotation, t);
        bone.localRotation = Quaternion.Euler(0, 0, rotation);
    }

    private void Update()
    {
        transitionTime = Mathf.MoveTowards(transitionTime, LimbAnimation.Duration, Time.deltaTime);

        if(Input.GetKey(keycode))
        {
            transitionTime = 0f;
            previousPoseIndex = currentPoseIndex;
            currentPoseIndex = (previousPoseIndex + 1) % Poses.Length;
        }

        float currentLerp = LimbAnimation.Curve.Evaluate(transitionTime / LimbAnimation.Duration);

        LimbPose previousPose = Poses[previousPoseIndex];
        LimbPose currentPose = Poses[currentPoseIndex];

        RotateBone(UpperBone.transform, previousPose.UpperRotation, currentPose.UpperRotation, currentLerp);
        RotateBone(LowerBone.transform, previousPose.LowerRotation, currentPose.LowerRotation, currentLerp);
    }
}
