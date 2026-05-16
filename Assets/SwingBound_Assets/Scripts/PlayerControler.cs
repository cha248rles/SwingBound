using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControler : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 5;

    bool isGrounded;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Jump();
    }
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;

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
        //Makes Sure that the player is grounded before they can jump again 
        ContactPoint contact = collision.contacts[0];

        if(contact.normal.y > 0.5f)
        {
            isGrounded = true;
        }

    }
}
