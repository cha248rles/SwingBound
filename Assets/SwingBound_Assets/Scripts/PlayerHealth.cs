using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private LevelManager levelManager;
    public int maxHealth = 2;
    public float invincibilityDuration = 1.5f;
    public AudioClip damageSfx;
    
    private bool isInvincible = false;
    void Start()
    {
        if (levelManager == null)
            levelManager = FindAnyObjectByType<LevelManager>();
    }

    public void TakeDamage()
    {
        if (isInvincible)
            return;

        maxHealth--;
        if (damageSfx != null)
            AudioSource.PlayClipAtPoint(damageSfx, transform.position, 1f);
        if (maxHealth <= 0)
            Die();
        else
            StartCoroutine(BecomeInvincible());
    }

    private IEnumerator BecomeInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
            TakeDamage();
    }

    public void Die()
    {
        if (levelManager != null)
            levelManager.LevelLost();

        Destroy(gameObject);
    }
}
