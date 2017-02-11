using UnityEngine;

[CreateAssetMenu]
public class PoserParts : ScriptableObject
{
    public PoserTorso Torso;
    public GameObject Head;
    public LimbPart UpperArm;
    public LimbPart LowerArm;
    public LimbPart UpperLeg;
    public LimbPart LowerLeg;
}