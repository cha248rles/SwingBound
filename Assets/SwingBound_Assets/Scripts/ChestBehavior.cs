using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    private bool isOpen = false;
    public Animator animator;
    public AudioClip openSFX;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator != null)
            animator.enabled = false;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OpenChest()
    {   
        if (!isOpen)
        {
            isOpen = true;

            if (animator != null)
            {
                animator.enabled = true;
                animator.SetBool("IsOpen", true);
            }

            if (openSFX && audioSource)
            {
                audioSource.clip = openSFX;
                audioSource.Play();
            }
        }
    }

    public bool IsOpen() { return isOpen; }

    public void SetVisible(bool visible)
    {
        foreach (Renderer renderer in GetComponentsInChildren<Renderer>())
            renderer.enabled = visible;
    }

    void Update()
    {
        
    }
}