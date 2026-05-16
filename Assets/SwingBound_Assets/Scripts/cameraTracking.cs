using UnityEngine;

public class cameraTracking : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -8);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        void LateUpdate()
    {
        if (target)
        {
            transform.position = target.position + offset;
            transform.LookAt(target.position);
        }
    }
}

