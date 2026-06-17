using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject interactPrompt;
    public GameObject textBubble;
    private bool interactable = false;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable)
        {
            if(Input.GetKeyDown("e"))
            {
                Talking();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            animator.SetBool("open_Animation", true);
            interactPrompt.SetActive(true);
            interactable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            animator.SetBool("open_Animation", false);
            interactPrompt.SetActive(false);
            textBubble.SetActive(false);
            interactable = false;
        }
    }

    void Talking()
    {
        interactPrompt.SetActive(false);
        textBubble.SetActive(true);
    }
}
