using UnityEngine;

public class MenuCameraAnimation : MonoBehaviour
{
    public Transform target;
    public float orbitSpeed = 50f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
    }
}
