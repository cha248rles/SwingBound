using UnityEngine;

public class LootBehavior : MonoBehaviour {
    public int scoreValue = 1;
    public AudioClip pickupSFX;
    
    public static int pickupCount = 0;
    LevelManager levelManager;
    
    void Start()
    {
        pickupCount++;
        Debug.Log("Pickup count from " + transform.name + " " + pickupCount);
        levelManager = FindAnyObjectByType<LevelManager>();
        Debug.Log("Found LM: " + levelManager.name);
    }
    
    void Update()
    {

    }
    
    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            DestroyPickup();
        }
    }
    
    void DestroyPickup()
{
    LevelManager.AddScore(scoreValue);
    Debug.Log("Pickup collected! Score: " + LevelManager.Score);
    
    PlayAudioEffect();
    
    // Removed animator code
    pickupCount--;
    Destroy(gameObject);
    }
    
    void PlayAudioEffect()
    {
        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
    }
    
    public static void ResetPickups()
    {
        pickupCount = 0;
        LevelManager.ResetScore();  // Reset via LevelManager
    }
}