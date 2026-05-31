using UnityEngine;

// left click + hold to grapple onto any trees (both small and big)
public class TetherGun : MonoBehaviour
{
    [Header("References")]
    public Camera cam;
    public LineRenderer lineRenderer;
    public Rigidbody playerRigidbody;

    [Header("Tether Settings")]
    public float maxRange    = 100f;
    public float pullForce   = 20f;     
    public float maxSpeed    = 15f;     

    private bool isHolding = false;
    private Vector3 grapplePoint;

    void Update()
    {
        HandleInput();
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
        RaycastHit hitInfo;

        Debug.DrawRay(ray.origin, ray.direction * maxRange, Color.red, 1f);

        int layerMask = ~LayerMask.GetMask("Player");

        if (Physics.Raycast(ray, out hitInfo, maxRange, layerMask))
        {
            Debug.Log("Raycast hit: " + hitInfo.collider.name + " tag: " + hitInfo.collider.tag);

            if (!hitInfo.collider.CompareTag("GrapplePoint"))
            {
                Debug.Log("Not a GrapplePoint, ignoring.");
                return;
            }

            grapplePoint = hitInfo.point;
            isHolding = true;
            Debug.Log("Tether attached to: " + grapplePoint);
        }
        else
        {
            Debug.Log("Raycast hit nothing.");
        }
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
        isHolding = false;
    }

    void DrawTetherLine()
    {
        if (lineRenderer == null) return;

        if (isHolding)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, grapplePoint);
            return;
        }

        lineRenderer.enabled = false;
    }

    void OnDestroy() { ReleaseTether(); }
}