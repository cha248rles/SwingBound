using UnityEngine;
using UnityEngine.AdaptivePerformance;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 5;
    public float dashForce = 20f;
    public float brakeMultiplier = 3f;  // extra force when pushing against momentum

    [Header("Audio")]
    public AudioClip jumpSFX;
    public AudioClip tetherSFX;
    public AudioClip deathSFX;

    public bool isDead;
    bool canDoubleJump;
    bool isGrounded;
    bool canDash;
    
    Rigidbody rb;
    AudioSource audioSource;
    LevelManager levelManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager == null)
        {
            Debug.LogError("LevelManager not found in scene!");
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        canDoubleJump = true;
        canDash = true;
    }

    void Update()
    {
        // Check if player fell off the map
        if (transform.position.y < 5 && LevelManager.IsPlaying)
            {
                PlayerDied();
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

            // Only allow controls if game is still playing
            if (LevelManager.IsPlaying)
            {
                Jump();
                Dash();
            }
    }    
    void FixedUpdate()
    {
        if(LevelManager.IsPlaying)
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

        // Extra stopping power when pushing against current momentum, so stops feel crisp
        float brake = (Vector3.Dot(movement, rb.linearVelocity) < 0) ? brakeMultiplier : 1f;
        rb.AddForce(movement * speed * brake);

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

            PlaySound(tetherSFX);
        }
    }
    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            canDoubleJump = true;

            PlaySound(jumpSFX);
        }
        // Second jump while in air
        else if(Input.GetKeyDown(KeyCode.Space) && canDoubleJump && !isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canDoubleJump = false;

            PlaySound(jumpSFX);
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

    void PlayerDied()
    {
        Debug.Log("Player died!");
        
        // Play death sound
        PlaySound(deathSFX);
        
        // Call LevelManager to handle game over
        if (levelManager != null)
        {
            levelManager.LevelLost();
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else if (clip == null)
        {
            Debug.LogWarning("Audio clip is not assigned to PlayerController!");
        }
    }
}
