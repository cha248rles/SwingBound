using UnityEngine;

public class TetherFollow : MonoBehaviour
{
    public Transform cam;

    void LateUpdate()
    {
        if (cam)
        {
            transform.position = cam.position;
            transform.rotation = cam.rotation;
        }
    }
}