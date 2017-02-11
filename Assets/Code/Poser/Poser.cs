using UnityEngine;

public class Poser : MonoBehaviour
{
    private const float _gravity = -1f;

    private float _velocity;
    private PoserTorso _torso;
    private Limb[] _limbs;
    private bool _doUpdate;

    private static void SetupLimb(Limb[] limbs, eLimbType limbType, Transform socket, LimbAnimation limbAnimation, LimbPose[] poses, LimbPart upperLimbPrefab, LimbPart lowerLimbPrefab, KeyCode[] controls, bool flipX)
    {
        int limbIndex = (int)limbType;

        Limb limb = Limb.CreateLimb(limbAnimation, poses, upperLimbPrefab, lowerLimbPrefab, controls[limbIndex], flipX);

        limb.transform.SetParent(socket.transform);
        limb.transform.localPosition = Vector3.zero;
        limb.transform.localRotation = Quaternion.identity;

        limbs[limbIndex] = limb;
    }

    public static Poser CreatePoser(bool doUpdate, LimbAnimation limbAnimation, PoserParts poserParts, PoseLibrary poseLibary, KeyCode[] controls)
    {
        GameObject gameObject = new GameObject("poser", typeof(Poser));
        Poser poser = gameObject.GetComponent<Poser>();

        PoserTorso torso = GameObject.Instantiate(poserParts.Torso).GetComponent<PoserTorso>();
        torso.transform.SetParent(poser.transform, false);
        torso.transform.localPosition = Vector3.zero;
        torso.transform.localRotation = Quaternion.identity;

        GameObject head = GameObject.Instantiate(poserParts.Head);
        head.transform.SetParent(torso.HeadSocket, false);
        head.transform.localPosition = Vector3.zero;
        head.transform.localRotation = Quaternion.identity;

        Limb[] limbs = new Limb[4];

        SetupLimb(
            limbs,
            eLimbType.LeftArm,
            torso.LeftArmSocket,
            limbAnimation,
            poseLibary.LeftArmPoses,
            poserParts.UpperArm,
            poserParts.LowerArm,
            controls,
            false
        );

        SetupLimb(
            limbs,
            eLimbType.RightArm,
            torso.RightArmSocket,
            limbAnimation,
            poseLibary.RightArmPoses,
            poserParts.UpperArm,
            poserParts.LowerArm,
            controls,
            true
        );

        SetupLimb(
            limbs,
            eLimbType.LeftLeg,
            torso.LeftLegSocket,
            limbAnimation,
            poseLibary.LeftLegPoses,
            poserParts.UpperLeg,
            poserParts.LowerLeg,
            controls,
            false
        );

        SetupLimb(
            limbs,
            eLimbType.RightLeg,
            torso.RightLegSocket,
            limbAnimation,
            poseLibary.RightLegPoses,
            poserParts.UpperLeg,
            poserParts.LowerLeg,
            controls,
            true
        );

        poser.Setup(doUpdate, torso, limbs);

        return poser;
    }

    private void Setup(bool doUpdate, PoserTorso torso, Limb[] limbs)
    {
        _doUpdate = doUpdate;
        _torso = torso;
        _limbs = limbs;
    }

    private void Update()
    {
        if(_doUpdate)
        {
            float dt = Time.deltaTime;
            _velocity += _gravity * dt;

            transform.localPosition += new Vector3(0, _velocity, 0);

            float minY = float.MaxValue;
            for(int i=0; i<_limbs.Length; i++)
            {
                minY = Mathf.Min(_limbs[i].CollisionPoint.position.y, minY);   
            }
            minY = Mathf.Min(_torso.Bottom.position.y, minY);

            if(minY < 0f)
            {
                transform.localPosition -= new Vector3(0, minY, 0);
            }
        }
    }

    public void SetPose(Pose pose)
    {
        for(int i=0; i<pose.LimbPoses.Length; i++)
        {
            _limbs[i].SetPose(pose.LimbPoses[i]);
        }
    }

    public Pose GetCurrentPose()
    {
        return new Pose
        {
            LimbPoses = new int[]
            {
                _limbs[0].CurrentPose,
                _limbs[1].CurrentPose,
                _limbs[2].CurrentPose,
                _limbs[3].CurrentPose
            }
        };
    }
}

