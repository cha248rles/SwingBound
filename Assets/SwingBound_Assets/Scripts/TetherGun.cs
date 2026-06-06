using UnityEngine;

public class TetherGun : MonoBehaviour
{
    [Header("References")]
    public Camera cam;
    public LineRenderer lineRenderer;
    public Rigidbody playerRigidbody;
    
    [Header("Tether Settings")]
    public float maxRange = 100f;
    public float pullForce = 20f;
    public float maxSpeed = 15f;
    [Tooltip("Aim-assist radius. Larger = easier to latch onto platforms without aiming dead-center.")]
    public float aimAssistRadius = 2f;
    
    [Header("Audio")]
    public AudioClip tetherAttachSFX;
    // public AudioClip tetherReleaseSFX;
    private AudioSource audioSource;
    
    private bool isHolding = false;
    private Vector3 grapplePoint;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Only handle input if game is playing
        if (LevelManager.IsPlaying)
        {
            HandleInput();
        }
        
        DrawTetherLine();
    }

    void FixedUpdate()
    {
        if (isHolding)
            PullPlayer();
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
            TryGrapple();
        
        if (Input.GetMouseButtonUp(0) && isHolding)
            ReleaseTether();
        
        if (Input.GetKeyDown(KeyCode.R))
            ReleaseTether();
    }

    void TryGrapple()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        Debug.DrawRay(ray.origin, ray.direction * maxRange, Color.red, 1f);

        int layerMask = ~LayerMask.GetMask("Player");

        if (TryGetGrappleHit(ray, layerMask, out RaycastHit hitInfo))
        {
            grapplePoint = hitInfo.point;
            isHolding = true;
            Debug.Log("Tether attached to: " + hitInfo.collider.name);

            // Play tether attach sound
            PlaySound(tetherAttachSFX);
        }
    }

    // Precise center hit first, then a forgiving fat cast so off-center aim still
    // latches onto a platform. The fat cast still can't reach through walls,
    // since SphereCast stops at the first collider it touches.
    bool TryGetGrappleHit(Ray ray, int layerMask, out RaycastHit hitInfo)
    {
        if (Physics.Raycast(ray, out hitInfo, maxRange, layerMask)
            && hitInfo.collider.CompareTag("GrapplePoint"))
            return true;

        if (Physics.SphereCast(ray, aimAssistRadius, out hitInfo, maxRange, layerMask)
            && hitInfo.collider.CompareTag("GrapplePoint"))
            return true;

        return false;
    }

    void PullPlayer()
    {
        if (playerRigidbody == null) return;
        
        Vector3 direction = (grapplePoint - playerRigidbody.position).normalized;
        playerRigidbody.AddForce(direction * pullForce, ForceMode.Acceleration);
        
        // Cap speed while grappling
        if (playerRigidbody.linearVelocity.magnitude > maxSpeed)
            playerRigidbody.linearVelocity = playerRigidbody.linearVelocity.normalized * maxSpeed;
        
        // Auto release when close enough to the grapple point
        if (Vector3.Distance(playerRigidbody.position, grapplePoint) < 1.5f)
            ReleaseTether();
    }

    void ReleaseTether()
    {
        if (!isHolding) return;
        
        isHolding = false;
        
        // PlaySound(tetherReleaseSFX);
    }

    void DrawTetherLine()
    {
        if (lineRenderer == null) return;
        
        if (isHolding)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, playerRigidbody.position);
            lineRenderer.SetPosition(1, grapplePoint);
            return;
        }
        
        lineRenderer.enabled = false;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void OnDestroy() { ReleaseTether(); }
}