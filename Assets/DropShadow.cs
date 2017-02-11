using UnityEngine;

public class DropShadow : MonoBehaviour
{
    void LateUpdate()
    {
        Vector3 position = transform.position;
        position.y = 0.01f;
        transform.position = position;
    }
}