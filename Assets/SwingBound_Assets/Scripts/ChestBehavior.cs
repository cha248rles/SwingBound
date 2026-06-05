using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    private bool isOpen = false;
    private AudioSource audioSource;
    
    [Header("References")]
    public Animator animator;
    public AudioClip openSFX;
    private LevelManager levelManager;
    private Rigidbody playerRigidbody;

    void Start()
    {
        // Get animator from children
        animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.enabled = false;
            animator.SetBool("IsOpen", false);
        }

        // Create or get AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        
        audioSource.playOnAwake = false;

        // Get LevelManager reference
        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager == null)
            Debug.LogError("LevelManager not found in scene!");

        // Ensure collider exists and is set as trigger
        Collider collider = GetComponent<Collider>();
        if (collider == null)
        {
            Debug.LogError("Chest needs a Collider! Adding BoxCollider...");
            collider = gameObject.AddComponent<BoxCollider>();
        }
        collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider collision)
    {
        // Check if the colliding object is the player
        if (collision.CompareTag("Player") && !isOpen)
        {
            playerRigidbody = collision.GetComponent<Rigidbody>();
            OpenChest();
        }
    }

    public void OpenChest()
    {
        if (isOpen) return;

        isOpen = true;
        Debug.Log("Chest opened!");

        // Freeze player movement
        if (playerRigidbody != null)
        {
            playerRigidbody.linearVelocity = Vector3.zero;  // Stop current velocity
            playerRigidbody.isKinematic = true;       // Freeze rigidbody
        }

        // Enable and play animator
        if (animator != null)
        {
            animator.enabled = true;
            animator.SetBool("IsOpen", true);
        }

        // Play sound effect
        if (openSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(openSFX);
        }

        // Trigger game win
        if (levelManager != null)
        {
            levelManager.LevelBeat();
        }
        else
        {
            Debug.LogError("LevelManager not found!");
        }
    }

    public bool IsOpen() { return isOpen; }

    public void SetVisible(bool visible)
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            renderer.enabled = visible;
    }
}