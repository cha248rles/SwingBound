using UnityEngine;

public class LootBehavior : MonoBehaviour
{
    public int scoreAmount = 10;
    public AudioClip lootSFX;

    void Start()
    {
    }

    void Update()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime);
        if(transform.position.y < Random.Range(1.0f, 3.0f)) 
        {
            Destroy(gameObject.GetComponent<Rigidbody>()); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LevelManager.AddScore(scoreAmount);
            
            if(lootSFX != null)
                AudioSource.PlayClipAtPoint(lootSFX, transform.position);
            
            Destroy(gameObject);
        }
    }
}