using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 5;
    public float dashForce = 20f;      
    public bool isDead;
    bool canDoubleJump;
    bool isGrounded;
    bool canDash;
    
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
        isDead = false;
        canDoubleJump = true;
        canDash = true;
    }

    void Update()
    {
    //Locks controls on death
    //Early death behavior will edit later to be more detailed 
    if (transform.position.y < 5)
        {
            isDead = true;
        }
    if (isDead)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
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
    if(!isDead){
        Jump();
        Dash();
    }
    }    void FixedUpdate()
    {
        if(!isDead)
        {
            Move();
        }
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
    void Dash()              
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && canDash && !isGrounded)
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        
        cameraForward.y = 0;
        cameraForward.Normalize();
        
        rb.linearVelocity = cameraForward * dashForce;
        canDash = false;
    }
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            canDoubleJump = true;
        }
        // Second jump while in air
        else if(Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canDoubleJump = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // makes sure that the player is grounded before they can jump again 
        ContactPoint contact = collision.contacts[0];

        if(contact.normal.y > 0.5f)
        {
            isGrounded = true;
            canDash = true; // Reset dash when grounded
        }

    }
}
