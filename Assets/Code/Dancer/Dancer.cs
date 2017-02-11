using UnityEngine;

public class Dancer : MonoBehaviour
{
    public Transform LeftArmSocket;
    public Transform RightArmSocket;
    public Transform LeftLegSocket;
    public Transform RightLegSocket;

    private Limb leftArm;
    private Limb rightArm;
    private Limb leftLeg;
    private Limb rightLeg;

    private Limb LoadAndParentLimb(string name, Transform socket)
    {
        GameObject prefab = Resources.Load<GameObject>(name);
        GameObject instance = GameObject.Instantiate(prefab);

        instance.transform.SetParent(socket, false);

        return instance.GetComponent<Limb>();
    }

    private void Awake()
    {
        leftArm = LoadAndParentLimb("Limbs/LeftArm", LeftArmSocket);
        rightArm = LoadAndParentLimb("Limbs/RightArm", RightArmSocket);
        leftLeg = LoadAndParentLimb("Limbs/LeftLeg", LeftLegSocket);
        rightLeg = LoadAndParentLimb("Limbs/RightLeg", RightLegSocket);
    }
}

