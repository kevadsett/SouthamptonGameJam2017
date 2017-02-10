using System;

using UnityEngine;

[CreateAssetMenu]
public class LimbPose : ScriptableObject
{
    [Range(-360f, 360f)]
    public float UpperRotation = 0f;

    [Range(-360f, 360f)]
    public float LowerRotation = 0f;
}