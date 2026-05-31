using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform player;
    public float heightOffset = 1f;     

    void LateUpdate()
    {
        if(!player)
            return;
        // stays upright regardless of how the ball rolls, never copies rotation
        if (player)
            transform.position = player.position + new Vector3(0, heightOffset, 0);
    }
}