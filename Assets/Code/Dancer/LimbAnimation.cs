using UnityEngine;

[CreateAssetMenu]
public class LimbAnimation : ScriptableObject
{
    [Range(0f, 1f)]
    public float Duration;
    public AnimationCurve Curve;
}