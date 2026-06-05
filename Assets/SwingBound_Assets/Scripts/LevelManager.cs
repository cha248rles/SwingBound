using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class LevelManager : MonoBehaviour
{
    public static bool IsPlaying {get; private set;}
    [SerializeField] public bool isFinalLevel;
    [SerializeField] public string nextLevel;
    [SerializeField] public TMP_Text messageText;
    [SerializeField] public GameObject nextButton;
    [SerializeField] public GameObject restartButton;
    [SerializeField] public AudioClip winSFX;
    [SerializeField] public AudioClip loseSFX;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        IsPlaying = true;
    } 

    // Update is called once per frame
    void Update()
    {        
        
    }
    public void LevelBeat()
    {
       IsPlaying = false;
        // play soundSFX
        PlaySoundClip(winSFX);
        if(isFinalLevel)
        {
            DisplayGameMessage("GAME COMPLETE!");
            restartButton.SetActive(true);
        } else
        {
            DisplayGameMessage("YOU WIN!");
            nextButton.SetActive(true);
        }
    }

    public void LevelLost()
    {
        IsPlaying = false;
        PlaySoundClip(loseSFX);
        DisplayGameMessage("GAME OVER!");
        Invoke("ReloadSameScene", 2);
    }

    private void PlaySoundClip(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioClip is null!");
        }
    }

    private void DisplayGameMessage(string message)
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
            messageText.text = message;
        }
    }

    public void LoadSceneByName(string name)
    {
        SceneManager.LoadScene(name);
    }

    void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    void ReloadSameScene()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        if(isFinalLevel)
        {
            SceneManager.LoadScene("Level1");
        }
        else if(nextLevel.Length > 0)
        {
           LoadSceneByName(nextLevel); 
        }
        else {
            Debug.LogWarning("No nextLevel is specified in the inspector.");
        }
    }
}
