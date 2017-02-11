﻿using UnityEngine;

public class Poser : MonoBehaviour
{
    public Transform LeftArmSocket;
    public Transform RightArmSocket;
    public Transform LeftLegSocket;
    public Transform RightLegSocket;
    public float Gravity;

    private float velocity;
    private Limb[] limbs;

    private void SetupLimb(eLimbType limbType, Transform socket, LimbPose[] poses,KeyCode[] controls)
    {
        GameObject prefab = Resources.Load<GameObject>("Limbs/" + limbType);
        GameObject instance = GameObject.Instantiate(prefab);

        instance.transform.SetParent(socket, false);

        Limb limb = instance.GetComponent<Limb>();

        int limbIndex = (int)limbType;

        limb.Setup(poses, controls[limbIndex]);

        limbs[limbIndex] = limb;
    }

    public void Setup(PoseLibrary poseLibrary, KeyCode[] controls)
    {
        limbs = new Limb[4];
        SetupLimb(eLimbType.LeftArm, LeftArmSocket, poseLibrary.LeftArmPoses, controls);
        SetupLimb(eLimbType.RightArm, RightArmSocket, poseLibrary.RightArmPoses, controls);
        SetupLimb(eLimbType.LeftLeg, LeftLegSocket, poseLibrary.LeftLegPoses, controls);
        SetupLimb(eLimbType.RightLeg, RightLegSocket, poseLibrary.RightLegPoses, controls);
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        velocity += Gravity * dt;

        transform.localPosition += new Vector3(0, velocity, 0);

        float minY = float.MaxValue;
        for(int i=0; i<limbs.Length; i++)
        {
            minY = Mathf.Min(limbs[i].CollisionPoint.position.y, minY);
        }

        if(minY < 0f)
        {
            transform.localPosition -= new Vector3(0, minY, 0);
        }
    }

    public Pose GetCurrentPose()
    {
        return new Pose
        {
            LimbPoses = new int[]
            {
                limbs[0].CurrentPose,
                limbs[1].CurrentPose,
                limbs[2].CurrentPose,
                limbs[3].CurrentPose
            }
        };
    }
}
