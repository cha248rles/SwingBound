using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private LevelManager levelManager;
    public int maxHealth = 2;
    void Start()
    {
        if (levelManager == null)
            levelManager = FindAnyObjectByType<LevelManager>();
    }

    public void TakeDamage()
    {
        maxHealth--;
        if (maxHealth <= 0)
            Die();
        
    }

    //void OnCollisionEnter(Collision collision)
    //{
     //   if (collision.gameObject.CompareTag("Spike"))
    //        TakeDamage();
    //}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spike"))
            TakeDamage();
    }

    public void Die()
    {
        if (levelManager != null)
            levelManager.LevelLost();

        Destroy(gameObject);
    }
}
