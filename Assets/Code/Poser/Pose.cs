public class Pose
{
    public int[] LimbPoses;

    public bool Equals(Pose other)
    {
        if(LimbPoses.Length != other.LimbPoses.Length)
        {
            return false;
        }

        for(int i=0; i<LimbPoses.Length; i++)
        {
            if(LimbPoses[i] != other.LimbPoses[i])
            {
                return false;
            }
        }

        return true;
    }
}