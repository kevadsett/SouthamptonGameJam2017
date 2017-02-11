using UnityEngine;
using System;

[CreateAssetMenu]
public class PoseLibrary : ScriptableObject
{
    public LimbPose[] LeftArmPoses;
    public LimbPose[] RightArmPoses;
    public LimbPose[] LeftLegPoses;
    public LimbPose[] RightLegPoses;

	static int lastLA, lastRA, lastLL, lastRL;

	public Pose GeneratePose(int mutations = 2)
    {
		int[] limbPoses = new int[] { lastLA, lastRA, lastLL, lastRL };
		int[] newLimbPoses = limbPoses;

		while (PosesToStr (limbPoses) == PosesToStr (newLimbPoses))
		{
			for (int i = 0; i < mutations; i++)
			{
				
				newLimbPoses = Mutate (limbPoses);
			}
		}

		Debug.Log (PosesToStr (newLimbPoses));

        return new Pose
        {
			LimbPoses = newLimbPoses
        };
    }

	private string PosesToStr (int[] limbPoses)
	{
		string str = "";
		for (int i = 0; i < 4; i++)
		{
			str += limbPoses[i] + " : ";
		}

		return str;
	}

	private int[] Mutate (int[] limbPoses)
	{
		limbPoses = limbPoses.Clone () as int[];

		int r = UnityEngine.Random.Range (0, 4);

		if (r == 0) limbPoses [0] = lastLA = (lastLA + 1) % LeftArmPoses.Length;
		if (r == 1) limbPoses [1] = lastRA = (lastRA + 1) % RightArmPoses.Length;
		if (r == 2) limbPoses [2] = lastLL = (lastLL + 1) % LeftLegPoses.Length;
		if (r == 3) limbPoses [3] = lastRL = (lastRL + 1) % RightLegPoses.Length;

		return limbPoses;
	}
}