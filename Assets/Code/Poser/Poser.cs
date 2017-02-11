using UnityEngine;

public class Poser : MonoBehaviour
{
    public Transform LeftArmSocket;
    public Transform RightArmSocket;
    public Transform LeftLegSocket;
    public Transform RightLegSocket;
    public float Gravity;

    private float velocity;
    private Limb[] limbs;

    private Limb LoadAndParentLimb(string name, Transform socket)
    {
        GameObject prefab = Resources.Load<GameObject>(name);
        GameObject instance = GameObject.Instantiate(prefab);

        instance.transform.SetParent(socket, false);

        return instance.GetComponent<Limb>();
    }

    private void Awake()
    {
        limbs = new Limb[4];
        limbs[0] = LoadAndParentLimb("Limbs/LeftArm", LeftArmSocket);
        limbs[1] = LoadAndParentLimb("Limbs/RightArm", RightArmSocket);
        limbs[2] = LoadAndParentLimb("Limbs/LeftLeg", LeftLegSocket);
        limbs[3] = LoadAndParentLimb("Limbs/RightLeg", RightLegSocket);
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

    public PoseModel GetPose()
    {
        return null;
    }
}

