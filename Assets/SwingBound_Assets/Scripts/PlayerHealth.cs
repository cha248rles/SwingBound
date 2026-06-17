using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private LevelManager levelManager;
    public int maxHealth = 2;
    public int currentHealth;
    public Slider healthSlider;
    public float invincibilityDuration = 1.5f;
    public AudioClip damageSFX;
    private bool isInvincible = false;
    void Start()
    {
        if (levelManager == null)
        {
            levelManager = FindAnyObjectByType<LevelManager>();
        }

        currentHealth = maxHealth;
        UpdateHealthSlider();
    }

    public void TakeDamage()
    {
        if (isInvincible)
            return;

        currentHealth--;
        UpdateHealthSlider();

        if (damageSFX != null)
            AudioSource.PlayClipAtPoint(damageSFX, transform.position, 1f);

        if (currentHealth <= 0)
            Die();
        else
            StartCoroutine(BecomeInvincible());
    }

    private void UpdateHealthSlider()
    {
        if(healthSlider != null)
        {
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
        }
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
