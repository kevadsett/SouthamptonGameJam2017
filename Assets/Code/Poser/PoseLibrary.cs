using UnityEngine;

[CreateAssetMenu]
public class PoseLibrary : ScriptableObject
{
    public LimbPose[] LeftArmPoses;
    public LimbPose[] RightArmPoses;
    public LimbPose[] LeftLegPoses;
    public LimbPose[] RightLegPoses;

    public Pose GeneratePose()
    {
        return new Pose
        {
            LimbPoses = new int[]
            {
                Random.Range(0, LeftArmPoses.Length),
                Random.Range(0, RightArmPoses.Length),
                Random.Range(0, LeftLegPoses.Length),
                Random.Range(0, RightLegPoses.Length)
            }
        };
    }
}