using UnityEngine;

public class LootBehavior : MonoBehaviour
{
    public int healthAmount = 5;
    public int spellAmount = 5;
    public AudioClip lootSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime);
        if(transform.position.y < Random.Range(1.0f, 3.0f)) 
        {
            Destroy(gameObject.GetComponent<Rigidbody>()); 
        }
    }
}