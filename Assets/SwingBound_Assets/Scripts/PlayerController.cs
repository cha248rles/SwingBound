using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 5;

    bool isGrounded;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        // Lock mouse on click
    if (Input.GetMouseButtonDown(0))
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    // Unlock mouse on Escape
    if (Input.GetKeyDown(KeyCode.Escape))
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
        Jump();
    }    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //going to add in camera tracking for player so that the movement is relative to the camera's forward direction instead of world space, so that the player can move in any direction based on where the camera is facing
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Flatten to XZ plane (no vertical component)
        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        // Build movement relative to camera
        Vector3 movement = (cameraForward * vertical + cameraRight * horizontal).normalized;
            rb.AddForce(movement * speed);
        
    }
    
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // makes sure that the player is grounded before they can jump again 
        ContactPoint contact = collision.contacts[0];

        if(contact.normal.y > 0.5f)
        {
            isGrounded = true;
        }

    }
}
