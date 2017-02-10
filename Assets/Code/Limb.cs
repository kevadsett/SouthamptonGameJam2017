﻿using UnityEngine;

public class Limb : MonoBehaviour
{
    [Range(0f, 1f)]
    public float TransitionDuration;
    public LimbAnimation LimbAnimation;

    [Space]
    public LimbPose[] Poses;
    public SpriteRenderer UpperBone;
    public SpriteRenderer LowerBone;

    private int previousPoseIndex;
    private int currentPoseIndex;
    private float transitionTime;

    private void Awake()
    {
        transitionTime = TransitionDuration;
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
        transitionTime = Mathf.MoveTowards(transitionTime, TransitionDuration, Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Q))
        {
            transitionTime = 0f;
            previousPoseIndex = currentPoseIndex;
            currentPoseIndex = (previousPoseIndex + 1) % Poses.Length;
        }

        float currentLerp = LimbAnimation.Curve.Evaluate(transitionTime / TransitionDuration);

        LimbPose previousPose = Poses[previousPoseIndex];
        LimbPose currentPose = Poses[currentPoseIndex];

        RotateBone(UpperBone.transform, previousPose.UpperRotation, currentPose.UpperRotation, currentLerp);
        RotateBone(LowerBone.transform, previousPose.LowerRotation, currentPose.LowerRotation, currentLerp);
    }
}
