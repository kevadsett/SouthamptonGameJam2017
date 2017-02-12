using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class StickmanCamera : MonoBehaviour
{
    private Camera _stickmanCamera;

    void Awake()
    {
        _stickmanCamera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        _stickmanCamera.orthographicSize = 0.5f * Screen.height;
    }
}